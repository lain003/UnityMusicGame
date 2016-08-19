using UnityEngine;
using System.Collections;

public class HoldLine_Play : MonoBehaviour{
	public HoldStart_Play holdStart;
	public HoldEnd_Play holdEnd;
	public void initialize(Vector2 position, Vector2 velocity,int speed){
		this.transform.position = position;
		this.holdStart.initialize (new Vector2 (0, 0), velocity, speed);
		this.holdEnd.initialize (new Vector2 (0, 0), velocity, speed);
		this.holdStart.move ();
	}

	public void move_holdEnd(){
		this.holdEnd.move ();
	}

	public void stop_holdStart(){
		this.holdStart.stop ();
	}
}