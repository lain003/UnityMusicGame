using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class TapNote : Note{
	public float time;

	public TapNote (int _id,int _tag, float _time):base(_id,_tag){
		time = _time;
	}

	public TapNote (int _tag, float _time):base(_tag){
		time = _time;
	}

	public TapNote(Dictionary<string,object> dic):base(dic){
		time = Convert.ToSingle (dic ["time"]);
	}

	public static List<TapNote> where(string param){
		return TapDatabase.where (param);
	}
	public static void inserts(List<TapNote> tapnotes){
		TapDatabase.inserts (tapnotes);
	}

	public static  List<TapNote> convertToTapNotes(List<object> importList){
		List<TapNote> tapNotes = new List<TapNote>();
		if (tapNotes != null) {
			foreach (Dictionary<string,object> dic in importList) {
				TapNote note = new TapNote(dic);
				tapNotes.Add(note);
			}
		}

		return tapNotes;
	}

	public override void save(){
		TapDatabase.insert (this);

		List<TapNote> tapnotes = TapDatabase.where (this.time,this.tag);
		this.id = tapnotes [0].id;
	}

	public static void delete_all(){
		TapDatabase.delete_all();
	}

	public void delete(){
		TapDatabase.delete (this.id);
	}
	
	public override string ToString(){
		return base.ToString () + "time= " + time;
	}

	public override Dictionary<string,string> toDictionary(){
		Dictionary<string,string> dic = base.toDictionary ();
		dic["time"] = this.time.ToString();
		
		return dic;
	}
}