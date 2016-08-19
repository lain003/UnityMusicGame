using UniRx;

public class ScenePresenter : PresenterBase {
	public NoteSheetsPresenter noteSheetsPresenter;
	public BGMAudioSourcePresenter bgmAudioSourcePresenter;
	public UploadButtonPresenter uploadButtonPresenter;
	public PlayModeButtonPresenter playModeButtonPresenter;

	public TapIconPresenters tapIconPresenters;
	public BPMPositioningPresenter bpmPositioningPresenter;
	public BPMSheetPresenter bpmSheetPresenter;
	public BPMValueInputPresenter bpmValueInputPresenter;

	public static MyServerTrack selectedTrack;

	protected override IPresenter[] Children{
		get{
			return new IPresenter[]{this.noteSheetsPresenter,this.bgmAudioSourcePresenter,this.uploadButtonPresenter,this.playModeButtonPresenter,
				this.tapIconPresenters,this.bpmPositioningPresenter,this.bpmSheetPresenter,this.bpmValueInputPresenter};
		}
	}

	protected override void BeforeInitialize(){
		BPM bpm = new BPM (selectedTrack.bpm, selectedTrack.position);

		this.noteSheetsPresenter.PropagateArgument(selectedTrack);
		this.bgmAudioSourcePresenter.PropagateArgument(selectedTrack);
		this.playModeButtonPresenter.PropagateArgument (selectedTrack);

		this.tapIconPresenters.PropagateArgument (bpm);
		this.bpmPositioningPresenter.PropagateArgument (bpm);
		this.bpmSheetPresenter.PropagateArgument (bpm);
		this.bpmValueInputPresenter.PropagateArgument (bpm);

		this.uploadButtonPresenter.PropagateArgument (new TrackAndBpm(bpm, selectedTrack));
	}

	protected override void Initialize(){
	}
}

//Presenterの引数は一つしか指定できないので、構造体で渡す
public struct TrackAndBpm{
	public BPM bpm;
	public MyServerTrack track;

	public TrackAndBpm(BPM p1, MyServerTrack p2)
	{
		bpm = p1;
		track = p2;
	}
}