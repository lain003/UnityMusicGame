using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class ComboTextPresenter : PresenterBase<int> {
	public Text text;
	public ReactiveProperty<int> combo;
	public HitIconsPresenter hitIconsPresenter;
	protected override IPresenter[] Children
	{
		get{
			return new IPresenter[] { hitIconsPresenter };
		}
	}

	protected override void BeforeInitialize(int argument)
	{
	}

	protected override void Initialize(int argument)
	{
		this.combo = new ReactiveProperty<int>(0);
		this.hitIconsPresenter.allfailedDetect_Observable.Subscribe (_ => {
			combo.Value = 0;
		});
		this.hitIconsPresenter.allPerfectDetect_Observavle.Subscribe (_ => {
			combo.Value += 1;
		});

		this.combo.Subscribe (x => this.text.text = combo.ToString ());
	}
	// Update is called once per frame
	void Update () {
	
	}
}
