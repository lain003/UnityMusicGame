using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class MyServerTracks {
	private List<MyServerTrack> tracks;
	
	public MyServerTracks(){
		tracks = new List<MyServerTrack> ();
	}
	
	public MyServerTracks(List<object> tracksJson){
		tracks = new List<MyServerTrack> ();

		foreach (Dictionary<string,object> trackJson in tracksJson) {
			MyServerTrack track = new MyServerTrack (trackJson);
			tracks.Add (track);
		}
	}
	
	public List<MyServerTrack> getTracks(){
		return tracks;
	}
}
