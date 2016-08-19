using UnityEngine;
using System.Collections;
using UniRx;

public class BGMAudioSource_Editor : BGMAudioSource_Base {
	public override ReactiveProperty<float> aboutMusicTime{ get; protected set;}

	void OnEnable(){
		base.OnEnable ();
		this.aboutMusicTime = this.audioSource.ObserveEveryValueChanged (a => (float)MyMath.ToRoundDown(a.time,MyConfig.Editor.EFFECTIVE_DIGIT)).ToReactiveProperty ();
	}

	void Start(){
		base.Start ();
	}
}
