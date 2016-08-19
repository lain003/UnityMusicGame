using System.Collections.Generic;

public abstract class BaseDatabase{
	public static void delete_all(string dbFileName,string tableName){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		string query = "DELETE FROM " + tableName + ";";
		sqlDB.ExecuteNonQuery (query);
		string query2 = "DELETE FROM sqlite_sequence WHERE name='" + tableName + "'";
		sqlDB.ExecuteNonQuery (query2);
	}

	public static DataTable all(string dbFileName,string tableName){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		string query = "SELECT * FROM " + tableName;
		return sqlDB.ExecuteQuery(query);
	}

	public static void delete(string dbFileName,string tableName,int id){
		SqliteDatabase sqlDB = new SqliteDatabase(dbFileName);
		string query = "DELETE FROM " + tableName + " WHERE id = " + id;
		sqlDB.ExecuteQuery (query);
	}
}