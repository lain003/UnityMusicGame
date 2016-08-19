using UnityEngine;
using System.Collections;
using UniRx;
using System;
using MiniJSON;
using System.Collections.Generic;

public class SoundCloud {

	private static IEnumerator _start_downloadTracks(){
		string sURL = add_clientID(add_parameter ("https://api.soundcloud.com/tracks","q=ラブライブ"));
		WWW www = new WWW(sURL);
		yield return www;

		List<object> noteJsons = (List<object>)Json.Deserialize (www.text);

		//SoundCloudTracks tracks = new SoundCloudTracks (noteJsons);
		//this.myDelegate.finish_downloadTracks (tracks);
	}

	public static IEnumerator start_downloadMusic(IObserver<AudioClip> observer, MyServerTrack track){
		string sURL = add_clientID("https://api.soundcloud.com/tracks/" + track.soundCloud_id.ToString () + "/stream");
		Debug.Log ("Start Download Music:URL = " + sURL);

		Dictionary<string, string> headers = new Dictionary<string, string>();
		//headers.Add("Connection", "close");
		WWW www = new WWW(sURL);
		yield return www;

		Debug.Log (www.error);
		Debug.Log ("Finish Download Music:status = " + www.responseHeaders ["STATUS"]);
		AudioClip download_audioClip = www.GetAudioClip (false,false,AudioType.MPEG);
		observer.OnNext(download_audioClip);
		observer.OnCompleted ();
	}

	private static string add_clientID(string baseUri_s){
		return add_parameter (baseUri_s, "client_id=");
	}

	private static string add_parameter(string baseUri_s,string parameter_s){
		UriBuilder baseUri = new UriBuilder(baseUri_s);
		
		if (baseUri.Query != null && baseUri.Query.Length > 1)
			baseUri.Query = baseUri.Query.Substring(1) + "&" + parameter_s; 
		else
			baseUri.Query = parameter_s; 
		
		return baseUri.ToString ();
	}
}