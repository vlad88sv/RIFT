using System;
using Gtk;
namespace Taquilla
{
	public partial class Eventos : Gtk.Window
	{	
		protected virtual void OnCmdCancelarClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}

		public Eventos () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			for (int h = 0; h < 65; h++)
				cmdHoraInicio.AppendText (DateTime.Parse("08:00").AddMinutes(15.0*(double)h).ToString("HH:mm"));
			
			for (int h = 0; h < 65; h++)
				cmdHoraFinal.AppendText (DateTime.Parse("08:00").AddMinutes(15.0*(double)h).ToString("HH:mm"));
			
			Gtk.TreeIter iter = new Gtk.TreeIter();
			cmdHoraInicio.Model.GetIterFirst(out iter);
			cmdHoraInicio.SetActiveIter(iter);
			
			cmdHoraFinal.Model.GetIterFirst(out iter);
			cmdHoraFinal.SetActiveIter(iter);
			
			lblNombreUsuario.Text = auth.nombre;
		}
		
		protected virtual void OnCmdProgramarClicked (object sender, System.EventArgs e)
		{
			
			MessageDialog Mensaje = null;

			if (txtPatrocinadoPor.Text.Length < 5)
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Favor especificar el nombre de la persona que solicita el evento.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}

			//*** Primero calculemos todos los parametros a evaluar para ver si el evento cabe ***//
			string fechaEvento = calFechaEvento.Date.ToString("yyyy-MM-dd");
			//string fechaInicio = calFechaEvento.Date.ToString("yyyy-MM-dd") + " " + cmdHoraInicio.ActiveText.ToString() +":00";
			//string fechaFinal = calFechaEvento.Date.ToString("yyyy-MM-dd") + " " + cmdHoraFinal.ActiveText.ToString() +":00";
			string horaInicio = cmdHoraInicio.ActiveText.ToString() +":00";
			string horaFinal = cmdHoraFinal.ActiveText.ToString() +":00";
			
			
			int TiempoAdecuado = DateTime.Parse(fechaEvento + " " + horaInicio).CompareTo(DateTime.Now);
			
			// Ya se pasó la hora del evento
			if(TiempoAdecuado == -1)
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede vender para este día y hora (ya pasó).");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}

			/*
			MySQL.consultar("SELECT COUNT(*) AS juegos FROM tickets WHERE fecha_juego BETWEEN '"+fechaInicio+"' AND '"+fechaFinal+"'");
			MySQL.Reader.Read();

			if (MySQL.Reader["juegos"].ToString() != "0")
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede programar. Hay "+MySQL.Reader["juegos"].ToString()+" reservados previamente para esa hora.\nDebe resolver manualmente.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}
			*/
			// Ingreso de evento en base de datos
			MySQL.consultar("INSERT INTO `rift3`.`eventos` (`ID_evento`,`precio_evento`, `patrocinado_por`, `nombre_evento`, `fecha_evento`, `fecha_vendido`, `hora_inicio`, `hora_final`, `agregado_por_usuario`, `infoContacto`) VALUES (0, '"+txtPrecioEvento.Text+"', '"+txtPatrocinadoPor.Text+"','"+txtNombreEvento.Text+"','"+fechaEvento+"', NOW() ,'"+horaInicio+"','"+horaFinal+"','"+auth.ID_usuario+"','"+txtinfoContacto.Text+"')");
			
			// Constancia de evento para cliente
			string Tiquete = "";
			Tiquete +=  Imprimidor.Imprimir ("+- COPIA CLIENTE -+",1);
			Tiquete +=  Imprimidor.Imprimir ("EVENTO ESPECIAL",1);
			Tiquete +=  Imprimidor.Imprimir ("Fecha de evento",fechaEvento);
			Tiquete +=  Imprimidor.Imprimir ("Precio evento","$"+txtPrecioEvento.Text);
			Tiquete +=  Imprimidor.Imprimir ("Hora de inicio",horaInicio);
			Tiquete +=  Imprimidor.Imprimir ("Hora de finalizacion",horaFinal);
			Tiquete +=  "\n\n\n\n\n__________________\n"+txtPatrocinadoPor.Text+"\n\n\n\n\n__________________\n"+auth.nombre;
			Imprimidor.Tiquete(Tiquete,"8888");
			
			// Constancia de evento para RIFT
			Tiquete = "";
			Tiquete +=  Imprimidor.Imprimir ("+- COPIA RIFT -+",1);
			Tiquete +=  Imprimidor.Imprimir ("EVENTO ESPECIAL",1);
			Tiquete +=  Imprimidor.Imprimir ("Fecha de evento",fechaEvento);
			Tiquete +=  Imprimidor.Imprimir ("Precio evento","$"+txtPrecioEvento.Text);
			
			Tiquete +=  Imprimidor.Imprimir ("Hora de inicio",horaInicio);
			Tiquete +=  Imprimidor.Imprimir ("Hora de finalizacion",horaFinal);
			Tiquete +=  "\n\n\n\n\n__________________\n"+txtPatrocinadoPor.Text+"\n\n\n\n\n__________________\n"+auth.nombre;
			Imprimidor.Tiquete(Tiquete,"8888");
			
			Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Evento ingresado.\nEsta ventana se cerrará");
			Mensaje.Title="Éxito";
			Mensaje.Run();
			Mensaje.Destroy();
			this.Destroy();
			return;
			
		}
		
		
	}
}

