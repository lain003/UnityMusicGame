using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MiniJSON;
using System.Net;
using System;
using UniRx;
using System.IO;

public class MyServer {
	#if UNITY_EDITOR
	private static string serverUrl = "http://localhost:3000";
	#else
	private static string serverUrl = "http://:3000";
	#endif
	//private static string serverUrl = "";

	public static void start_downloadTracks(Action<MyServerTracks> afterAction){
		Dictionary<string,string> header = new Dictionary<string,string>();
		header.Add ("Accept-Language", "ja");
		string url = serverUrl + "/tracks.json";

		ObservableWWW.Get (url, header).Subscribe (body => {
			List<object> trackJsons = (List<object>)Json.Deserialize (body);
			MyServerTracks tracks = new MyServerTracks (trackJsons);
			afterAction(tracks);
		});
	}

	public static void start_updateTrack(string trackJson,int server_id){
		string requestUrl = serverUrl + "/tracks/" + server_id + ".json";
		MyHttp.sendRequest (requestUrl, "PUT", trackJson, "application/json; charset=UTF-8", 5000,
		response => {
			Encoding enc = System.Text.Encoding.GetEncoding ("UTF-8");
			StreamReader sr = new StreamReader (response.GetResponseStream (), enc);
			//string str = sr.ReadToEnd ();
			sr.Close ();
			Debug.Log("Track Update");
		},
		exception => {
			Debug.LogWarning (exception.Message);
		});
	}
}
