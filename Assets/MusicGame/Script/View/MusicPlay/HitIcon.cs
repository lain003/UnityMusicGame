using UnityEngine;
using UniRx.Triggers;

public class HitIcon : MonoBehaviour {
	public int tag;
	public AudioSource audioSource;
	public GameObject tapEffect;
	[HideInInspector]
	public TapLine_Play contain_tapLine;
	[HideInInspector]
	public HoldLine_Play contain_holdLine;

	void Start () {
	}

	void Update () {
	}

	public void playSE(){
		this.audioSource.Play ();
	}

	public void enter2d_tapLine(TapLine_Play tapLine){
		this.contain_tapLine = tapLine;
	}

	public void enter2d_holdLine(HoldLine_Play holdLine){
		this.contain_holdLine = holdLine;
	}

	public void tapStart(){
		if (this.contain_tapLine != null) {
			this.taped_tapLine ();
		}
	}

	public void destroy_containTap(bool is_sound){
		Destroy (this.contain_tapLine.gameObject);
		this.contain_tapLine = null;
		if (is_sound) {
			this.audioSource.Play ();
		}
	}

	public void destroy_containHold(bool is_sound){
		Destroy (this.contain_holdLine.gameObject);
		this.contain_holdLine = null;
		if (is_sound) {
			this.audioSource.Play ();
		}
	}

	private void taped_tapLine(){
		Destroy (this.contain_tapLine.gameObject);
		this.contain_tapLine = null;
		this.audioSource.Play ();

		GameObject ani = (GameObject)Instantiate (tapEffect, Vector3.zero, Quaternion.identity);
		ani.transform.SetParent (this.transform, false);
	}
}
