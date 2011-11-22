using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Taquilla
{
	public class RIFTnetwork
	{
		public RIFTnetwork ()
		{
		}
		
		public void iniciar () {}
		public void abortar () {}
		public void finalizar () {}
		
		public void enviarPaqueteUDP ()
		{
			Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);

			IPAddress serverAddr  = IPAddress.Parse("192.168.2.255");
			
			IPEndPoint endPoint = new IPEndPoint(serverAddr, 11000);
			
			string text = "Hello";
			byte[] send_buffer = Encoding.ASCII.GetBytes(text );
			
			sock.SendTo(send_buffer , endPoint);	
		}
	}
}

