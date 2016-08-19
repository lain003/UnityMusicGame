using System.Collections.Generic;
using UnityEngine;

using BaseDataBase = BaseDatabase;
public class HoldDatabase : BaseDataBase{
	private const string tableName = "holdnote";
	private const string dbFileName = "musicgame.db";

	public static List<HoldNote> where(string param){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		string query = "SELECT * FROM " + HoldDatabase.tableName + " where " + param + ";";
		DataTable dataTable = sqlDB.ExecuteQuery(query);
		return getHoldNotes (dataTable.Rows);
	}

	public static List<HoldNote> where(float startTime,float endTime, int tag){
		return HoldDatabase.where ("start_time=" + startTime + " and end_time=" + endTime + " and tag=" + tag);
	}

	public static List<HoldNote> where_startTime(float time){
		return HoldDatabase.where ("start_time=" + time);
	}

	public static List<HoldNote> where_endTime(float time){
		return HoldDatabase.where ("end_time=" + time);
	}

	public static void inserts(List<HoldNote> notes){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		// トランザクション開始
		sqlDB.transactionStart();

		try {
			foreach (HoldNote note in notes) {
				string query = "INSERT INTO " + tableName + "(tag,start_time,end_time) values('" + note.tag + "','" + note.start_time +  "','" + note.end_time + "');";
				sqlDB.ExecuteNonQueryEx (query);
			}
		} catch ( SqliteException ex) {
			// エラー発生時、ロールバック
			sqlDB.transactionRollBack();    
		}

		// コミット
		sqlDB.transactionCommit();
	}

	public static void insert(HoldNote note){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		string query = "INSERT INTO " + tableName + "(tag,start_time,end_time) values('" + note.tag + "','" + note.start_time + "','" + note.end_time + "');";
		sqlDB.ExecuteQuery (query);
	}

	public static void delete_all(){
		BaseDataBase.delete_all (dbFileName, tableName);
	}

	public static void delete(int id){
		BaseDataBase.delete (dbFileName, tableName, id);
	}

	public static List<HoldNote> all(){
		return getHoldNotes(BaseDataBase.all (dbFileName, tableName).Rows);
	}

	private static List<HoldNote> getHoldNotes(List<DataRow> rows){
		List<HoldNote> notes = new List<HoldNote>();
		foreach (DataRow dr in rows) {
			HoldNote note = new HoldNote ((int)dr["id"], (int)dr ["tag"], (float)(double)dr ["start_time"], (float)(double)dr ["end_time"]);
			notes.Add (note);
		}

		return notes;
	}
}
