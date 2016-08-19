using UnityEngine;
using System.Collections;

public class NoteSheet : MonoBehaviour {
	public TapLinePresenter tapLinePresenterPrefab;
	public HoldLinePresenter holdLinePresenterPrefab;

	public void drawAllNote(AllNote allNote){
		foreach (TapNote tapNote in allNote.tapNotes) {
			float y = this.reckon_linePosition(tapNote.time);
			TapLinePresenter tapLine = (TapLinePresenter)Instantiate (tapLinePresenterPrefab);
			tapLine.transform.SetParent (this.transform, false);
			tapLine.transform.localPosition = new Vector2 (0, y);
			tapLine.tapNote = tapNote;
		}
		foreach (HoldNote holdNote in allNote.holdNotes) {
			float start_y = this.reckon_linePosition(holdNote.start_time);
			float end_y = this.reckon_linePosition(holdNote.end_time);
			HoldLinePresenter holdLine = (HoldLinePresenter)Instantiate (holdLinePresenterPrefab);
			holdLine.transform.SetParent (this.transform, false);
			holdLine.holdLine.initialize (start_y, end_y);
			holdLine.holdNote = holdNote;
		}
	}
	public TapLinePresenter drowTapLine(float startTime){
		Vector2 position = new Vector2 (0, this.reckon_linePosition(startTime));
		TapLinePresenter tapLine = (TapLinePresenter)Instantiate (tapLinePresenterPrefab);
		tapLine.transform.SetParent (this.transform, false);
		tapLine.transform.localPosition = position;
		tapLine.tapLine.on_justmade ();
		return tapLine;
	}

	public HoldLinePresenter drowHoldLine(float start_musicTime,float end_musicTime){
		HoldLinePresenter holdLine = (HoldLinePresenter)Instantiate (this.holdLinePresenterPrefab);
		holdLine.transform.SetParent (this.transform, false);
		Vector2 startPosition = new Vector2 (0, this.reckon_linePosition(start_musicTime));
		Vector2 endPosition = new Vector2 (0, this.reckon_linePosition(end_musicTime));
		holdLine.holdLine.initialize (startPosition.y, endPosition.y,true);
		return holdLine;
	}

	private float reckon_linePosition(float musicTime){
		return musicTime * MyConfig.Editor.NOTE_SPEED;
	}

	public void deleteAllChild(){
		foreach ( Transform child in this.transform ){
			Object.Destroy(child.gameObject);
		}
	}
}
