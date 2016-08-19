using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BPMValueInputPresenter : PresenterBase<BPM> {
	public InputField inputField;
	private BPM bpm;
	// Use this for initialization
	void Start () {
	}

	protected override IPresenter[] Children{
		get{
			return EmptyChildren;
		}
	}

	protected override void BeforeInitialize(BPM _bpm){
	}

	protected override void Initialize(BPM _bpm){
		bpm = _bpm;

		inputField.OnEndEditAsObservable ()
			.DistinctUntilChanged()
			.Select(x => string.IsNullOrEmpty(x) ? "30" : x)
			.Select (x => Mathf.Clamp(int.Parse(x), 30, 300))
			.Subscribe(x => this.bpm.value.Value = x);
		bpm.value.Subscribe (x => this.inputField.text = x.ToString());
	}
	// Update is called once per frame
	void Update () {
	}
}
