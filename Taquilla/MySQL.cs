using System;
using System.Data;
using MySql.Data.MySqlClient;
using Gtk;

public static class MySQL
{
	private static string servidor = "127.0.0.1";
	private static string MyConString = "SERVER="+servidor+";DATABASE=rift3;USER ID=root;PASSWORD=RIFT;CHARACTER SET=utf8;";
	private static MySql.Data.MySqlClient.MySqlConnection MyCon = new MySql.Data.MySqlClient.MySqlConnection(MyConString);
	private static MySql.Data.MySqlClient.MySqlCommand MyCmd = new MySql.Data.MySqlClient.MySqlCommand();
	public static MySqlDataReader Reader;
	static MySQL()
	{
		IniciarConexionMySQL();
	}
	
	private static void IniciarConexionMySQL()
	{
		if (MyCon.State == ConnectionState.Open)
			return;
		MyCon.Close();
		MyCon.Open();
		MyCmd.Connection = MyCon;
		consultar("SET LC_TIME_NAMES='es_SV'; SET NAMES UTF8; SET CHARACTER SET UTF8;");
	}
	
 	public static bool consultar(string query)
	{
		IniciarConexionMySQL();
		double benchmark = DateTime.Now.TimeOfDay.TotalMilliseconds;
		Console.WriteLine (query);
		try {
			if (Reader != null && Reader.IsClosed == false)
				Reader.Close();
			MyCmd.CommandText = query;
			Reader = MyCmd.ExecuteReader();
		} catch (MySqlException e) {
			Console.WriteLine ("MySQL.Error :: #" + e.Number + " = " + e.Message);
			Console.WriteLine ("MySQL.Error.Query :: " + query);
			return false;
		}
		Console.WriteLine("MySQL.Benchmark :: " + (DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		return true;
	}
}