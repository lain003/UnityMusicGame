using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundCloudTracks{
	private List<SoundCloudTrack> tracks;

	public SoundCloudTracks(){
		tracks = new List<SoundCloudTrack> ();
	}

	public SoundCloudTracks(List<object> tracksJson){
		tracks = new List<SoundCloudTrack> ();

		foreach (Dictionary<string,object> trackJson in tracksJson) {
			SoundCloudTrack track = new SoundCloudTrack(trackJson);
			tracks.Add (track);
		}
	}

	public List<SoundCloudTrack> getTracks(){
		return tracks;
	}
}
