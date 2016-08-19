using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;

public class HitIconPresenter : PresenterBase<int> {
	public ObservableTrigger2DTrigger trigger2dTrigger;
	public ObservablePressStateTrigger pressTrigger;
	public HitIcon hitIcon;

	private Subject<int> perfectDetect_Subject;
	public IObservable<int> perfectDetect_Observable;
	private Subject<int> failedDetect_Subject;
	public IObservable<int> failedDetect_Observable;

	private IDisposable failed_timer;
	protected override IPresenter[] Children
	{
		get{
			return EmptyChildren;
		}
	}
	protected override void BeforeInitialize(int argument)
	{
	}
	protected override void Initialize(int argument)
	{
		this.perfectDetect_Subject = new Subject<int> ();
		this.failedDetect_Subject = new Subject<int> ();
		this.perfectDetect_Observable = this.perfectDetect_Subject.AsObservable ();
		this.failedDetect_Observable = this.failedDetect_Subject.AsObservable ();
		trigger2dTrigger.OnTriggerEnter2DAsObservable ()
			.Select(col => this.as_tapline(col)).Where(tapLine => tapLine != null)
			.Subscribe (x => this.hitIcon.enter2d_tapLine(x));
		trigger2dTrigger.OnTriggerExit2DAsObservable ()
			.Select (col => this.as_tapline (col)).Where (tapLine => tapLine != null)
			.Subscribe (x => this.falied_TapLine());

		trigger2dTrigger.OnTriggerEnter2DAsObservable ()
			.Select (col => this.as_holdStartLine (col)).Where (holdLine => holdLine != null)
			.Subscribe (x => {
				x.stop_holdStart ();
				this.hitIcon.enter2d_holdLine (x);
				this.failed_timer = Observable.Timer (TimeSpan.FromSeconds (1)).Subscribe (_ => this.failed_HoldLine ());
			});

		pressTrigger.TapStartAsObservable()
			.Where(_ => this.hitIcon.contain_tapLine != null)
			.Subscribe (_ => this.perfect_TapLine());

		this.trigger2dTrigger.OnTriggerStay2DAsObservable()
			.Select (col => this.as_holdEndLine (col))
			.Where(holdLine => holdLine != null)
			.SkipUntil(pressTrigger.HoldEndAsObservable())
			.FirstOrDefault().RepeatUntilDestroy(this)
			.Subscribe(x => this.perfect_HoldEnd());
		this.trigger2dTrigger.OnTriggerExit2DAsObservable ()
			.Select (col => this.as_holdEndLine (col))
			.Where (holdLine => holdLine != null)
			.Subscribe (x => this.failed_HoldLine ());
	}

	private void tapTapLine(){
		this.hitIcon.tapStart();
		if(this.failed_timer != null){
			this.failed_timer.Dispose();
		}
	}

	void Update () {
	}

	private TapLine_Play as_tapline(Collider2D col){
		return col.gameObject.GetComponent(typeof(TapLine_Play)) as TapLine_Play;
	}

	private HoldLine_Play as_holdStartLine(Collider2D col){
		HoldStart_Play holdStart = col.gameObject.GetComponent (typeof(HoldStart_Play)) as HoldStart_Play;
		if (holdStart == null) {
			return null;
		}

		return holdStart.transform.parent.gameObject.GetComponent(typeof(HoldLine_Play)) as HoldLine_Play;
	}

	private HoldLine_Play as_holdEndLine(Collider2D col){
		HoldEnd_Play holdEnd = col.gameObject.GetComponent (typeof(HoldEnd_Play)) as HoldEnd_Play;
		if (holdEnd == null) {
			return null;
		}

		return holdEnd.transform.parent.gameObject.GetComponent(typeof(HoldLine_Play)) as HoldLine_Play;
	}

	private void failed_HoldLine(){
		this.hitIcon.destroy_containHold (false);
		this.failedDetect_Subject.OnNext (0);
	}

	private void perfect_HoldEnd(){
		this.hitIcon.destroy_containHold (true);
		this.perfectDetect_Subject.OnNext (0);
	}

	private void falied_TapLine(){
		this.hitIcon.destroy_containTap (false);
		this.failedDetect_Subject.OnNext (0);
	}

	private void perfect_TapLine(){
		this.perfectDetect_Subject.OnNext (0);
		this.hitIcon.tapStart();
		if(this.failed_timer != null){
			this.failed_timer.Dispose();
		}
	}
}
