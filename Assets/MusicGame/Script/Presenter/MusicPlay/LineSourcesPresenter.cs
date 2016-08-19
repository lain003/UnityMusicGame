using UnityEngine;
using UniRx;
using System.Collections.Generic;

public class LineSourcesPresenter : MonoBehaviour {
	public List<TapLineSource> _tapLineSources;
	public List<HoldLineSource> _holdLineSources;
	private Dictionary<int,TapLineSource> tapLineSources;
	private Dictionary<int,HoldLineSource> holdLineSources;
	public BGMAudioSource_Play bgmAudioSource;
	private float timeToReach;

	void Start () {
		this.tapLines_ToDitionary ();
		this.holdLines_ToDitionary ();
		bgmAudioSource.aboutMusicTime.Subscribe(x => this.findTapNotes(x));
		bgmAudioSource.aboutMusicTime.Subscribe(x => this.findHoldNotes_Start(x));
		bgmAudioSource.aboutMusicTime.Subscribe(x => this.findHoldNotes_End(x));
		this.timeToReach = this.caluclate_arrivalTime ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void tapLines_ToDitionary(){
		tapLineSources = new Dictionary<int, TapLineSource> ();
		foreach (TapLineSource tapLineSource in _tapLineSources) {
			this.tapLineSources.Add (tapLineSource.tag, tapLineSource);
		}
	}

	private void holdLines_ToDitionary(){
		this.holdLineSources = new Dictionary<int, HoldLineSource> ();
		foreach (HoldLineSource holdLineSource in _holdLineSources) {
			this.holdLineSources.Add (holdLineSource.tag, holdLineSource);
		}
	}

	private void findTapNotes(float musicTime){
		List<TapNote> tapNotes = TapDatabase.where (musicTime + timeToReach);
		foreach (TapNote tapNote in tapNotes) {
			TapLineSource source = this.tapLineSources [tapNote.tag];
			source.drowLine (timeToReach, tapNote);
		}
	}

	private void findHoldNotes_Start(float musicTime){
		List<HoldNote> notes = HoldDatabase.where_startTime (musicTime + timeToReach);
		foreach (HoldNote note in notes) {
			HoldLineSource source = this.holdLineSources [note.tag];
			source.drowLine ();
		}
	}

	private void findHoldNotes_End(float musicTime){
		List<HoldNote> notes = HoldDatabase.where_endTime (musicTime + timeToReach);
		foreach (HoldNote note in notes) {
			HoldLineSource source = this.holdLineSources [note.tag];
			source.moveHoldEndLine ();
		}
	}

	private float caluclate_arrivalTime(){
		TapLineSource source = this._tapLineSources [0];
		HitIcon hitIcon = source.target_hitIcon;

		float distance_toHiticon = Vector2.Distance (source.transform.localPosition, hitIcon.transform.localPosition);
		float time = distance_toHiticon / MyConfig.Play.NOTE_SPEED;
		float roundTime = (float)MyMath.ToRoundDown (time, MyConfig.Play.EFFECTIVE_DIGIT);
		return roundTime + 0.1f;//微妙にずれるので調整
	}

	private void print_all_distance(){
		Debug.Log ("---------------");
		foreach(TapLineSource source in this._tapLineSources){
			HitIcon hitIcon = source.target_hitIcon;

			float distance_toHiticon = Vector2.Distance (source.transform.localPosition, hitIcon.transform.localPosition);
			Debug.Log (distance_toHiticon);
		}
		Debug.Log ("---------------");
	}
}
