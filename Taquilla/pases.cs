using System;
using Gtk;

namespace Taquilla
{
	public partial class pases : Gtk.Dialog
	{
		Gtk.ListStore lsPerfilPase = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
		Gtk.CellRendererText crt1 = new Gtk.CellRendererText();
		Gtk.CellRendererText crt2 = new Gtk.CellRendererText();
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			int Cantidad = int.Parse(txtCantidadDeBoletos.Text);
			string c = "";
			string ID_ticket = "";
			string Tiquete = "";
			
			if (txtRazonBoletos.Text.Length < 10)
			{
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "No es posible generar los pases si no ingresa un motivo. Al menos 10 caracteres.");
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
			
			Gtk.TreeIter iter = new Gtk.TreeIter();
			cmbPerfilPase.GetActiveIter(out iter);
			
			c = "INSERT INTO `pase_razon` (`ID_pase_razon`, `fechatiempo`, `razon`, `cantidad`, `precio`, `caducidad`, `ID_usuario`, `pase_dias_valido`, `ID_perfil`) VALUES (0, NOW(), '"+txtRazonBoletos.Text+"', '"+txtCantidadDeBoletos.Text+"', '"+txtPrecioBoletos.Text+"', '"+pase_expiracion+"', "+auth.ID_usuario+",'"+pase_dias_valido+"','"+lsPerfilPase.GetValue(iter,0).ToString()+"')";
			MySQL.consultar(c);
			
			for (int i=0; i<Cantidad; i++)
			{
				
				c = "INSERT INTO tickets (`fecha_juego`,`numero_jugador`,`precio_grabado`,`fecha_vendido`,`ID_tipo_boleto`,`pase_dias_valido`,`pase_expiracion`) VALUES(NOW(), "+ (i+100) +" , '"+txtPrecioBoletos.Text+"', NOW(), 11, '"+pase_dias_valido+"', '"+pase_expiracion+"')";
				MySQL.consultar(c);
				
				c = "SELECT MAX(ID_ticket) AS Last_ID FROM tickets";
				MySQL.consultar(c);
				if(MySQL.Reader.Read())
					ID_ticket = MySQL.Reader["Last_ID"].ToString();

				Tiquete = "";
				Tiquete += Imprimidor.Imprimir("+SERIE:", (i+1) +" de " + Cantidad);
				
				Tiquete += Imprimidor.Imprimir("+CADUCIDAD:", calCaducidad.Date.ToString("dd/MM/yyyy"));
				
				Tiquete += Imprimidor.Imprimir("VALIDO PARA", "1 JUEGO");
				Tiquete += Imprimidor.Imprimir("DIAS VALIDO", 1);
				if (chkLunes.Active)
					Tiquete += Imprimidor.Imprimir("-Lunes", "");
				if (chkMartes.Active)
					Tiquete += Imprimidor.Imprimir("-Martes", "");
				if (chkMiercoles.Active)
					Tiquete += Imprimidor.Imprimir("-Miercoles", "");
				if (chkJueves.Active)
					Tiquete += Imprimidor.Imprimir("-Jueves", "");
				if (chkViernes.Active)
					Tiquete += Imprimidor.Imprimir("-Viernes", "");
				if (chkSabado.Active)
					Tiquete += Imprimidor.Imprimir("-Sabado", "");
				if (chkDomingo.Active)
					Tiquete += Imprimidor.Imprimir("-Domingo", "");
				
				Imprimidor.Tiquete(Tiquete,ID_ticket);
			}
			
			c = "UPDATE `perfil_pases` SET `utilizados` = (`utilizados` + 1) WHERE `ID_perfil` = '" + lsPerfilPase.GetValue(iter,0).ToString() + "'";
			MySQL.consultar(c);
			
			MessageDialog MensajeSalida = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Pases generados.\nEsta ventana se cerrará");
			MensajeSalida.Title="Éxito";
			MensajeSalida.Run();
			MensajeSalida.Destroy();
			this.Destroy();
		}

		public pases ()
		{
			this.Build ();

			cmbPerfilPase.PackStart(crt1,false);
			cmbPerfilPase.PackStart(crt2,false);
			cmbPerfilPase.AddAttribute(crt1, "text", 1);
			cmbPerfilPase.Model = lsPerfilPase;

			string c = "SELECT `ID_perfil`, `nombre_perfil`, `cantidad_pases`, `dias_validos`, `precio_individual`, `fecha_expiracion`, `razon`, `disponibles`, `ilimitado`, `utilizados` FROM `perfil_pases` WHERE deshabilitado=0";
			MySQL.consultar(c);
			
			while (MySQL.Reader.Read())
			{
				lsPerfilPase.AppendValues(
					MySQL.Reader["ID_perfil"].ToString(),			//0
					MySQL.Reader["nombre_perfil"].ToString(),		//1
					MySQL.Reader["cantidad_pases"].ToString(),		//2
					MySQL.Reader["dias_validos"].ToString(), 		//3
					MySQL.Reader["precio_individual"].ToString(), 	//4
					MySQL.Reader["fecha_expiracion"].ToString(), 	//5
					MySQL.Reader["razon"].ToString(), 				//6
					MySQL.Reader["disponibles"].ToString(), 		//7
					MySQL.Reader["ilimitado"].ToString(), 			//8
					MySQL.Reader["utilizados"].ToString()			//9
					);
			}
			
			Gtk.TreeIter iter = new Gtk.TreeIter();
			lsPerfilPase.GetIterFirst(out iter);
			cmbPerfilPase.SetActiveIter(iter);
		}
		
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
		protected void OnCmbPerfilPaseChanged (object sender, System.EventArgs e)
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close,"");
						
			Gtk.TreeIter iter = new Gtk.TreeIter();
			cmbPerfilPase.GetActiveIter(out iter);
			Console.WriteLine(lsPerfilPase.GetValue(iter,0) + " - " + lsPerfilPase.GetValue(iter,1) + " - " + lsPerfilPase.GetValue(iter,8));
			
			// Primero probar si aún hay disponibles, si no es ilimitado
			if ( lsPerfilPase.GetValue(iter,8).ToString() != "1" && int.Parse(lsPerfilPase.GetValue(iter,7).ToString()) <= int.Parse(lsPerfilPase.GetValue(iter,9).ToString()) )
			{
				Mensaje.Text="Este pérfil de pase esta agotado.";
				Mensaje.Title="No hay disponibilidad";
				Mensaje.Run();
			}
			
			// Si esta disponible entonces cambiar la UI
			
			// Cantidad de pases
			txtCantidadDeBoletos.Text = lsPerfilPase.GetValue(iter,2).ToString();
			
			// Días válidos
			chkDomingo.Active = lsPerfilPase.GetValue	(iter,3).ToString().Substring(0,1) == "1";
			chkLunes.Active = lsPerfilPase.GetValue		(iter,3).ToString().Substring(1,1) == "1";
			chkMartes.Active = lsPerfilPase.GetValue	(iter,3).ToString().Substring(2,1) == "1";
			chkMiercoles.Active = lsPerfilPase.GetValue	(iter,3).ToString().Substring(3,1) == "1";
			chkJueves.Active = lsPerfilPase.GetValue	(iter,3).ToString().Substring(4,1) == "1";
			chkViernes.Active = lsPerfilPase.GetValue	(iter,3).ToString().Substring(5,1) == "1";
			chkSabado.Active = lsPerfilPase.GetValue	(iter,3).ToString().Substring(6,1) == "1";
			
			// Precio individual
			txtPrecioBoletos.Text = lsPerfilPase.GetValue(iter,4).ToString();
			
			// Fecha expiración
			try {
				double DiasExpiracion = double.Parse(lsPerfilPase.GetValue(iter,5).ToString());
				calCaducidad.Date = DateTime.Now;
				calCaducidad.Date.AddDays(DiasExpiracion);
			} catch {
				try {calCaducidad.Date = DateTime.Parse(lsPerfilPase.GetValue(iter,5).ToString());} catch {}
			}
			
			// Razón
			txtRazonBoletos.Text = lsPerfilPase.GetValue(iter,6).ToString();
			
			//GC
			Mensaje.Destroy();
		}
	}
}

