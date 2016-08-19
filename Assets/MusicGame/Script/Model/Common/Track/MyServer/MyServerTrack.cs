using System.Collections.Generic;
using UniRx;
using MiniJSON;
using UnityEngine;
using System;

[Serializable]
public class MyServerTrack {
	public long soundCloud_id;
	[NonSerialized]
	public string title;
	[NonSerialized]
	public int server_id;
	public int bpm;
	public int position;
	public List<TapNote> tapNotes;
	public List<HoldNote> holdNotes;

	public MyServerTrack(Dictionary<string,object> trackJson){
		this.title = (string)trackJson["title"];
		this.server_id = (int)(long)trackJson ["id"];
		this.soundCloud_id = (long)trackJson ["soundcloud_id"];
		this.bpm = (int)(long)trackJson ["bpm"];
		this.position = (int)(long)trackJson ["position"];

		Dictionary<string,object> noteJson = (Dictionary<string,object>)Json.Deserialize ((string)trackJson ["note"]);
		this.tapNotes = TapNote.convertToTapNotes((List<object>)noteJson["tapNotes"]);
		this.holdNotes = HoldNote.convertToTapNotes((List<object>)noteJson["holdNotes"]);
	}

	public MyServerTrack(MyServerTrack originalTrack, BPM _bpm, List<TapNote> _tapNotes, List<HoldNote> _holdNotes){
		this.soundCloud_id = originalTrack.soundCloud_id;
		this.title = originalTrack.title;
		this.server_id = originalTrack.server_id;
		this.bpm = _bpm.value.Value;
		this.position = _bpm.positioning.Value;
		this.tapNotes = _tapNotes;
		this.holdNotes = _holdNotes;
	}
}
