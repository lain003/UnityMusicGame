using UnityEngine;
using System.Collections;

public abstract class BaseLineSource : MonoBehaviour
{
	void Start (){
	
	}
	
	void Update (){
	
	}

	protected Vector2 calculate_NormalizeVelocity(Vector2 source_position,Vector2 target_position){
		Vector2 pos = target_position - source_position;
		return pos.normalized;
	}
}

