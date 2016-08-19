using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections;

public class BPMPositioningPresenter : PresenterBase<BPM> {
	public InputField inputField;
	public BPMSheetPresenter bpmSheetPresenter;

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

		inputField.OnEndEditAsObservable ()
			.DistinctUntilChanged()
			.Select(x => string.IsNullOrEmpty(x) ? "0" : x)
			.Select (x => Mathf.Clamp(int.Parse(x), 0, 99))
			.Subscribe(x => this.valueChange(x));
		bpm.positioning.Subscribe (x => this.inputField.text = x.ToString());
	}

	// Update is called once per frame
	void Update () {
	}

	public void valueChange(int percentage){
		this.bpm.positioning.Value = percentage;
	}

	private float reckon_linePosition(float musicTime){
		return musicTime * MyConfig.Editor.NOTE_SPEED;
	}
}
