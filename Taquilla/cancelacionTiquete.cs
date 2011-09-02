using System;
using Gtk;
namespace Taquilla
{
	public partial class cancelacionTiquete : Gtk.Dialog
	{
		
		public cancelacionTiquete ()
		{
			this.Build ();
		}
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			string c = "SELECT COUNT(*) AS autorizados FROM usuarios WHERE clave=SHA1('') AND nivel='gerente'";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["autorizados"].ToString() == "0")
			{
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede cancelar. Esta clave no esta registrada.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}
		}
	}
}

