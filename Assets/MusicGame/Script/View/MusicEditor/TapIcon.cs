using UnityEngine;
using UniRx;

public class TapIcon : MonoBehaviour {
	public int tag;
	public AudioSource audioSource;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public void playSE(){
		this.audioSource.Play ();
	}
}
