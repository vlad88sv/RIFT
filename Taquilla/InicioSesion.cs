using System;

namespace Taquilla
{
	public partial class InicioSesion : Gtk.Dialog
	{
		public InicioSesion ()
		{
			this.Build ();
			txtClave.Visibility = false;
			lblVersion.Text = "Versión " + global.Version;
		}

		protected void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			string c = "SELECT COUNT(*) AS autorizados, ID_usuario, usuario, nombre, nivel FROM usuarios WHERE usuario='"+txtUsuario.Text+"' AND clave=SHA1('"+this.txtClave.Text+"')";
			if (!MySQL.consultar(c))
				return;
			
			MySQL.Reader.Read();
			
			if (MySQL.Reader["autorizados"].ToString() == "0")
			{
				lblInfo.Markup = "<span  color='red' font='13'>No autorizado</span>";
				auth.ID_usuario = "0";
				auth.Autorizado = false;
				auth.nombre = "";
				auth.nivel = "";
				txtClave.Text = "";
				txtUsuario.Text = "";
				return;
			}
	
			txtUsuario.Text = "";
			txtClave.Text = "";
			auth.ID_usuario = MySQL.Reader["ID_usuario"].ToString();
			auth.Autorizado = true;
			auth.nombre = MySQL.Reader["nombre"].ToString();
			auth.nivel = MySQL.Reader["nivel"].ToString();
			
			// Registramos en el log el suceso
			Historial.Registrar("SESION INICIADA");
			Respond (Gtk.ResponseType.Yes);
		}

		protected void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			Respond(Gtk.ResponseType.Cancel);
		}
	}
}

