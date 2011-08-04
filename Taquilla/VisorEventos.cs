using System;
namespace Taquilla
{
	public partial class VisorEventos : Gtk.Dialog
	{
		//MySQL DB = new MySQL();
		public string FechaSQL;
		
		Gtk.ListStore treeEventosModel = new Gtk.ListStore(
			typeof(string), // ID Evento
			typeof(string), // Patrocinado por
			typeof(string), // Descripcion
			typeof(string), // Precio evento
			typeof(string), // Precio comida
			typeof(string), // Detalle comida
			typeof(string), // Precio cafeteria
			typeof(string), // Detalle cafeteria
			typeof(string), // Hora inicio
			typeof(string), // Hora final
			typeof(string)  // Vendido por
		);
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
		public VisorEventos (string bFechaSQL)
		{		
			this.Build ();
		
			this.FechaSQL = bFechaSQL;
			treeEventos.Model = treeEventosModel;
			
			treeEventos.AppendColumn("ID Evento",			new Gtk.CellRendererText(),"markup",0);
			treeEventos.AppendColumn("Patrocinado por",		new Gtk.CellRendererText(),"markup",1);
			treeEventos.AppendColumn("Descripción evento",	new Gtk.CellRendererText(),"markup",2);
			treeEventos.AppendColumn("Precio evento $",		new Gtk.CellRendererText(),"markup",3);
			treeEventos.AppendColumn("Precio comida $",		new Gtk.CellRendererText(),"markup",4);
			treeEventos.AppendColumn("Detalle comida",		new Gtk.CellRendererText(),"markup",5);
			treeEventos.AppendColumn("Precio cafetería $",	new Gtk.CellRendererText(),"markup",6);
			treeEventos.AppendColumn("Detalle cafetería",	new Gtk.CellRendererText(),"markup",7);
			treeEventos.AppendColumn("Hora inicio",			new Gtk.CellRendererText(),"markup",8);
			treeEventos.AppendColumn("Hora final",			new Gtk.CellRendererText(),"markup",9);
			treeEventos.AppendColumn("Vendido por",			new Gtk.CellRendererText(),"markup",10);
			CargarTreeEventos("DATE(`eventos`.`fecha_evento`)='"+this.FechaSQL+"'"); 
		}
		
		protected virtual void OnCmdEventosVendidosHoyClicked (object sender, System.EventArgs e)
		{
			CargarTreeEventos("DATE(`eventos`.`fecha_vendido`)='"+this.FechaSQL+"'"); 
		}
		
		private void CargarTreeEventos(string WHERE)
		{
			treeEventosModel.Clear();
			string c = "SELECT `ID_evento`, `patrocinado_por`, `nombre_evento`, `fecha_evento`, `fecha_vendido`, `eventos`.`ID_evento`, `eventos`.`precio_evento`, `eventos`.`precio_comida`, `eventos`.`precio_cafeteria`, `detalle_comida`, `detalle_cafeteria`,  `hora_inicio`, `hora_final`, `agregado_por_usuario` FROM `rift3`.`eventos` WHERE "+WHERE+" ORDER BY `eventos`.`hora_inicio`";
			MySQL.consultar(c);

			
			while (MySQL.Reader.Read()) {
				treeEventosModel.AppendValues(
					MySQL.Reader["ID_evento"].ToString(), 
					MySQL.Reader["patrocinado_por"].ToString(), 
					MySQL.Reader["nombre_evento"].ToString(), 
					MySQL.Reader["precio_evento"].ToString(), 
					MySQL.Reader["precio_comida"].ToString(), 
					MySQL.Reader["detalle_comida"].ToString(), 
					MySQL.Reader["precio_cafeteria"].ToString(), 
					MySQL.Reader["detalle_cafeteria"].ToString(),
					MySQL.Reader["hora_inicio"].ToString(),
					MySQL.Reader["hora_final"].ToString(),
					MySQL.Reader["agregado_por_usuario"].ToString()
				);
			}

		}
		
		protected virtual void OnCmdEventosHoyClicked (object sender, System.EventArgs e)
		{
			CargarTreeEventos("DATE(`eventos`.`fecha_evento`)='"+this.FechaSQL+"'"); 
		}
		
		protected virtual void OnCmdBuscarEventosClicked (object sender, System.EventArgs e)
		{
			CargarTreeEventos("`eventos`.`ID_evento`='"+txtIDEvento.Text+"' OR LOWER(patrocinado_por) LIKE '%"+txtIDEvento.Text+"%' OR LOWER(nombre_evento) LIKE '%"+txtIDEvento.Text+"%'"); 
		}
		
		[GLib.ConnectBeforeAttribute]
		protected virtual void OnTreeEventosKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			if (args.Event.Key == Gdk.Key.e || args.Event.Key == Gdk.Key.E)
			{
				Taquilla.Eventos eventos = new Taquilla.Eventos();
				eventos.Show();
			}
		}
		
		
	}
}

