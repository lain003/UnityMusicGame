using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class TapNotes:Notes{
	public void addNote (TapNote add_note){
	}

	public void removeNote (TapNote remove_note){
	}

	public List<TapNote> findNotes (int time){
		IEnumerable findObjects = duplicate_dic.findObjects(time);
		if (findObjects != null) {
			return findObjects.OfType<TapNote> ().ToList ();
		} else {
			return null;
		}
	}

	public override void importList(List<object> importList){
		foreach (Dictionary<string,object> dic in importList) {
			TapNote note = new TapNote(dic);
			this.addNote(note);
		}
	}
}
