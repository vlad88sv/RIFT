using System;
public static class Historial
{
	//static MySQL DB_Historial = new MySQL();
	public static void Registrar(string historial)
	{		
		MySQL.consultar("INSERT INTO historial (fecha, ID_usuario, historial) VALUES(NOW(),'" + auth.ID_usuario + "', '" + historial + "')");	
	}	

	public static void Transaccion(string transaccion)
	{
		MySQL.consultar("INSERT INTO transacciones (fecha, ID_usuario, tipo) VALUES(NOW(),'" + auth.ID_usuario + "', '" + transaccion + "')");
	}
}