using System;
using Gtk;
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
			MySQL.consultar ("SELECT COUNT(*) AS cuenta FROM tickets WHERE ID_ticket="+txtIDTiquete.Text);
			MySQL.Reader.Read();
			if (MySQL.Reader["cuenta"].ToString() == "0")
			{
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Operación inválida, código de tiquete no encontrado");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				Respond(ResponseType.Cancel);
				return;
			}
			
			MySQL.consultar("UPDATE tickets SET ID_tipo_boleto='"+tiquete.ID_tipo_movimiento+"' WHERE ID_tipo_boleto <> "+tiquete.ID_tipo_movimiento+" AND ID_ticket="+txtIDTiquete.Text);
			Resultado.precio_grabado = 0.00;
			Resultado.error = false;
			Resultado.transaccion = "movimiento";
			Resultado.ID_tipo_boleto = tiquete.ID_tipo_normal;
			Resultado.extra = txtIDTiquete.Text;
			
			Respond(ResponseType.Ok);
		}
	}
}

