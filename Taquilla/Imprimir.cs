using System;
namespace Impresora
{
	public class Imprimidor
	{
		public void Tiquete (string linea, string ID_ticket)
		{
			Tiquete(linea,ID_ticket,"","Tiquete");
		}
		
		public void Tiquete (string linea, string ID_ticket, string LPR_Printer, string Titulo)
		{
			
			string Tiquete = "";
			if (LPR_Printer == "")
			{
				LPR_Printer = "EPSON-TEXT";
				
				// Imprimir solo en receipt (solo para EPSON-TEXT porque PDF backend se rompe)
				Tiquete += "\x1B\x63\x30\x02";
			}
			Tiquete += this.Imprimir("RIFT LASER TAG",											1);
			/*
			Tiquete += this.Imprimir("CC La Gran Via",											1);
			Tiquete += this.Imprimir("Edif. 104, L. 104",										1);
			Tiquete += this.Imprimir("Antiguo Cuscatlan, La Libertad",							1);
			*/
			Tiquete += this.Imprimir("www.riftelsalvador.com",									1);
			Tiquete += this.Imprimir("Tel. 2514-0818",											1);
			
			/*
			Tiquete += this.Imprimir("N.I.T. 0614-141084-101-5",								1);
			Tiquete += this.Imprimir("N.R.C. 199960-2",											1);
			Tiquete += this.Imprimir("Laura Elena Margarita Cañas Kurz",						1);
			*/
			
			Tiquete += this.Imprimir("Tiquete No." + ulong.Parse(ID_ticket).ToString("000000"), 1);
			Tiquete += this.Imprimir("Cajero: "+auth.nombre,									1);
			Tiquete += this.Imprimir(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),1);			
			Tiquete += "\n";
			
			
			linea = Tiquete + linea;
			
			/*
			linea += "\n\n\n";
			linea += this.Imprimir("Giro:\n1.venta de otros productos\nno clasificados previamente.",1);
			linea += this.Imprimir("2.venta de otros productos\nalimenticios no clasificados\npreviamente no incluye bebidas.",	1);
			*/
			linea += "\n\n\n\n\n\n\n\n\n\n\x1B\x69";
			
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false;
			proc.StartInfo.FileName="/bin/bash";
			proc.StartInfo.Arguments=" -c \"echo '"+linea+"' | lpr -T '"+Titulo+"' -P '"+LPR_Printer+"'\"";
			
	        proc.StartInfo.CreateNoWindow = true;
			proc.EnableRaisingEvents = true;
	        proc.StartInfo.UseShellExecute = true;
	        proc.StartInfo.ErrorDialog = false;
			
			proc.Start();
			proc.Close();
		}

		// Dos columnas. Segunda columna alineada a la derecha.
		public string Imprimir (string linea, string linea2)
		{
			string lineaCompuesta = linea.ToString() + linea2.ToString().PadLeft(30-linea.Length,' ');
			return lineaCompuesta+"\n";
		}
	
		// Cuatro columnas. Centradas.
		public string Imprimir (string linea, string linea2, string linea3, string linea4)
		{
			string lineaCompuesta = linea.ToString().PadRight(8,' ') + linea2.ToString().PadRight(8,' ') + " | " + linea3.ToString().PadRight(8,' ') + linea4.ToString();
			return lineaCompuesta+"\n";
		}
	
		
		// Dos columnas. Segunda columna alineada a la derecha. DOBLE ALTURA
		public string Imprimir (string linea, string linea2, bool dobleAltura)
		{
			string lineaCompuesta = linea.ToString() + linea2.ToString().PadLeft(15,' ');
			return lineaCompuesta+"\n";
		}
	
		// Una columna, alineada segun int modo.
		public string Imprimir (string linea, int modo)
		{
			/* modo
			 * 1 = Izq.
			 * 2 = Centrado.
			 * 3 = Derecha
			 */
			string lineaCompuesta = "";
			if (modo == 1) {
				lineaCompuesta = new string(' ',Math.Max((15-(int)(linea.Length/2)),0)) + linea.ToString();
			}
			return lineaCompuesta+"\n";
		}		
	}
}