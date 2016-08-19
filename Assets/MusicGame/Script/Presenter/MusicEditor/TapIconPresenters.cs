using UnityEngine;
using System.Collections;
using UniRx;

public class TapIconPresenters : PresenterBase<BPM> {
	public TapIconPresenter[] tapIconPresenters;

	protected override IPresenter[] Children{
		get{
			return tapIconPresenters;
		}
	}

	protected override void BeforeInitialize(BPM _bpm){
		foreach (TapIconPresenter tapIconPresenter in this.tapIconPresenters) {
			tapIconPresenter.PropagateArgument (_bpm);
		}
	}

	protected override void Initialize(BPM _bpm){
	}
}
