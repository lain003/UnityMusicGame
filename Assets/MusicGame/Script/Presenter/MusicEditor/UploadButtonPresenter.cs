using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UniRx;

public class UploadButtonPresenter : PresenterBase<TrackAndBpm> {
	public UploadButton uploadButton;

	private MyServerTrack myServerTrack;
	private BPM bpm;

	protected override IPresenter[] Children{
		get{
			return EmptyChildren;
		}
	}

	protected override void BeforeInitialize(TrackAndBpm trackAndBpm){
		this.myServerTrack = trackAndBpm.track;
		this.bpm = trackAndBpm.bpm;
	}

	protected override void Initialize(TrackAndBpm trackAndBpm){
	}

	void Start () {
		this.uploadButton.button.onClick.AsObservable ().Subscribe (_ => this.upload_allNote_ToServer());
	}

	private void upload_allNote_ToServer(){
		MyServerTrack upLoadTrack = new MyServerTrack (myServerTrack, bpm, TapDatabase.all (), HoldDatabase.all ());
		MyServer.start_updateTrack (JsonUtility.ToJson(upLoadTrack), this.myServerTrack.server_id);
	}
}
