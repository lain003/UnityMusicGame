using UnityEngine;
using System.Collections;

public class TapLineSource : BaseLineSource {
	public int tag;
	public TapLine_Play tapLinePrefab;
	public HitIcon target_hitIcon;
	public GameObject lines;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void drowLine(float timeToReach, TapNote tapNote){
		TapLine_Play tapLine = Instantiate (this.tapLinePrefab);
		tapLine.transform.SetParent (this.lines.transform, false);
		tapLine.transform.localPosition = this.gameObject.transform.position;
		tapLine.transform.Rotate(new Vector3(0,0,this.gameObject.transform.localEulerAngles.z));

		Vector2 velocity = this.calculate_NormalizeVelocity (this.transform.position, target_hitIcon.transform.position);
		tapLine.initialize (this.gameObject.transform.position, velocity, timeToReach, target_hitIcon.transform.position, tapNote);
		tapLine.move ();
	}
}
