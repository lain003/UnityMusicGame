using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class HoldLinePresenter : MonoBehaviour{
	public HoldLine holdLine;
	public ObservablePressStateTrigger pressTrigger;

	[SerializeField]
	public HoldNote holdNote;
	// Use this for initialization
	void Start (){
		pressTrigger.TapStartAsObservable ().Subscribe (_ => destroyMe());
	}
	
	// Update is called once per frame
	void Update (){
	}

	private void destroyMe(){
		this.holdNote.delete ();
		Object.Destroy(this.gameObject);
	}
}