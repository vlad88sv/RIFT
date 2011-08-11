using System;
using System.Data;
using MySql.Data.MySqlClient;
using Gtk;

public static class MySQL
{
	private static string servidor = "127.0.0.1";
	private static string MyConString = "SERVER="+servidor+";DATABASE=rift3;USER ID=root;PASSWORD=RIFT;";
	private static MySql.Data.MySqlClient.MySqlConnection MyCon = new MySql.Data.MySqlClient.MySqlConnection(MyConString);
	private static MySql.Data.MySqlClient.MySqlCommand MyCmd = new MySql.Data.MySqlClient.MySqlCommand();
	public static MySqlDataReader Reader;
	static MySQL()
	{
		MyCon.Open();
		MyCmd.Connection = MyCon;
		consultar("set lc_time_names='es_SV';");
		consultar("set names 'utf8';");
	}
	
 	public static bool consultar(string query)
	{
		double benchmark = DateTime.Now.TimeOfDay.TotalMilliseconds;
		Console.WriteLine (query);
		try {
			if (Reader != null && Reader.IsClosed == false)
				Reader.Close();
			MyCmd.CommandText = query;
			Reader = MyCmd.ExecuteReader();
		} catch (MySqlException e) {
			Console.WriteLine ("MySQL.Error :: #" + e.Number + " = " + e.Message);
			Console.WriteLine ("MySQL.Error.query :: " + query);
			return false;
		}		
		Console.WriteLine("MySQL_STAGE0:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		return true;
	}
}