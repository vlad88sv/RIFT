using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public static class RIFTnetwork
{	
	public static void iniciar () {enviarPaquete("T2_CONTROL_CENTRE,GAME_START\n");}
	public static void abortar () {enviarPaquete("T2_CONTROL_CENTRE,GAME_ABORT\n");}
	public static void finalizar () {enviarPaquete("T2_CONTROL_CENTRE,GAME_FINISH\n");}
	
	private static void enviarPaquete (string Paquete)
	{
		try
		{
			Socket sock = new TcpClient("192.168.1.104",12050).Client;
			sock.Send (Encoding.UTF8.GetBytes(Paquete));
		} catch {
			Console.WriteLine ("No se pudo conectar con el servidor RIFT");
		}
	}
}