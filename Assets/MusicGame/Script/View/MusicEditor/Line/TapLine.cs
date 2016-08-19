using UnityEngine;
using System.Collections;
using UniRx;

public class TapLine : MonoBehaviour {
	[HideInInspector]
	public bool is_justmade;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void on_justmade(){
		this.is_justmade = true;
		Observable.TimerFrame(100).Subscribe(_ => is_justmade = false);
	}
}
