using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class HoldNote : Note{
	public float start_time;
	public float end_time;

	public HoldNote(int _tag,float _start_time,float _end_time):base(_tag){
		start_time = _start_time;
		end_time = _end_time;
	}

	public HoldNote(int _id,int _tag,float _start_time,float _end_time):base(_id,_tag){
		start_time = _start_time;
		end_time = _end_time;
	}

	public HoldNote(Dictionary<string,object> dic):base(dic){
		start_time = Convert.ToSingle (dic ["start_time"]);
		end_time = Convert.ToSingle (dic ["end_time"]);
	}

	public static List<HoldNote> where(string param){
		return HoldDatabase.where (param);
	}
	public static void inserts(List<HoldNote> notes){
		HoldDatabase.inserts (notes);
	}

	public override void save(){
		HoldDatabase.insert (this);
		List<HoldNote> notes =  HoldDatabase.where (start_time, end_time, tag);
		this.id = notes [0].id;
	}

	public static  List<HoldNote> convertToTapNotes(List<object> importList){
		List<HoldNote> notes = new List<HoldNote>();

		if (importList != null) {
			foreach (Dictionary<string,object> dic in importList) {
				HoldNote note = new HoldNote(dic);
				notes.Add (note);
			}
		}

		return notes;
	}

	public static void delete_all(){
		HoldDatabase.delete_all();
	}

	public void delete(){
		HoldDatabase.delete (this.id);
	}

	public override string ToString()
	{
		return base.ToString() + "start_time= " + start_time + " end_time= " + end_time;
	}
	
	public override Dictionary<string,string> toDictionary(){
		Dictionary<string,string> dic = base.toDictionary ();
		dic["start_time"] = this.start_time.ToString();
		dic["end_time"] = this.end_time.ToString();
		
		return dic;
	}
}
