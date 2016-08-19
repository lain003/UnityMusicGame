using System.Collections.Generic;
using System.Collections;
using System;

[Serializable]
public class AllNote {
	public List<TapNote> tapNotes;
	public List<HoldNote> holdNotes;

	public AllNote(List<TapNote> _tapNotes,List<HoldNote> _holdNotes){
		this.tapNotes = _tapNotes;
		this.holdNotes = _holdNotes;
	}

	public Dictionary<string, List<Dictionary<string,string>>> toDict(){
		List<Dictionary<string,string>> holdNotes_json = new List<Dictionary<string,string>> ();
		foreach (HoldNote holdNote in holdNotes) {
			Dictionary<string,string> dic = new Dictionary<string, string> ();
			dic.Add ("tag", holdNote.tag.ToString());
			dic.Add ("start_time", holdNote.start_time.ToString());
			dic.Add ("end_time", holdNote.end_time.ToString());
			holdNotes_json.Add (dic);
		}

		List<Dictionary<string,string>> tapNotes_json = new List<Dictionary<string,string>> ();
		foreach (TapNote tapNote in tapNotes) {
			Dictionary<string,string> dic = new Dictionary<string,string> ();
			dic.Add ("tag", tapNote.tag.ToString());
			dic.Add ("time", tapNote.time.ToString());
			tapNotes_json.Add (dic);
		}

		var notes_json = new Dictionary<string, List<Dictionary<string,string>>> ();
		notes_json ["tapNote"] = tapNotes_json;
		notes_json ["holdNote"] = holdNotes_json;

		var dic1 = new Dictionary<string, Dictionary<string,List<Dictionary<string,string>>>> ();
		dic1 ["note"] = notes_json;

		//var dic2 = new Dictionary<string, Dictionary<string, Dictionary<string,List<Dictionary<string,string>>>>> ();
		//dic2 ["track"] = dic1;
		return notes_json;
	}
}
