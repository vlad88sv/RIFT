using System;
namespace Taquilla
{
	public partial class Registro : Gtk.Window
	{
		//MySQL DB_registro = new MySQL();
		public Registro (string fecha) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			Gtk.TreeStore tree_model_log = new Gtk.TreeStore(typeof(string),typeof(string),typeof(string));
			treeLog.AppendColumn("Hora",new Gtk.CellRendererText(), "markup", 0);
			treeLog.AppendColumn("Usuario",new Gtk.CellRendererText(), "markup", 1);
			treeLog.AppendColumn("Historial",new Gtk.CellRendererText(), "markup", 2);
			/* Obtengamos los últimos <n> logs */
			string c = "SELECT `ID_historial`, DATE_FORMAT(`fecha`,'%r') AS 'hora', `ID_usuario`, `usuario`, `historial` FROM `historial` LEFT JOIN `usuarios` USING (`ID_usuario`) WHERE DATE(`fecha`)='"+fecha+"' ORDER BY fecha DESC";
					
			if (MySQL.consultar(c))
			{
				while (MySQL.Reader.Read())
				{
					tree_model_log.AppendValues(MySQL.Reader["hora"].ToString() , MySQL.Reader["usuario"].ToString() , MySQL.Reader["historial"].ToString());
				}
			}
			treeLog.Model = tree_model_log;
			this.Title = "Registro de día: " + fecha;
		}
	}
}

