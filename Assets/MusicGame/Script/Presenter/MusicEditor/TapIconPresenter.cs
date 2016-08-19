using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
public class TapIconPresenter : PresenterBase<BPM> {
	public TapIcon tapIcon;
	public TapLine tapLinePrefab;
	public HoldLine holdLinePrefab;
	public NoteSheetPresenter noteSheetPresenter;
	public ObservablePressStateTrigger pressTrigger;
	public BGMAudioSource_Editor bgmAudioSource;

	public ObservableTrigger2DTriggerCustom centerCollider_trigger;
	public Toggle deleteMode_toggle;

	private TapLinePresenter before_tapLine;
	private HoldLinePresenter drowing_holdLine;
	private BPM bpm;

	protected override IPresenter[] Children{
		get{
			return EmptyChildren;
		}
	}

	protected override void BeforeInitialize(BPM _bpm){
	}

	protected override void Initialize(BPM _bpm){
		this.bpm = _bpm;

		pressTrigger.TapStartAsObservable().Subscribe (_ => drowTapLine());
		pressTrigger.TapEndAsObservable().Subscribe(_ => this.registDB_tapLine());
		pressTrigger.HoldStartAsObservable ().Subscribe (_ => drowHoldLine ());
		pressTrigger.HoldEndAsObservable ().Subscribe (_ => registDB_holdLine ());

		var tapLineColliderStream = centerCollider_trigger.OnTriggerEnter2DAsObservable ()
			.Select (col => this.as_taplinePresenter (col))
			.Where (tapLinePre => tapLinePre != null)
			.Where(tapLinePre => tapLinePre.tapLine.is_justmade != true)
			.Publish ().RefCount ();
		
		tapLineColliderStream.Where(_ => !deleteMode_toggle.isOn).Subscribe (_ => tapIcon.playSE ());
		tapLineColliderStream.Where (_ => deleteMode_toggle.isOn).Subscribe (tapLinePre => tapLinePre.destroyMe ());
	}

	void Update () {
	}

	private void drowTapLine(){
		TapNote tapNote = new TapNote (tapIcon.tag, this.calculate_bpmMusicTime());
		before_tapLine = noteSheetPresenter.drowTapLine (tapNote.time);
		before_tapLine.tapNote = tapNote;

		tapIcon.playSE ();
	}

	private void drowHoldLine(){
		drowing_holdLine = noteSheetPresenter.drowHoldLine (before_tapLine.tapNote.time, this.calculate_bpmMusicTime());
	}

	private void registDB_tapLine(){
		TapNote note = before_tapLine.tapNote;
		note.save ();
	}

	private void registDB_holdLine(){
		this.drowing_holdLine.holdLine.holdEndLine.end_keepPosition ();

		HoldNote holdNote = new HoldNote (tapIcon.tag, before_tapLine.tapNote.time, this.calculate_bpmMusicTime());
		holdNote.save ();
		this.drowing_holdLine.holdNote = holdNote;
	}

	private TapLinePresenter as_taplinePresenter(Collider2D col){
		return col.gameObject.transform.parent.gameObject.GetComponent(typeof(TapLinePresenter)) as TapLinePresenter;
	}

	private float calculate_bpmMusicTime(){
		return this.bpm.caluculate_nearTime (bgmAudioSource.aboutMusicTime.Value);
	}
}
