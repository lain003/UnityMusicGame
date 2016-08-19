using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class AutoPlay : MonoBehaviour {
	public HitIcon hitIcon;
	public ObservableTrigger2DTrigger collider_trigger;
	public BGMAudioSource_Base bgmAudio;
	// Use this for initialization
	void Start () {
		collider_trigger.OnTriggerEnter2DAsObservable ().Subscribe (x => {
			this.hitIcon.playSE ();
			TapLine_Play tapIcon = x.gameObject.GetComponent<TapLine_Play>();

			Debug.Log("Tag=" + this.hitIcon.tag + " 到達時間=" + this.bgmAudio.aboutMusicTime + tapIcon.tapNote.ToString());
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
