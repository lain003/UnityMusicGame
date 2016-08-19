using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class MySlider : MonoBehaviour {
	public AudioSource audioSource;
	public ObservablePointerUpTrigger pointerUpTrigger;
	public ObservablePointerDownTrigger pointerDownTrigger;
	public Slider slider;
	public BGMAudioSourcePresenter bgmAudioSourcePresenter;

	void Start () {
		this.pointerDownTrigger.OnPointerDownAsObservable().Subscribe(_ => audioSource.Pause());

		this.pointerUpTrigger.OnPointerUpAsObservable ().Subscribe (_ => {
			audioSource.time = slider.value * audioSource.clip.length;
			audioSource.Play();
		});

		var repeatStream = Observable.FromCoroutine<Unit>(o => 
			UpdateCoroutine(o, this.pointerDownTrigger.OnPointerDownAsObservable(), this.pointerUpTrigger.OnPointerUpAsObservable()))
			.RepeatUntilDestroy(gameObject);
		this.bgmAudioSourcePresenter.finishMusicDownLoad_Stream
			.First()
			.Subscribe(_ => repeatStream.Subscribe(__ => slider.value = audioSource.time / audioSource.clip.length));
	}

	private IEnumerator UpdateCoroutine(IObserver<Unit> observer, IObservable<PointerEventData> eventB, IObservable<PointerEventData> eventC)
	{
		var isActive = true;
		//Bが来るまで繰り返す
		eventB.First().Subscribe(_ => isActive = false);
		while (isActive)
		{
			observer.OnNext(Unit.Default);
			yield return null;
		}

		//Cが来るまで待機
		yield return eventC.First().StartAsCoroutine();
		observer.OnCompleted();
	}
}
