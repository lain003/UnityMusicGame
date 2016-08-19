using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class DotLineCollider : MonoBehaviour {
	public ObservableTrigger2DTriggerCustom trigger;
	public AudioSource audioSource;
	public BGMAudioSource_Editor bgmAudioSource;
	// Use this for initialization
	void Start () {
		trigger.OnTriggerEnter2DAsObservable ().Subscribe (_ => this.playSE());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void playSE(){
		this.audioSource.Play ();
	}
}
