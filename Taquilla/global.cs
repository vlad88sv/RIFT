using System;
using Gtk;
public static class global
{
	public const int maximo_jugadores = 16;
	public static string fechaDiaTrabajoFMySQL = "";
	public static string fechaDiaTrabajoMasJuegoFMySQL = "";
	public static string fechaDiaTrabajoMasJuego = "";
	public static string fechaDiaTrabajo = "";
	public static string HoraJuego = "";
	public static string HoraJuegoFMySQL = "";
	public static int diaNumero = 0;
	public static string Version = "4.5 - Sintigo";

	public static void ReportarError(string Titulo, string Mensaje)
	{
		
	}
	
	// Verificar si CantidadJugadores caben dentro de FechaHoraInicio y FechaHoraFinal
	public static bool EsHorarioDisponible(DateTime FechaHoraInicio, DateTime FechaHoraFinal, int CantidadJugadores)
	{
		string SQLFechaHoraInicio = FechaHoraInicio.ToString("yyyy-MM-dd HH:mm:00");
		string SQLFechaHoraFinal = FechaHoraFinal.ToString("yyyy-MM-dd HH:mm:00");
		string c = "SELECT COUNT(*) AS CantidadTiquetes FROM tickets WHERE fecha_juego BETWEEN '" + SQLFechaHoraInicio + "' AND '" + SQLFechaHoraFinal;
		MySQL.consultar(c);
		MySQL.Reader.Read();
		if (MySQL.Reader["CantidadTiquetes"].ToString() != "0")
			return false;
		
		c = "SELECT COUNT(*) AS CantidadEventos FROM eventos WHERE fecha_evento=DATE('" + SQLFechaHoraInicio + "') AND TIME('" + SQLFechaHoraInicio + "') < hora_final AND AND TIME('" + SQLFechaHoraFinal + "') > hora_inicio";
		MySQL.Reader.Read();
		if (MySQL.Reader["CantidadTiquetes"].ToString() != "0")
			return false;
		
		return true;
	}
}
