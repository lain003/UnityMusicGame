using System.Collections.Generic;
using UnityEngine;
using System;

using BaseDataBase = BaseDatabase;
public class TapDatabase : BaseDataBase{
	private const string tableName = "tapnote";
	private const string dbFileName = "musicgame.db";

	public static List<TapNote> where(string param){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		string query = "SELECT * FROM " + TapDatabase.tableName + " where " + param + ";";
		DataTable dataTable = sqlDB.ExecuteQuery(query);

		return getTapNotes(dataTable.Rows);
	}

	public static List<TapNote> where(float startTime){
		double round_startTime = MyMath.ToRoundDown (startTime, MyConfig.Play.EFFECTIVE_DIGIT);
		double next_time = Math.Pow (0.1, MyConfig.Play.EFFECTIVE_DIGIT) + round_startTime;
		return TapDatabase.where ("(start_time BETWEEN " + round_startTime + " AND " + next_time + ") AND NOT " + "start_time IN(" + next_time + ")");
	}

	public static List<TapNote> where(float startTime,int tag){
		return TapDatabase.where ("(start_time=" + startTime + ") and tag=" + tag);
	}
	public static void inserts(List<TapNote> notes){

		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		// トランザクション開始
		sqlDB.transactionStart();

		try {
			foreach (TapNote note in notes) {
				string query = "INSERT INTO " + tableName + "(tag,start_time) values('" + note.tag + "','" + note.time +  "');";
				sqlDB.ExecuteNonQueryEx (query);
			}
		} catch ( SqliteException) {
			// エラー発生時、ロールバック
			sqlDB.transactionRollBack();
		}

		// コミット
		sqlDB.transactionCommit();
	}

	public static void insert(TapNote note){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		string query = "INSERT INTO " + tableName + "(tag,start_time) values('" + note.tag + "','" + note.time + "');";
		sqlDB.ExecuteQuery (query);
	}

	public static void delete_all(){
		BaseDataBase.delete_all (dbFileName, tableName);
	}

	public static void delete(int id){
		BaseDataBase.delete (dbFileName, tableName, id);
	}

	public static List<TapNote> all(){
		return getTapNotes(BaseDataBase.all (dbFileName, tableName).Rows);
	}

	public static void printAll(){
		var tapNotes = TapDatabase.all ();
		foreach (TapNote note in tapNotes) {
			Debug.Log (note.ToString ());
		}
	}

	private static List<TapNote> getTapNotes(List<DataRow> rows){
		List<TapNote> notes = new List<TapNote>();
		foreach (DataRow dr in rows) {
			TapNote note = new TapNote ((int)dr["id"],(int)dr ["tag"], (float)(double)dr ["start_time"]);
			notes.Add (note);
		}

		return notes;
	}
}