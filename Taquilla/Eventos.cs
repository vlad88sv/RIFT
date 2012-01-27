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
			
			for (int h = 1; h < 20; h++)
				cmbDuracion.AppendText (DateTime.Parse("00:00").AddMinutes(15.0*(double)h).ToString("HH:mm"));
			
			Gtk.TreeIter iter = new Gtk.TreeIter();
			cmdHoraInicio.Model.GetIterFirst(out iter);
			cmdHoraInicio.SetActiveIter(iter);
			
			cmbDuracion.Model.GetIterFirst(out iter);
			cmbDuracion.SetActiveIter(iter);
			
			txtinfoContacto.Changed += (sender, e) => ValidarEvento();
			txtNombreEvento.Changed += (sender, e) => ValidarEvento();
			txtPatrocinadoPor.Changed += (sender, e) => ValidarEvento();
			txtPrecioEvento.Changed += (sender, e) => ValidarEvento();
			calFechaEvento.DaySelected += (sender, e) => ValidarEvento();
			cmdHoraInicio.Changed += (sender, e) => ValidarEvento();
			cmbDuracion.Changed += (sender, e) => ValidarEvento();
			
			ValidarEvento();
		}
		
		protected virtual void OnCmdProgramarClicked (object sender, System.EventArgs e)
		{
			
			MessageDialog Mensaje = null;

			//*** Primero calculemos todos los parametros a evaluar para ver si el evento cabe ***//
			string fechaEvento = calFechaEvento.Date.ToString("yyyy-MM-dd");
			//string fechaInicio = calFechaEvento.Date.ToString("yyyy-MM-dd") + " " + cmdHoraInicio.ActiveText.ToString() +":00";
			//string fechaFinal = calFechaEvento.Date.ToString("yyyy-MM-dd") + " " + cmdHoraFinal.ActiveText.ToString() +":00";
			string horaInicio = cmdHoraInicio.ActiveText +":00";
			string horaFinal = (DateTime.Parse(cmdHoraInicio.ActiveText) + TimeSpan.Parse(cmbDuracion.ActiveText)).ToString("HH:mm") + ":00";
			
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
			MySQL.consultar("INSERT INTO `rift3`.`eventos` (`ID_evento`,`precio_evento`, `patrocinado_por`, `nombre_evento`, `fecha_evento`, `fecha_vendido`, `hora_inicio`, `hora_final`, `agregado_por_usuario`, `infoContacto`, `certificado`,`duracion`) VALUES (0, '"+txtPrecioEvento.Text+"', '"+txtPatrocinadoPor.Text+"','"+txtNombreEvento.Text+"',"+(chkCertificado.Active ? "NULL" : "'"+fechaEvento+"'")+", NOW() ,'"+horaInicio+"','"+horaFinal+"','"+auth.ID_usuario+"','"+txtinfoContacto.Text+"','"+(chkCertificado.Active ? 1 : 0)+"','"+cmbDuracion.ActiveText+"')");
			
			MySQL.consultar("SELECT LAST_INSERT_ID() AS ID_evento");
			MySQL.Reader.Read();
			string ID_evento = MySQL.Reader["ID_evento"].ToString();
			
			// Constancia de evento para cliente
			string Tiquete = "";
			Tiquete +=  Imprimidor.Imprimir ("EVENTO ESPECIAL",1);
			if (chkCertificado.Active) Tiquete +=  Imprimidor.Imprimir ("***certificado de regalo***",1);
			if (!chkCertificado.Active) Tiquete +=  Imprimidor.Imprimir ("Fecha de evento",fechaEvento);
			if (!chkCertificado.Active) Tiquete +=  Imprimidor.Imprimir ("Precio evento","$"+txtPrecioEvento.Text);
			if (!chkCertificado.Active) Tiquete +=  Imprimidor.Imprimir ("Hora de inicio",horaInicio);
			if (!chkCertificado.Active) Tiquete +=  Imprimidor.Imprimir ("Hora de finalizacion",horaFinal);
			Tiquete +=  Imprimidor.Imprimir ("Duracion",((int)(TimeSpan.Parse(cmbDuracion.ActiveText).TotalMinutes / 60)).ToString() + " horas y " + (TimeSpan.Parse(cmbDuracion.ActiveText).TotalMinutes % 60).ToString() + " minutos");
			Tiquete +=  Imprimidor.Imprimir ("Valido para", global.maximo_jugadores.ToString() + " personas");
			
			Tiquete +=  "\n\n\n\n\n__________________\n"+auth.nombre;
			Imprimidor.Tiquete(Tiquete,ID_evento);
			
			
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
			bool HayContacto = false;
			bool HayNombreEvento = false;
			bool HayPatrocinador = false;
			bool PrecioValido = false;
			
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
			if ( (chkCertificado.Active || FechaCorrecta) && HayContacto && HayNombreEvento && HayPatrocinador && PrecioValido )
			{
				if (!chkCertificado.Active)
					lblInfo.Markup = "El evento inicia a las <b>" + DateTime.Parse(cmdHoraInicio.ActiveText).ToString("hh:mm tt") + "</b> y termina a las <b>" + (DateTime.Parse(cmdHoraInicio.ActiveText) + TimeSpan.Parse(cmbDuracion.ActiveText)).ToString("hh:mm tt") + "</b> el día <b>" + calFechaEvento.Date.ToString("dddd d MMMM yyyy") + "</b>";
				else
					lblInfo.Markup = "El certificado es válido, será canjeable por un evento con duración de: <b>" + ((int)(TimeSpan.Parse(cmbDuracion.ActiveText).TotalMinutes / 60)).ToString() + " horas y " + (TimeSpan.Parse(cmbDuracion.ActiveText).TotalMinutes % 60).ToString() + " minutos</b>";
				
				cmdProgramar.Sensitive = true;
			} else {
				string errores = "";
				
				if (!HayContacto)
					errores += "Falta número télefonico de contacto.\n";
				
				if (!HayNombreEvento)
					errores += "Falta nombre de evento.\n";
				
				if (!HayPatrocinador)
					errores += "Falta nombre de patrocinador.\n";
				
				if (!PrecioValido)
					errores += "El precio debe ser un número decimal positivo y sin '$'. Ej: <b>160.00</b>.\n";
				
				if (!chkCertificado.Active && !FechaCorrecta)
					errores += "La fecha del evento debe ser igual o despues de hoy.";
				
				lblInfo.Markup = "El evento aún no es válido:\n" + errores;
				
				cmdProgramar.Sensitive = false;
			}
		}
		
		protected void OnChkCertificadoToggled (object sender, System.EventArgs e)
		{
			calFechaEvento.Sensitive = !chkCertificado.Active;
			cmdHoraInicio.Sensitive = !chkCertificado.Active;
			ValidarEvento();
		}
	}
}

