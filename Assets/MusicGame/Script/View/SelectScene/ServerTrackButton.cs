using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class ServerTrackButton : MonoBehaviour {
	public Button button;
	public Text buttonText;

	[HideInInspector]
	public MyServerTrack track;
	// Use this for initialization
	void Start () {
		button.onClick.AsObservable ().Subscribe (_ => {
			ScenePresenter.selectedTrack = track;
			Application.LoadLevel("MusicEditor");
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
