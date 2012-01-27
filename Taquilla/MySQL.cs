using System;
using System.Data;
using MySql.Data.MySqlClient;
using Gtk;

public static class MySQL
{
	private static string servidor = "127.0.0.1";
	private static string MyConString = "SERVER="+servidor+";DATABASE=rift3;USER ID=root;PASSWORD=RIFT;CHARACTER SET=utf8;Treat Tiny As Boolean=False;";
	private static MySql.Data.MySqlClient.MySqlConnection MyCon = new MySql.Data.MySqlClient.MySqlConnection(MyConString);
	private static MySql.Data.MySqlClient.MySqlCommand MyCmd = new MySql.Data.MySqlClient.MySqlCommand();
	public static MySqlDataReader Reader;
	
	private static bool IniciarConexionMySQL()
	{
		
		if (Reader != null)
		{
			Console.WriteLine("MySQL.IniciarConexion :: Reader.Close");
			Reader.Close();
		}
		
		/*
		if (Reader != null)
		Console.WriteLine("MySQL.IniciarConexion :: Reader.IsClosed : " + Reader.IsClosed);
		Console.WriteLine("MySQL.IniciarConexion :: MyCon.Ping : " + MyCon.Ping());
		Console.WriteLine("MySQL.IniciarConexion :: MyCon.State : " + ConnectionState.Open);
		*/
		
		if (MyCon.Ping() == true && MyCon.State == ConnectionState.Open)
		{
			Console.WriteLine("MySQL.IniciarConexion :: <--- MYSQL LISTO --->");
			return true;
		}
	
		Console.WriteLine("MySQL.IniciarConexion :: --- INICIANDO MYSQL ---");
		
		try {
			if (MyCon.State != ConnectionState.Open)
				MyCon.Open();
			MyCmd.Connection = MyCon;
		} catch (MySqlException e) {
			Console.WriteLine ("MySQL.Error :: #" + e.Number + " = " + e.Message);
			return false;
		}
	
		Console.WriteLine("MySQL.IniciarConexion :: --- CONFIGURANDO MYSQL --->");
		
		consultar("SET LC_TIME_NAMES='es_SV'; SET NAMES UTF8; SET CHARACTER SET UTF8;");
		Reader.Close();
		
		Console.WriteLine("MySQL.IniciarConexion :: <--- MYSQL CONFIGURADO ---");
		return true;
	}
	
 	public static bool consultar(string query)
	{
		double benchmark = DateTime.Now.TimeOfDay.TotalMilliseconds;
		Console.WriteLine ("MySQL.Consultar :: =================== + INICIO + ===============================");
		Console.WriteLine ("MySQL.Consultar.Query :: " + query);
		
		if (!IniciarConexionMySQL())
			return false;
		
		try {
			MyCmd.CommandText = query;
			Reader = MyCmd.ExecuteReader();
		} catch (MySqlException e) {
			Console.WriteLine ("MySQL.Error :: #" + e.Number + " = " + e.Message);
			Console.WriteLine ("MySQL.Error.Query :: " + query);
			return false;
		}
		Console.WriteLine("MySQL.Benchmark :: " + (DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		Console.WriteLine ("MySQL.Consultar :: ===================== + FIN + =============================");
		return true;
	}
}