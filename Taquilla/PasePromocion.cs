using System;
using Gtk;
namespace Taquilla
{
	public partial class PasePromocion : Gtk.Dialog
	{
		public Resultadotiquete Resultado = new Resultadotiquete();
		private string c = "";

		public static void msgBox (string title, string body)
		{
			MessageDialog mdmsgbox = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "");
			mdmsgbox.Text = body;
			mdmsgbox.Title = title;
			mdmsgbox.Modal = true;
			mdmsgbox.Run();
			mdmsgbox.Destroy();
		}
		
		public PasePromocion (bool Promocion)
		{
			this.Build ();
			if (Promocion)
				rdoPromocion.Active = true;
		}

		protected virtual bool CorroborarCodigo()
		{
			// Corroborar en tabla "codigo_promociones" si existe tal código y notificar al cajero de lo que producirá usarlo.
			c = "SELECT COUNT(*) AS encontrado, ID_tipo_boleto, subtipo, precio, porcentaje FROM tipo_boleto WHERE ID_tipo_boleto = (SELECT ID_tipo_boleto FROM codigo_promociones WHERE codigo_promociones.codigo_barra = '"+txtIngreso.Text+"' LIMIT 1)";
			
			if (!MySQL.consultar (c))
			{
				return false;
			}
	
			if (MySQL.Reader.Read() && MySQL.Reader["encontrado"].ToString() == "0")
			{
				msgBox ("Error", "Código de promoción no encontrado");
				return false;
			}
			
			Resultado.ID_tipo_boleto =  MySQL.Reader["ID_tipo_boleto"].ToString();
			
			if (MySQL.Reader["subtipo"].ToString() == "precio")
			{
				Resultado.precio_grabado = double.Parse(MySQL.Reader["precio"].ToString());
			}
			else
			{
				double Porcentaje = double.Parse(MySQL.Reader["porcentaje"].ToString());
				double dPrecioTiquete = tiquete.PrecioBase;
				Resultado.precio_grabado = (dPrecioTiquete*Porcentaje);
			}
			Resultado.error = false;
			Resultado.transaccion = "promocion";
			Resultado.extra = txtIngreso.Text;
			return true;
		}
		
		// Restricción para operación pase
		protected virtual bool CorroborarPase()
		{
			// ID no utilizado
			c = "SELECT COUNT(*) AS pase FROM tickets WHERE ID_pase='"+(txtIngreso.Text)+"'";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["pase"].ToString() != "0")
			{
				msgBox ("Error","Código de pase ya fue utilizado.");
				return false;
			}
	
			// Pase no existente
			c = "SELECT COUNT(*) AS pase FROM tickets WHERE ID_tipo_boleto=11 && ID_ticket='"+(txtIngreso.Text)+"'";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["pase"].ToString() == "0")
			{
				msgBox ("Error","no existe tal código de pase.");
				return false;
			}
			
			// Verifiquemos si no ha vencido
			c = "SELECT COUNT(*) AS pase FROM tickets WHERE ID_tipo_boleto=11 && ID_ticket='"+(txtIngreso.Text)+"' && ((pase_expiracion >= '"+global.fechaDiaTrabajoFMySQL+"') && (SUBSTRING(pase_dias_valido,("+global.diaNumero+"+1),1) = '1')) ";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["pase"].ToString() == "0")
			{
				msgBox ("Error:","este pase ya expiró o no es válido este día.");
				return false;
			}
			
			Resultado.ID_tipo_boleto = tiquete.ID_tipo_gratis;
			Resultado.precio_grabado = 0.00;
    		Resultado.transaccion = "pase";
	    	Resultado.extra = txtIngreso.Text;

			Resultado.transaccion = "promocion";
			return true;
		}
		
		protected virtual bool CorroborarEvento()
		{
			string c = "SELECT COUNT(*) AS existe,`ID_evento`,`certificado`, `duracion` FROM `eventos` WHERE `ID_evento` = '"+(txtIngreso.Text)+"' LIMIT 1";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["existe"].ToString() == "0")
			{
				msgBox ("Error:","este evento no existe.");
				return false;
			}

			if (MySQL.Reader["certificado"].ToString() == "2")
			{
				msgBox ("Error:","este certificado de evento ya fue utilizado.");
				return false;
			}
			
			Console.WriteLine("Certificado: " + MySQL.Reader["certificado"].ToString());
			
			if (MySQL.Reader["certificado"].ToString() != "1")
			{
				msgBox ("Error:","este no es un certificado de evento");
				return false;
			}
			
			
			// Todo bien, procedemos a actualizar el evento con fecha_evento = ahorita
			// Ademas inválidamos el certificado
			c = "UPDATE `eventos` SET `certificado`=2,`fecha_evento`='"+global.fechaDiaTrabajoFMySQL+"', hora_inicio='"+global.HoraJuegoFMySQL+"', hora_final='"+(DateTime.Parse(global.HoraJuego) + TimeSpan.Parse(MySQL.Reader["duracion"].ToString())).ToString("HH:mm") + ":00' WHERE `ID_evento`="+MySQL.Reader["ID_evento"].ToString() + " LIMIT 1";
			MySQL.consultar(c);
			
			// Solicitamos que se actualice la lista para que vean el evento, asi no
			// tenemos que escribir nuestro propio actualizador de lista y reinventar la rueda
			Resultado.ActualizarListaTiquetes = true;
			
			// Siempre retornamos false porque no queremos que se imprima ningun tiquete
			return false;
		}
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			bool valido = false;
			
			if (rdoPase.Active)
				valido = CorroborarPase();
			if (rdoPromocion.Active)
				valido = CorroborarCodigo();
			if (rdoEvento.Active)
				valido = CorroborarEvento();
			
			Respond (valido ? ResponseType.Ok : ResponseType.Close);
		}		
		
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			Respond (ResponseType.Close);
		}		
		
		[GLib.ConnectBeforeAttribute]
		protected virtual void OnTxtIngresoKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
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
		
		
	}
}