using System; // require keep for Windows Universal App
using UnityEngine;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;

[DisallowMultipleComponent]
public class ObservableTrigger2DTriggerCustom : ObservableTriggerBase {
	Subject<Collider2D> onTriggerEnter2D;
	[Name(typeof(TagName))] public List<string> target_tags;

	/// <summary>Sent when another object enters a trigger collider attached to this object (2D physics only).</summary>
	void OnTriggerEnter2D(Collider2D other)
	{
		if (!(this.check_containTag (other.tag))) {
			return;
		}
		if (onTriggerEnter2D != null) onTriggerEnter2D.OnNext(other);
	}

	/// <summary>Sent when another object enters a trigger collider attached to this object (2D physics only).</summary>
	public IObservable<Collider2D> OnTriggerEnter2DAsObservable()
	{
		return onTriggerEnter2D ?? (onTriggerEnter2D = new Subject<Collider2D>());
	}

	Subject<Collider2D> onTriggerExit2D;

	/// <summary>Sent when another object leaves a trigger collider attached to this object (2D physics only).</summary>
	void OnTriggerExit2D(Collider2D other)
	{
		if (!(this.check_containTag (other.tag))) {
			return;
		}

		if (onTriggerExit2D != null) onTriggerExit2D.OnNext(other);
	}

	/// <summary>Sent when another object leaves a trigger collider attached to this object (2D physics only).</summary>
	public IObservable<Collider2D> OnTriggerExit2DAsObservable()
	{
		return onTriggerExit2D ?? (onTriggerExit2D = new Subject<Collider2D>());
	}

	Subject<Collider2D> onTriggerStay2D;

	/// <summary>Sent each frame where another object is within a trigger collider attached to this object (2D physics only).</summary>
	void OnTriggerStay2D(Collider2D other)
	{
		if (!(this.check_containTag (other.tag))) {
			return;
		}

		if (onTriggerStay2D != null) onTriggerStay2D.OnNext(other);
	}

	/// <summary>Sent each frame where another object is within a trigger collider attached to this object (2D physics only).</summary>
	public IObservable<Collider2D> OnTriggerStay2DAsObservable()
	{
		return onTriggerStay2D ?? (onTriggerStay2D = new Subject<Collider2D>());
	}

	protected override void RaiseOnCompletedOnDestroy()
	{
		if (onTriggerEnter2D != null)
		{
			onTriggerEnter2D.OnCompleted();
		}
		if (onTriggerExit2D != null)
		{
			onTriggerExit2D.OnCompleted();
		}
		if (onTriggerStay2D != null)
		{
			onTriggerStay2D.OnCompleted();
		}
	}

	private bool check_containTag(string tagName){
		return target_tags.Contains (tagName);
	}
}
