using UnityEngine;
using UniRx;

public class TapLine_Play : MonoBehaviour {
	public Rigidbody2D rigidbody;
	private Vector2 velocity;
	public TapNote tapNote;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public void initialize(Vector3 position, Vector2 _velocity, float timeToReach, Vector3 _endPosition, TapNote _tapNote){
		this.transform.position = position;
		this.tapNote = _tapNote;

		float speed = Vector2.Distance (this.transform.position, _endPosition) / timeToReach / _velocity.magnitude;
		this.velocity = _velocity * speed;
	}

	public void move(){
		rigidbody.velocity = this.velocity;
	}

	void OnBecameInvisible(){
		Destroy (this.gameObject);
	}
}
