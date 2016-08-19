using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public abstract class Note{
	public int tag;
	public int id;

	protected static int serial_count;

	public Note(int _id,int _tag){
		this.id = _id;
		tag = _tag;
	}

	public Note(int _tag){
		tag = _tag;
	}

	public Note(Dictionary<string,object> dic){
		tag = Convert.ToInt16 (dic ["tag"]);
	}

	public abstract void save();

	public override string ToString()
	{
		return "Note:" + "id=" + this.id + " tag=" + tag + " ";
	}

	virtual public Dictionary<string,string> toDictionary(){
		Dictionary<string,string> dic = new Dictionary<string, string>();
		dic["tag"] = this.tag.ToString();
		return dic;
	}
}
