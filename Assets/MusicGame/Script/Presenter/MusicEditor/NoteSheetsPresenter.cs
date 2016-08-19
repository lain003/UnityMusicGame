using UniRx;
using System.Collections.Generic;

public class NoteSheetsPresenter : PresenterBase<MyServerTrack> {
	public NoteSheetPresenter[] noteSheetPresenters;

	protected override IPresenter[] Children{
		get{
			return this.noteSheetPresenters;
		}
	}
	protected override void BeforeInitialize(MyServerTrack myServerTrack){
		TapNote.delete_all ();
		HoldNote.delete_all ();
		HoldNote.inserts(myServerTrack.holdNotes);
		TapNote.inserts(myServerTrack.tapNotes);
		this.redrawNote_fromDB ();
	}
	protected override void Initialize(MyServerTrack myServerTrack){
	}

	public void redrawNote_fromDB(){
		foreach(NoteSheetPresenter noteSheetPresenter in this.noteSheetPresenters){
			noteSheetPresenter.noteSheet.deleteAllChild ();

			List<TapNote> tapNotes = TapNote.where("tag=" + noteSheetPresenter.tag);
			List<HoldNote> holdNotes = HoldNote.where("tag=" + noteSheetPresenter.tag);
			AllNote allNote = new AllNote(tapNotes,holdNotes);
			noteSheetPresenter.PropagateArgument(allNote);
		}
	}
}
