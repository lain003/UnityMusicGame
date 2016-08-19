using System;
using UniRx;

public class ScenePresenter_MusicPlay : PresenterBase {
	public BGMAudioSourcePresenter bgmAudioSourcePresenter;

	public static MyServerTrack selectedTrack;

	protected override IPresenter[] Children{
		get{
			return new IPresenter[]{this.bgmAudioSourcePresenter};
		}
	}
	protected override void BeforeInitialize(){
		TapNote.delete_all ();
		HoldNote.delete_all ();
		HoldNote.inserts(selectedTrack.holdNotes);
		TapNote.inserts(selectedTrack.tapNotes);

		this.bgmAudioSourcePresenter.PropagateArgument(selectedTrack);
	}
	protected override void Initialize(){
	}
}
