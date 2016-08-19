using UnityEngine;
using System.Collections;

public class HoldLineSource : BaseLineSource{
	public int tag;
	public HoldLine_Play holdLinePrefab;
	public HitIcon target_hitIcon;
	public GameObject lines;
	public HoldLine_Play active_holdLine;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public void drowLine(){
		HoldLine_Play holdLine = Instantiate (this.holdLinePrefab);
		holdLine.transform.SetParent (this.lines.transform, false);
		holdLine.transform.Rotate (new Vector3 (0, 0, this.gameObject.transform.localEulerAngles.z));

		Vector2 velocity = this.calculate_NormalizeVelocity (this.transform.position, target_hitIcon.transform.position);
		holdLine.initialize (this.gameObject.transform.position, velocity, MyConfig.Play.NOTE_SPEED);

		this.active_holdLine = holdLine;
	}

	public void moveHoldEndLine(){
		if (this.active_holdLine == null) {
			return;
		}

		this.active_holdLine.move_holdEnd ();
	}
}

