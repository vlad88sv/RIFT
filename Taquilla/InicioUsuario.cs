using System;
namespace Taquilla
{
	public partial class InicioUsuario : Gtk.Window
	{
		public InicioUsuario () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		protected void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			string c = "SELECT COUNT(*) AS autorizados, ID_usuario, usuario, nombre, nivel FROM usuarios WHERE usuario='"+txtUsuario.Text+"' AND clave=SHA1('"+this.txtClave.Text+"')";
			if (MySQL.consultar(c))
			{
				MySQL.Reader.Read();
				
				if (MySQL.Reader["autorizados"].ToString() == "0")
				{
					lblInfo.Markup = "<span  color='red' font='14'>Usuario/Clave no autorizada</span>";
					auth.ID_usuario = "0";
					auth.Autorizado = false;
					auth.nombre = "";
					auth.nivel = "";
					return;
				}
		
				txtUsuario.Text = "";
				txtClave.Text = "";
				auth.ID_usuario = MySQL.Reader["ID_usuario"].ToString();
				auth.Autorizado = true;
				auth.nombre = MySQL.Reader["nombre"].ToString();
				auth.nivel = MySQL.Reader["nivel"].ToString();
				cmdSesion.Label = "Finalizar\n"+MySQL.Reader["usuario"].ToString();
				
				// Registramos en el log el suceso
				Historial.Registrar("SESION INICIADA");
				this.Destroy();
			}

		}
	}
}

