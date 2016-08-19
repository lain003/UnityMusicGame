using UnityEngine;
using System.Collections;
using UniRx;
public class NoteSheetPresenter : PresenterBase<AllNote> {
	public int tag;
	public NoteSheet noteSheet;

	protected override IPresenter[] Children{
		get{
			return EmptyChildren;
		}
	}

	protected override void BeforeInitialize(AllNote allNote){
	}

	protected override void Initialize(AllNote allNote){
		this.noteSheet.drawAllNote (allNote);
	}

	public TapLinePresenter drowTapLine(float musicTime){
		return noteSheet.drowTapLine (musicTime);
	}

	public HoldLinePresenter drowHoldLine(float start_musicTime,float now_musicTime){
		return noteSheet.drowHoldLine (start_musicTime, now_musicTime);
	}
}