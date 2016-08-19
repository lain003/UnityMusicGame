using UnityEngine;
using UniRx;

public class TapLinePresenter : MonoBehaviour
{
	public ObservablePressStateTrigger pressStateTrigger;
	public TapLine tapLine;

	[SerializeField]
	public TapNote tapNote;

	void Start (){
		this.pressStateTrigger.TapStartAsObservable ().Subscribe (_ => this.destroyMe());
	}

	void Update (){
	}

	public void destroyMe(){
		this.tapNote.delete ();
		Object.Destroy(this.gameObject);
	}
}

