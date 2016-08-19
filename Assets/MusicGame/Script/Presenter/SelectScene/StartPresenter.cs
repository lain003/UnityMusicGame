using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class StartPresenter : MonoBehaviour {
	public ServerTrackButton trackButtonPrefab;
	public ScrollController scrollController;
	// Use this for initialization
	void Start () {
		MyServer.start_downloadTracks(myServerTracks => {
			var tracks = myServerTracks.getTracks();
			foreach(MyServerTrack track in tracks){
				this.setTrack(track);
			}
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void setTrack(MyServerTrack track){
		ServerTrackButton trackButton = Object.Instantiate(this.trackButtonPrefab) as ServerTrackButton;
		trackButton.track = track;
		trackButton.buttonText.text = track.title;
		this.scrollController.addButton (trackButton);
	}
}
