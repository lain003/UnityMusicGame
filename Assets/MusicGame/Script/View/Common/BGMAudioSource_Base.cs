using UnityEngine;
using System.Collections;
using UniRx;

public abstract class BGMAudioSource_Base : MonoBehaviour {
	public ReactiveProperty<bool> isPlaying;
	public AudioSource audioSource;
	//本来のAudioSourceTimeを返す。
	public ReactiveProperty<float> detailMusicTime { get; private set; }
	//丸めたAudioSourceTimeを返す。
	public abstract ReactiveProperty<float> aboutMusicTime{ get; protected set;}

	protected void OnEnable(){
		this.isPlaying = new ReactiveProperty<bool>(audioSource.isPlaying);
		this.detailMusicTime = this.audioSource.ObserveEveryValueChanged (a => a.time).ToReactiveProperty ();
	}
	protected void Start () {
		this.isPlaying.Where(x => x == true).Subscribe (_ => audioSource.Play());
		this.isPlaying.Where(x => x == false).Subscribe (_ => audioSource.Stop());
	}

	public void play(){
		this.isPlaying.Value = true;
	}

	public void stop(){
		this.isPlaying.Value = false;
	}
}
