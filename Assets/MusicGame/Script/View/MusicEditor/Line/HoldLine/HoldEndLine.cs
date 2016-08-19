using UnityEngine;
using System.Collections;

public class HoldEndLine : MonoBehaviour {
	private Vector2 keepPosition;
	private bool keepFlag = false;
	public Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate() {
		if (keepFlag) {
			this.transform.position = this.keepPosition;
		}
	}
	void Update () {

	}

	public void start_keepPosition(){
		keepPosition = this.transform.position;

		keepFlag = true;
	}

	public void end_keepPosition(){
		keepFlag = false;
	}
}
