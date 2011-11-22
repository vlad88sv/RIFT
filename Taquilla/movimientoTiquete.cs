using System;
using Gtk;
using System.Globalization;
namespace Taquilla
{
	public partial class movimientoTiquete : Gtk.Dialog
	{
		MessageDialog Mensaje = null;
		public Resultadotiquete Resultado = new Resultadotiquete();
		
		[GLib.ConnectBeforeAttribute]
		protected virtual void OnTxtIDTiqueteKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			switch (args.Event.Key)
			{
			case Gdk.Key.Return:
				OnButtonOkClicked(null,null);
			break;	
			
			case Gdk.Key.Escape:
				OnButtonCancelClicked(null,null);
			break;
			}
		}

		public movimientoTiquete ()
		{
			this.Build ();
		}
		
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			Respond(ResponseType.Cancel);
		}
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{		
			// Comprobemos que exista ese tiquete
			MySQL.consultar ("SELECT precio_grabado, fecha_juego, CAST(fecha_juego AS CHAR) AS cast_fecha_juego, CAST(DATE(fecha_juego) AS CHAR) AS cast_fecha_dia_juego, ID_tipo_boleto FROM tickets WHERE ID_ticket='"+txtIDTiquete.Text+"'");
			
			if (!MySQL.Reader.Read())
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Operación inválida, código de tiquete no encontrado.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				Respond(ResponseType.Cancel);
				return;
			}
			
			if (MySQL.Reader["ID_tipo_boleto"].ToString() == (string) tiquete.ID_tipo_movimiento)
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Operación inválida, no puede mover el mismo tiquete dos veces.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				Respond(ResponseType.Cancel);
				return;
			}
			
			Console.WriteLine(MySQL.Reader["cast_fecha_juego"].ToString());

			if (MySQL.Reader["cast_fecha_juego"].ToString() == global.fechaDiaTrabajoMasJuegoFMySQL)
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Operación inválida, no se puede mover a la misma hora.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				Respond(ResponseType.Cancel);
				return;
			}
			
			if (MySQL.Reader["cast_fecha_dia_juego"].ToString() != global.fechaDiaTrabajoFMySQL)
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Operación inválida, no se puede mover un tiquete de otro día.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				Respond(ResponseType.Cancel);
				return;
			}

			// Lo esta tratando de mover antes
			if (DateTime.Parse(global.fechaDiaTrabajoMasJuego,CultureInfo.InvariantCulture).CompareTo(MySQL.Reader["fecha_juego"]) == -1)
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "No se puede mover el tiquete para una hora antes de su venta original.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}
			
			Resultado.precio_grabado = double.Parse(MySQL.Reader["precio_grabado"].ToString());
			Resultado.error = false;
			Resultado.transaccion = "movimiento";
			Resultado.ID_tipo_boleto = tiquete.ID_tipo_normal;
			Resultado.extra = txtIDTiquete.Text;

			MySQL.consultar("UPDATE tickets SET ID_tipo_boleto='"+tiquete.ID_tipo_movimiento+"' WHERE ID_tipo_boleto <> "+tiquete.ID_tipo_movimiento+" AND ID_ticket="+txtIDTiquete.Text);
			
			Respond(ResponseType.Ok);
		}
	}
}

