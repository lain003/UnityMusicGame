using UnityEngine;
using System.Collections;

public class HoldLine : MonoBehaviour {
	public static HoldLine holdLinePrefab;
	public HoldStartLine holdStartLine;
	public HoldEndLine holdEndLine;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void initialize(float startLinePosition_y,float endLinePosition_y,bool is_endLineKeep = false){
		this.transform.localPosition = new Vector2 (0, 0);
		this.holdStartLine.transform.localPosition = new Vector2(0, startLinePosition_y);
		this.holdEndLine.transform.localPosition = new Vector2(0,endLinePosition_y);
		if (is_endLineKeep) {
			this.holdEndLine.start_keepPosition ();
		}
	}
}
