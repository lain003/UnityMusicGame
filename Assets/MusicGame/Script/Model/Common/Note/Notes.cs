using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public abstract class Notes{
	protected DuplicateDictionary duplicate_dic;

	public Notes(){
		duplicate_dic = new DuplicateDictionary();
	}

	public List<Dictionary<string,string>> exportList(){
		List<Dictionary<string,string>> noteDictionaries = new List<Dictionary<string,string>> ();
		
		foreach(int key_time in duplicate_dic.Dictionary.Keys){
			foreach(Note note in duplicate_dic.Dictionary[key_time]){
				noteDictionaries.Add (note.toDictionary());
			}
		}
		
		return noteDictionaries;
	}

	public abstract void importList(List<object> importList);
}