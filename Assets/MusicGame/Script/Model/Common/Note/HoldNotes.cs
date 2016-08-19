using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Linq;

public class HoldNotes:Notes{
	public HoldNotes(){
	}
	
	public void addNote(HoldNote add_note){
		duplicate_dic.addObject(add_note.start_time ,add_note);
	}
	
	public void removeNote(HoldNote remove_note){
		duplicate_dic.removeObject (remove_note.start_time, remove_note);
	}
	
	public List<HoldNote> findNotes(int time){
		IEnumerable findObjects = duplicate_dic.findObjects(time);
		if (findObjects != null) {
			return findObjects.OfType<HoldNote> ().ToList ();
		} else {
			return null;
		}
	}

	public override void importList(List<object> importList){
		foreach (Dictionary<string,object> dic in importList) {
			HoldNote note = new HoldNote(dic);
			this.addNote(note);
		}
	}
}