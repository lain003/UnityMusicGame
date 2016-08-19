using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class MetronomeTogglePresenter : MonoBehaviour {
	public Toggle toggle;
	public AudioSource metoro_audioSource;

	void Start () {
		toggle.OnValueChangedAsObservable().Subscribe(x => metoro_audioSource.mute = !x);
	}
}
