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
			Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
	
			IPAddress serverAddr  = IPAddress.Parse("192.168.1.104");
			
			IPEndPoint endPoint = new IPEndPoint(serverAddr, 12050);
			
			byte[] send_buffer = Encoding.ASCII.GetBytes(Paquete);
			
			sock.SendTo(send_buffer , endPoint);
		} catch {
			Console.WriteLine ("No se pudo conectar con el servidor RIFT");
		}
	}
}