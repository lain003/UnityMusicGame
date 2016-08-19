using UnityEngine;
using System.Collections;

public class BaseSheet : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void move(float musicTime){
		this.gameObject.transform.localPosition = new Vector2 (this.transform.localPosition.x, -musicTime * MyConfig.Editor.NOTE_SPEED);
	}
}
