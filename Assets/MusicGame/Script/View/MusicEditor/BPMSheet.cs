using UnityEngine;
using System.Collections.Generic;

public class BPMSheet : MonoBehaviour {
	public DotLine dotLine_prefab;

	private List<DotLine> dotLines = new List<DotLine> ();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void draw_dotLines(float bpm_interval, float musicLength){
		foreach ( Transform child in this.transform ){
			Object.Destroy(child.gameObject);
		}
		int dotLineCount = (int)(musicLength / bpm_interval * 1.1);//念のために10%多めに作っておく
		for (int i = 1; i <= dotLineCount; i++) {
			float distance = this.reckon_linePosition (bpm_interval * i);
			DotLine dotLine = (DotLine)Instantiate (this.dotLine_prefab);
			dotLine.transform.SetParent (this.transform, false);
			dotLine.transform.localPosition = new Vector3 (0, distance, this.transform.position.z);
		}
	}

	private float reckon_linePosition(float musicTime){
		return musicTime * MyConfig.Editor.NOTE_SPEED;
	}
}
