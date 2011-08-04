using System;
namespace Taquilla
{
	public partial class Ingresos : Gtk.Dialog
	{
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			if (MySQL.consultar("SELECT COUNT(*) AS cuenta FROM `cafeteria_articulos` WHERE codigo_barra='"+txtCodigoBarra.Text+"'"))
			{
				MySQL.Reader.Read();

				if (MySQL.Reader["cuenta"].ToString() == "0")
				{
					lblInformacion.Text = "Producto no existe";
					return;
				}
			}

			
			MySQL.consultar("INSERT INTO `cafeteria_stock` (`codigo_barra`, `stock`, `fecha`, `ingresado_por`) VALUES('"+txtCodigoBarra.Text+"','"+txtCantidad.Text+"', NOW(), '"+auth.ID_usuario.ToString()+"')");
			lblInformacion.Text = "Stock [" + txtCantidad.Text + "] ingresado para: " + txtCodigoBarra.Text;
			ReiniciarDatos();
		}
		
		
		public Ingresos ()
		{
			this.Build ();
		}
		
		protected virtual void OnCmdLimpiarClicked (object sender, System.EventArgs e)
		{
			ReiniciarDatos();
		}
		
		private void ReiniciarDatos()
		{
			txtCantidad.Text = "";
			txtCodigoBarra.Text = "";
			lblInformacion.Text = "";
		}
		
	}
}

