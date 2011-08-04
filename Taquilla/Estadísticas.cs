using System;
using Gtk;
namespace Taquilla
{
	public partial class Estadísticas : Gtk.Dialog
	{
		Gtk.ListStore treemodelDia 		= new Gtk.ListStore(typeof(string),typeof(string),typeof(string));
		Gtk.ListStore treemodelMes 		= new Gtk.ListStore(typeof(string),typeof(string),typeof(string));
		Gtk.ListStore treemodelHora 	= new Gtk.ListStore(typeof(string),typeof(string),typeof(string));
		Gtk.ListStore treemodelDiario 	= new Gtk.ListStore(typeof(string),typeof(string),typeof(string));
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
		public Estadísticas ()
		{
			this.Build ();
			treeDia.Model 		= treemodelDia;
			treeMes.Model 		= treemodelMes;
			treeHora.Model 		= treemodelHora;
			treeDiario.Model 	= treemodelDiario;
			
			treeDia.AppendColumn	("Día",new Gtk.CellRendererText(), "markup",0);
			treeDia.AppendColumn	("Juegos vendidos",new Gtk.CellRendererText(), "markup",1);
			treeDia.AppendColumn	("Total venta (jugado) $$$",new Gtk.CellRendererText(), "markup",2);
			
			treeMes.AppendColumn	("Mes",new Gtk.CellRendererText(), "markup",0);
			treeMes.AppendColumn	("Juegos vendidos",new Gtk.CellRendererText(), "markup",1);
			treeMes.AppendColumn	("Total venta (jugado) $$$",new Gtk.CellRendererText(), "markup",2);
			
			treeHora.AppendColumn	("Hora",new Gtk.CellRendererText(), "markup",0);
			treeHora.AppendColumn	("Juegos vendidos",new Gtk.CellRendererText(), "markup",1);
			treeHora.AppendColumn	("Total venta (jugado) $$$",new Gtk.CellRendererText(), "markup",2);
					
			treeDiario.AppendColumn	("Día",new Gtk.CellRendererText(), "markup",0);
			treeDiario.AppendColumn	("Juegos vendidos",new Gtk.CellRendererText(), "markup",1);
			treeDiario.AppendColumn	("Total venta (jugado) $$$",new Gtk.CellRendererText(), "markup",2);	
			
			string c = null;
			
			// Venta por día (Lu..Do)
			c = "SELECT DATE_FORMAT(fecha_vendido,'%W') AS dia, COUNT(*) AS juegos_vendidos, COALESCE(SUM(precio_grabado),0) + (SELECT COALESCE(SUM(precio_evento+precio_cafeteria+precio_comida),0) FROM eventos WHERE DATE_FORMAT(eventos.fecha_vendido,'%w')=DATE_FORMAT(tickets.fecha_vendido,'%w')) AS total FROM tickets GROUP BY DATE_FORMAT(fecha_vendido,'%w') ORDER BY total DESC";
			MySQL.consultar(c);
			while(MySQL.Reader.Read())
			{
				treemodelDia.AppendValues(MySQL.Reader["dia"].ToString(), MySQL.Reader["juegos_vendidos"].ToString(), MySQL.Reader["total"].ToString());
			}
			
			// Venta por mes
			c = "SELECT DATE_FORMAT(fecha_vendido,'%M') AS mes,COUNT(*) as juegos_vendidos, COALESCE(SUM(precio_grabado),0) + (SELECT COALESCE(SUM(precio_evento+precio_cafeteria+precio_comida),0) FROM eventos WHERE DATE_FORMAT(eventos.fecha_vendido,'%m')=DATE_FORMAT(tickets.fecha_vendido,'%m')) AS total FROM tickets GROUP BY DATE_FORMAT(fecha_vendido,'%m') ORDER BY total DESC";
			MySQL.consultar(c);
			while(MySQL.Reader.Read())
			{
				treemodelMes.AppendValues(MySQL.Reader["mes"].ToString(), MySQL.Reader["juegos_vendidos"].ToString(), MySQL.Reader["total"].ToString());
			}
			
			// Venta por hora
			c = "SELECT DATE_FORMAT(fecha_juego,'%l%p') AS hora,COUNT(*) as juegos_vendidos, COALESCE(SUM(precio_grabado),0) + (SELECT COALESCE(SUM(precio_evento+precio_cafeteria+precio_comida),0) FROM eventos WHERE DATE_FORMAT(fecha_evento,'%k')=DATE_FORMAT(fecha_juego,'%k')) AS total FROM tickets GROUP BY DATE_FORMAT(fecha_juego,'%k') ORDER BY total DESC";
			MySQL.consultar(c);
			while(MySQL.Reader.Read())
			{
				treemodelHora.AppendValues(MySQL.Reader["hora"].ToString(), MySQL.Reader["juegos_vendidos"].ToString(), MySQL.Reader["total"].ToString());
			}
			
			// Venta por día (1..{28,31})
			c = "SELECT DATE_FORMAT(fecha_vendido,'%W %e de %M de %Y') AS dia, COUNT(*) as juegos_vendidos, COALESCE(SUM(precio_grabado),0) + (SELECT COALESCE(SUM(precio_evento+precio_cafeteria+precio_comida),0) FROM eventos WHERE DATE(eventos.fecha_vendido)=DATE(tickets.fecha_vendido)) AS total FROM tickets GROUP BY DATE(fecha_vendido) ORDER BY DATE(fecha_vendido) ASC";
			MySQL.consultar(c);
			while(MySQL.Reader.Read())
			{
				treemodelDiario.AppendValues(MySQL.Reader["dia"].ToString(), MySQL.Reader["juegos_vendidos"].ToString(), MySQL.Reader["total"].ToString());
			}
		}
	}
}

