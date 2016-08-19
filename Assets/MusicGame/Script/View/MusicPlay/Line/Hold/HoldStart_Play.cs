using UnityEngine;
using System.Collections;

public class HoldStart_Play : BaseHoldStartEnd_Play {
	private Vector2 target_position;
	public void initialize(Vector3 position,Vector2 _velocity,float speed,Vector2 _target_position){
		target_position = _target_position;
		base.initialize (position, _velocity, speed);
	}
}