using UnityEngine;
using System.Collections;

public abstract class BaseHoldStartEnd_Play : MonoBehaviour
{
	public Rigidbody2D rigidbody;
	private Vector2 velocity;

	public void initialize(Vector3 position,Vector2 _velocity,float speed){
		this.transform.localPosition = position;
		this.velocity = _velocity * speed * 0.02f;
	}

	public void move(){
		rigidbody.velocity = this.velocity;
	}
	public void stop(){
		this.rigidbody.velocity = Vector3.zero;
	}
}