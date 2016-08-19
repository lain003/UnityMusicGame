using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MiniJSON;
using System.Net;
using System;
using UniRx;
using System.IO;

public class MyHttp {
	public static IEnumerator get(Dictionary<string,string> header,string url,MonoBehaviour mono,float timeout ,Action<string> afterAction){
		WWW www = new WWW (url, null, header);
		yield return mono.StartCoroutine(ResponceCheckForTimeOutWWW(www, timeout));
		
		if (www.error != null) {
			Debug.Log(www.error);
		}
		
		afterAction (www.text);
	}

	private static IEnumerator ResponceCheckForTimeOutWWW(WWW www, float timeout)
	{
		float requestTime = Time.time;
		
		while(!www.isDone)
		{
			if(Time.time - requestTime < timeout){
				yield return null;
			}
			else{
				Debug.LogWarning("TimeOut");
				break;
			}
		}
		
		yield return null;
	}

	public static void sendRequest(string url,string methodName,string body,string contentType,int timeout,Action<HttpWebResponse> afterAction,Action<WebException> exceptionAction){
		byte[] bodyData = Encoding.Default.GetBytes (body);

		HttpWebRequest request = (HttpWebRequest)WebRequest.Create (url); 
		request.Method = methodName;
		request.ContentType = contentType;
		request.ContentLength = bodyData.Length;
		request.Timeout = timeout;//ms
		
		Observable.Start (() => {
			Stream reqStream = request.GetRequestStream ();
			reqStream.Write (bodyData, 0, bodyData.Length);
			reqStream.Close ();
			return (HttpWebResponse)request.GetResponse ();
		}).ObserveOnMainThread ().CatchIgnore ((WebException ex) => {
			exceptionAction(ex);
		}).Subscribe (response => {
			afterAction(response);
		});
	}
}
