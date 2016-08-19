using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

public class ObservablePressStateTrigger : ObservableTriggerBase, IPointerDownHandler, IPointerUpHandler {
	class Finger{
		public bool isHold;
		public readonly float raiseTime;
		public Finger(float _raiseTime){
			this.raiseTime = _raiseTime;
		}
	}

	public float IntervalSecond = 1f;

	Dictionary<int,Finger> fingers = new Dictionary<int,Finger>();
	Subject<Unit> holdStart;
	Subject<Unit> holdEnd;
	Subject<Unit> tapStart;
	Subject<Unit> tapEnd;

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
		float raiseTime = Time.realtimeSinceStartup + IntervalSecond;
		fingers [eventData.pointerId] = new Finger (raiseTime);
		if (tapStart != null) tapStart.OnNext(Unit.Default);
	}

	void Update()
	{
		foreach (Finger finger in fingers.Values) {
			if (!finger.isHold) {
				if (finger.raiseTime <= Time.realtimeSinceStartup) {
					if (holdStart != null) holdStart.OnNext(Unit.Default);
					finger.isHold = true;
				}
			}
		}
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData){
		Finger finger = fingers [eventData.pointerId];
		if (finger.isHold) {
			if (holdEnd != null) holdEnd.OnNext (Unit.Default);
		} else {
			if (tapEnd != null) tapEnd.OnNext(Unit.Default);
		}
		fingers.Remove (eventData.pointerId);
	}

	public IObservable<Unit> HoldStartAsObservable(){
		return holdStart ?? (holdStart = new Subject<Unit>());
	}
	public IObservable<Unit> HoldEndAsObservable(){
		return holdEnd ?? (holdEnd = new Subject<Unit>());
	}
	public IObservable<Unit> TapStartAsObservable(){
		return tapStart ?? (tapStart = new Subject<Unit>());
	}
	public IObservable<Unit> TapEndAsObservable(){
		return tapEnd ?? (tapEnd = new Subject<Unit>());
	}

	protected override void RaiseOnCompletedOnDestroy()
	{
		if (holdStart != null)
		{
			holdStart.OnCompleted();
		}
		if (holdEnd != null)
		{
			holdEnd.OnCompleted();
		}
		if (tapStart != null)
		{
			tapStart.OnCompleted();
		}
		if (tapEnd != null)
		{
			tapEnd.OnCompleted();
		}
	}
}