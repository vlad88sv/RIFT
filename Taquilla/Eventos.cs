using System;
using Gtk;
namespace Taquilla
{
	public partial class Eventos : Gtk.Window
	{	
		// modo
		// 0 = creación de evento
		// 1 = edición de evento
		private int modo = 0;
		
		// Usado si modo = 1
		private int ID_evento = 0;
		
		protected virtual void OnCmdCancelarClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}

		public Eventos (int ID_evento = 0) : base(Gtk.WindowType.Toplevel)
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
		
		
		private void ValidarEvento()
		{
			bool FechaCorrecta = false;
			bool InicioMenorQueFinal = false;
			bool HayContacto = false;
			bool HayNombreEvento = false;
			bool HayPatrocinador = false;
			bool PrecioValido = false;
			try
			{
				if (DateTime.Parse(cmdHoraInicio.ActiveText) < DateTime.Parse(cmdHoraFinal.ActiveText))
					InicioMenorQueFinal = true;
				//Console.WriteLine("Evento:: prueba InicioMenorQueFinal superada");
			} catch {}
			
			try
			{
				if (calFechaEvento.Date >= DateTime.Now.Date)
					FechaCorrecta = true;
			} catch {}
			
			HayContacto = txtinfoContacto.Text != "";
			HayNombreEvento = txtNombreEvento.Text != "";
			HayPatrocinador = txtPatrocinadoPor.Text != "";
			
			try
			{
				PrecioValido = double.Parse(txtPrecioEvento.Text) >= 0.00;
			} catch {}
			
			//Console.WriteLine("Evento:: todas las pruebas superadas");
			if (InicioMenorQueFinal && HayContacto && HayNombreEvento && HayPatrocinador && PrecioValido && FechaCorrecta )
			{
				lblInfo.Markup = "El evento inicia a las <b>" + DateTime.Parse(cmdHoraInicio.ActiveText).ToString("hh:mm tt") + "</b> y termina a las <b>" + DateTime.Parse(cmdHoraFinal.ActiveText).ToString("hh:mm tt") + "</b> el día <b>" + calFechaEvento.Date.ToString("dddd d MMMM yyyy") + "</b>";
				cmdProgramar.Sensitive = true;
			} else {
				string errores = "";
				
				if (!InicioMenorQueFinal)
					errores += "La hora de inicio debe ser menor que la hora de final.\n";
				
				if (!HayContacto)
					errores += "Falta número télefonico de contacto.\n";
				
				if (!HayNombreEvento)
					errores += "Falta nombre de evento.\n";
				
				if (!HayPatrocinador)
					errores += "Falta nombre de patrocinador.\n";
				
				if (!PrecioValido)
					errores += "El precio debe ser un número decimal positivo y sin '$'. Ej: <b>160.00</b>.\n";
				
				if (!FechaCorrecta)
					errores += "La fecha del evento debe ser igual o despues de hoy.";
				
				lblInfo.Markup = "El evento aún no es válido:\n" + errores;
				
				cmdProgramar.Sensitive = false;
			}
		}
		
		protected void OnCmdHoraInicioChanged (object sender, System.EventArgs e)
		{
			ValidarEvento();
		}

		protected void OnCmdHoraFinalChanged (object sender, System.EventArgs e)
		{
			ValidarEvento();
		}

		protected void OnCalFechaEventoDaySelected (object sender, System.EventArgs e)
		{
			ValidarEvento();
		}

		protected void OnTxtPrecioEventoKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ValidarEvento();
		}

		protected void OnTxtPatrocinadoPorKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ValidarEvento();
		}

		protected void OnTxtinfoContactoKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ValidarEvento();
		}

		protected void OnTxtNombreEventoKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ValidarEvento();
		}
	}
}

