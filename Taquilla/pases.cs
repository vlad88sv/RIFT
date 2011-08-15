using System;
using Gtk;
namespace Taquilla
{
	public partial class pases : Gtk.Dialog
	{
		Impresora.Imprimidor impTiquete = new Impresora.Imprimidor();
		//MySQL DB = new MySQL();
				
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			int Cantidad = int.Parse(txtCantidadDeBoletos.Text);
			string c = "";
			string ID_ticket = "";
			string Tiquete = "";
			
			if (txtRazonBoletos.Text.Length < 20)
			{
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "No es posible generar los pases si no ingresa un motivo. Al menos 20 caracteres.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}

			string pase_dias_valido =
							Convert.ToInt32(chkDomingo.Active).ToString() +
							Convert.ToInt32(chkLunes.Active).ToString()	+  	
							Convert.ToInt32(chkMartes.Active).ToString() +	
						  	Convert.ToInt32(chkMiercoles.Active).ToString() +	
						  	Convert.ToInt32(chkJueves.Active).ToString() +	
							Convert.ToInt32(chkViernes.Active).ToString() +	
							Convert.ToInt32(chkSabado.Active).ToString();
			
			string pase_expiracion = calCaducidad.Date.ToString("yyyy-MM-dd");

			
			c = "INSERT INTO `pase_razon` (`ID_pase_razon`, `fechatiempo`, `razon`, `cantidad`, `precio`, `caducidad`, `ID_usuario`, `pase_dias_valido`) VALUES (0, NOW(), '"+txtRazonBoletos.Text+"', '"+txtCantidadDeBoletos.Text+"', '"+txtPrecioBoletos.Text+"', '"+pase_expiracion+"', "+auth.ID_usuario+",'"+pase_dias_valido+"')";
			MySQL.consultar(c);
			
			
			for (int i=0; i<Cantidad; i++)
			{
				
				c = "INSERT INTO tickets (`fecha_juego`,`numero_jugador`,`precio_grabado`,`fecha_vendido`,`ID_tipo_boleto`,`pase_dias_valido`,`pase_expiracion`) VALUES(NOW(), "+i+" , '"+txtPrecioBoletos.Text+"', NOW(), 11, '"+pase_dias_valido+"', '"+pase_expiracion+"')";
				MySQL.consultar(c);
				
				c = "SELECT MAX(ID_ticket) AS Last_ID FROM tickets";
				MySQL.consultar(c);
				if(MySQL.Reader.Read())
					ID_ticket = MySQL.Reader["Last_ID"].ToString();

				Tiquete = "";
				Tiquete += impTiquete.Imprimir("+SERIE:", (i+1) +" de " + Cantidad);
				
				Tiquete += impTiquete.Imprimir("+CADUCIDAD:", calCaducidad.Date.ToString("dd/MM/yyyy"));
				
				Tiquete += impTiquete.Imprimir("VALIDO PARA", "1 JUEGO");
				Tiquete += impTiquete.Imprimir("DIAS VALIDO", 1);
				if (chkLunes.Active)
					Tiquete += impTiquete.Imprimir("-Lunes", "");
				if (chkMartes.Active)
					Tiquete += impTiquete.Imprimir("-Martes", "");
				if (chkMiercoles.Active)
					Tiquete += impTiquete.Imprimir("-Miercoles", "");
				if (chkJueves.Active)
					Tiquete += impTiquete.Imprimir("-Jueves", "");
				if (chkViernes.Active)
					Tiquete += impTiquete.Imprimir("-Viernes", "");
				if (chkSabado.Active)
					Tiquete += impTiquete.Imprimir("-Sabado", "");
				if (chkDomingo.Active)
					Tiquete += impTiquete.Imprimir("-Domingo", "");
				
				impTiquete.Tiquete(Tiquete,ID_ticket);
			}
		}

		public pases ()
		{
			this.Build ();
		}
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
	}
}

