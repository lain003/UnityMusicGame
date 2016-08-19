using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class HitIconsPresenter : PresenterBase<int> {
	public List<HitIconPresenter> hitIconPresenters;

	public IObservable<int> allPerfectDetect_Observavle;
	public IObservable<int> allfailedDetect_Observable;
	// Use this for initialization
	void Start () {
	}

	protected override IPresenter[] Children
	{
		get{
			return this.hitIconPresenters.ToArray();
		}
	}

	protected override void BeforeInitialize(int argument)
	{
	}

	protected override void Initialize(int argument){
		this.allPerfectDetect_Observavle = Observable.Merge (hitIconPresenters.Select (x => x.perfectDetect_Observable));
		this.allfailedDetect_Observable = Observable.Merge (hitIconPresenters.Select (x => x.failedDetect_Observable));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
