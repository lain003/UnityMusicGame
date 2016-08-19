using UnityEngine;
using UniRx;

public class PlayModeButtonPresenter : PresenterBase<MyServerTrack> {
	public PlayModeButton playModeButton;

	private MyServerTrack myServerTrack;

	protected override IPresenter[] Children{
		get{
			return EmptyChildren;
		}
	}
	protected override void BeforeInitialize(MyServerTrack _myServerTrack){
		this.myServerTrack = _myServerTrack;
	}
	protected override void Initialize(MyServerTrack _myServerTrack){
	}

	void Start () {
		base.Start();
		this.playModeButton.button.onClick.AsObservable ().Subscribe (_ => this.load_scene());
	}

	private void load_scene(){
		ScenePresenter_MusicPlay.selectedTrack = myServerTrack;
		Application.LoadLevel("MusicPlay");
	}
}
