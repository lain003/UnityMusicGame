using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SoundCloudTrack {
	public string title;
	public string artwork_url;
	//public string avatar_url;
	public long id;

	public SoundCloudTrack(Dictionary<string,object> trackJson){
		this.title = (string)trackJson["title"];
		this.artwork_url = (string)trackJson ["artwork_url"];
		this.id = (long)trackJson ["id"];
		//this.avatar_url = (string)trackJson ["artwork_url"];
	}
}
