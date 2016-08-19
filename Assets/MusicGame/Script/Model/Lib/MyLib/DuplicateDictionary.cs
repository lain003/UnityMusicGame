using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DuplicateDictionary{
	protected Dictionary<float, List<System.Object>> dictionary;

	public Dictionary<float, List<System.Object>> Dictionary{
		get{return this.dictionary;}
	}

 	public DuplicateDictionary(){
		dictionary = new Dictionary<float, List<System.Object>>();
  	}

	public void addObject(float key,System.Object value){
    	if (!dictionary.ContainsKey (key)) {
			dictionary[key] = new List<System.Object> ();
    	}
		dictionary[key].Add (value);
  	}

	public void removeObject(float key,System.Object value){
    	if (dictionary.ContainsKey (key)) {
			List<System.Object> objects = dictionary [key];
      		objects.Remove (value);
    	}
  	}

	public void remove(float key){
		dictionary.Remove (key);
	}

	public IEnumerable findObjects(float key){
		if (dictionary.ContainsKey (key)) {
      		return dictionary [key];
    	}

		return null;
  	}
}
