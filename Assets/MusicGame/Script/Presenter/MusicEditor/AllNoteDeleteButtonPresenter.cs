using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
public class AllNoteDeleteButtonPresenter : MonoBehaviour {
	public Button button;

	public NoteSheetsPresenter noteSheetsPresenter;
	// Use this for initialization
	void Start () {
		button.onClick.AsObservable ().Subscribe (_ => {
			TapNote.delete_all();
			HoldNote.delete_all();

			this.noteSheetsPresenter.redrawNote_fromDB();
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
