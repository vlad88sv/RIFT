using System;
namespace Taquilla
{
	public partial class CorteZ : Gtk.Dialog
	{
		//MySQL DB = new MySQL();
		string FechaSQL = "";
		
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
		public CorteZ (string bFechaSQL)
		{
			this.Build ();
			
			this.FechaSQL = bFechaSQL;
			
			string fechaDiaTrabajoFMySQL = this.FechaSQL;
			MySQL.consultar(
						    "SELECT " +
						    "COALESCE((SELECT COALESCE(SUM(precio_grabado),0) FROM tickets WHERE ID_tipo_boleto NOT IN (2,3,4) AND DATE(fecha_vendido) = '" + fechaDiaTrabajoFMySQL + "'),0) AS totalJuegos," +
						    "COALESCE((SELECT SUM(`eventos`.`precio_evento` + `eventos`.`precio_comida` + `eventos`.`precio_cafeteria`) FROM `rift3`.`eventos` WHERE DATE(`eventos`.`fecha_vendido`)='"+fechaDiaTrabajoFMySQL+"'),0) AS totalEventos," +
							"COALESCE((SELECT SUM(precio_grabado*cantidad) FROM `cafeteria_transacciones` WHERE DATE(`cafeteria_transacciones`.`fecha`) = '"+fechaDiaTrabajoFMySQL+"'),0) AS totalCafeteria"
					         );
			MySQL.Reader.Read();
			
			double totalCafeteria = double.Parse(MySQL.Reader["totalCafeteria"].ToString());
			double totalJuegos = double.Parse(MySQL.Reader["totalJuegos"].ToString());
			double totalEventos = double.Parse(MySQL.Reader["totalEventos"].ToString());
			double TotalDelDia = totalCafeteria+totalEventos+totalJuegos;

			lblTotal.Text = "[COMBINADO: $" + TotalDelDia.ToString("0.00") + "] [JUEGO $" + totalJuegos.ToString("0.00") + "] [EVENTOS $" + totalEventos.ToString("0.00") + "] [CAFETERIA $" + totalCafeteria.ToString("0.00") + "]";
			
			string numero_de_compras = "0", total_de_compras = "0.00";
			if (MySQL.consultar("SELECT COUNT(*) AS 'numero_de_compras', COALESCE(SUM(total_compra),0.00) AS 'total_de_compras' FROM cafeteria_ingresos WHERE DATE(fechatiempo) = '"+fechaDiaTrabajoFMySQL+"'"))
			{
				if (MySQL.Reader.Read())
				{
					numero_de_compras = MySQL.Reader["numero_de_compras"].ToString();
					total_de_compras = MySQL.Reader["total_de_compras"].ToString();
				}
			}			
			txtNoCompras.Text = numero_de_compras;
			txtTotalCompras.Text = total_de_compras;
		}
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			string fechaDiaTrabajoFMySQL = this.FechaSQL;
			Impresora.Imprimidor impTiquete = new Impresora.Imprimidor();
			string Tiquete = "";
	
			Tiquete += impTiquete.Imprimir("**** CORTE Z ****",1);
			Tiquete += impTiquete.Imprimir("Corte de dia: "+fechaDiaTrabajoFMySQL,1);
			MySQL.consultar(
						    "SELECT " +
						    "COALESCE((SELECT COALESCE(SUM(precio_grabado),0) FROM tickets WHERE ID_tipo_boleto NOT IN (2,3,4) AND DATE(fecha_vendido) = '" + fechaDiaTrabajoFMySQL + "'),0) AS totalJuegos," +
						    "COALESCE((SELECT SUM(`eventos`.`precio_evento` + `eventos`.`precio_comida` + `eventos`.`precio_cafeteria`) FROM `rift3`.`eventos` WHERE DATE(`eventos`.`fecha_vendido`)='"+fechaDiaTrabajoFMySQL+"'),0) AS totalEventos," +
							"COALESCE((SELECT SUM(precio_grabado*cantidad) FROM `cafeteria_transacciones` WHERE DATE(`cafeteria_transacciones`.`fecha`) = '"+fechaDiaTrabajoFMySQL+"'),0) AS totalCafeteria"
					         );
			MySQL.Reader.Read();
			
			double totalCafeteria = double.Parse(MySQL.Reader["totalCafeteria"].ToString());
			double totalJuegos = double.Parse(MySQL.Reader["totalJuegos"].ToString());
			double totalEventos = double.Parse(MySQL.Reader["totalEventos"].ToString());
			double TotalDelDia = totalCafeteria+totalEventos+totalJuegos;
			Tiquete += impTiquete.Imprimir("Total:","$"+TotalDelDia.ToString("0.00"));
			Tiquete += impTiquete.Imprimir("Total [JUEGO]","$"+totalJuegos.ToString("0.00"));
			Tiquete += impTiquete.Imprimir("Total [EVENTOS]","$"+totalEventos.ToString("0.00"));
			Tiquete += impTiquete.Imprimir("Total [CAFETERIA]","$"+totalCafeteria.ToString("0.00"));
			Tiquete += impTiquete.Imprimir("T.Efectivo","$"+txtEfectivo.Text);
			Tiquete += impTiquete.Imprimir("T.POS","$"+txtCredito.Text);
			Tiquete += impTiquete.Imprimir("T.Cheque","$"+txtCheque.Text);
			Tiquete += impTiquete.Imprimir("T.Compras","$"+txtTotalCompras.Text);
			Tiquete += impTiquete.Imprimir("\nMonto a remesar:","$"+txtMontoRemesar.Text);			
			
			double diferencia = TotalDelDia - (double.Parse(txtEfectivo.Text) + double.Parse(txtCredito.Text) + double.Parse(txtCheque.Text) + double.Parse(txtTotalCompras.Text));
			string TextoDiferencia = "?";
			
			if (diferencia == 0.00)
				TextoDiferencia = "";
			else if (diferencia > 0.00)
				TextoDiferencia = "[falta]";
			else if (diferencia < 0.00)
				TextoDiferencia = "[sobra]";
			
			Tiquete += impTiquete.Imprimir("Diferencia: "+TextoDiferencia,"$"+diferencia.ToString("0.00"));
			Tiquete += impTiquete.Imprimir("Numero de compras:",txtNoCompras.Text);
			
			//Tiquete += impTiquete.Imprimir("Transacciones:",MySQL.Reader["transacciones"].ToString());
	
			Tiquete += impTiquete.Imprimir("\n+- Detalle transacciones -+",1);
			MySQL.consultar("SELECT COUNT(*) AS 'cuenta', `tipo` AS 'descripcion' FROM `transacciones` WHERE DATE(fecha) = '"+fechaDiaTrabajoFMySQL+"' GROUP BY tipo ORDER BY tipo");
			while (MySQL.Reader.Read()) {
				Tiquete += impTiquete.Imprimir("T:"+MySQL.Reader["descripcion"].ToString(), MySQL.Reader["cuenta"].ToString());
			}
	
			Tiquete += impTiquete.Imprimir("\n+- Detalle boletos -+",1);
			MySQL.consultar("SELECT COUNT(*) AS 'cuenta', `descripcion` FROM `tickets` LEFT JOIN `tipo_boleto` USING(ID_tipo_boleto) WHERE DATE(fecha_vendido) = '"+fechaDiaTrabajoFMySQL+"' GROUP BY ID_tipo_boleto ORDER BY descripcion");
			while (MySQL.Reader.Read()) {
				Tiquete += impTiquete.Imprimir("T:"+MySQL.Reader["descripcion"].ToString(), MySQL.Reader["cuenta"].ToString());
			}
			
			Tiquete += impTiquete.Imprimir("\n+- Detalle eventos -+",1);
			MySQL.consultar("SELECT `eventos`.`ID_evento`, (`eventos`.`precio_evento` + `eventos`.`precio_comida` + `eventos`.`precio_cafeteria`) AS precio_evento FROM `rift3`.`eventos` WHERE DATE(`eventos`.`fecha_vendido`)='"+fechaDiaTrabajoFMySQL+"' ORDER BY `eventos`.`hora_inicio`");
			while (MySQL.Reader.Read()) {
				Tiquete += impTiquete.Imprimir("E:"+MySQL.Reader["ID_evento"].ToString(), MySQL.Reader["precio_evento"].ToString());
			}
		
			
			Tiquete += impTiquete.Imprimir("\n+- Cafeteria [inventario]-+",1);
			MySQL.consultar("SELECT descripcion, (COALESCE((SELECT SUM(stock) FROM cafeteria_stock AS cs WHERE cs.ID_articulo=ca.ID_articulo),0) -  COALESCE((SELECT SUM(cantidad) FROM cafeteria_transacciones AS ct WHERE ct.ID_articulo=ca.ID_articulo),0)) AS disponibles FROM `cafeteria_articulos` AS ca ORDER BY `descripcion` ASC");
			while (MySQL.Reader.Read()) {
				Tiquete += impTiquete.Imprimir("[_______] == " + MySQL.Reader["disponibles"].ToString(), MySQL.Reader["descripcion"].ToString());
			}	
			
			Tiquete += impTiquete.Imprimir("\n+- Cafeteria [venta]-+",1);
			
			MySQL.consultar("SELECT ID_transaccion, ID_ticket, (COALESCE((SELECT costo FROM cafeteria_articulos WHERE cafeteria_articulos.ID_articulo = cafeteria_transacciones.ID_articulo),0)*cantidad) AS 'costo_total', (precio_grabado*cantidad) AS precio_total, DATE_FORMAT(fecha, '%r') AS hora, descripcion, cantidad FROM  `cafeteria_transacciones` LEFT JOIN  `cafeteria_articulos` USING ( ID_articulo ) WHERE DATE(fecha) ='"+fechaDiaTrabajoFMySQL+"' AND cantidad > 0 ORDER BY ID_transaccion ASC ");
		
			while (MySQL.Reader.Read())	
			{
				Tiquete += impTiquete.Imprimir(MySQL.Reader["hora"].ToString() + "," + MySQL.Reader["descripcion"].ToString() + "," + MySQL.Reader["cantidad"].ToString() + "," + "$" + MySQL.Reader["precio_total"].ToString(),1);
			}
	
			Tiquete += impTiquete.Imprimir("\n+- Detalle juegos -+",1);
			MySQL.consultar("SELECT DATE_FORMAT(fecha_juego,'%H:%i') AS 'hora_juego', COUNT(*) AS 'numero_jugadores' FROM rift3.tickets WHERE DATE(fecha_juego)='"+fechaDiaTrabajoFMySQL+"' AND ID_tipo_boleto NOT IN (2,3,4,11) GROUP BY DATE_FORMAT(fecha_juego,'%H:%i') ORDER BY fecha_juego");
			Tiquete += impTiquete.Imprimir("Hora", "No.", "Hora", "No.");
			string bCol1, bCol2, bCol3, bCol4;
			int biCol2=0,biCol4=0, biMaxCol2=0, biMaxCol4=0;
			int TotalJugadores=0, TotalMaxJugadores=0;
			
			while (MySQL.Reader.Read()) {
				bCol1 = MySQL.Reader["hora_juego"].ToString();
				bCol2 = MySQL.Reader["numero_jugadores"].ToString();
				biCol2 = int.Parse(MySQL.Reader["numero_jugadores"].ToString());
				biMaxCol2 = calcularMaximoJugadores(biCol2);
				
				if (MySQL.Reader.Read())
				{
					bCol3 = MySQL.Reader["hora_juego"].ToString();
					bCol4 = MySQL.Reader["numero_jugadores"].ToString();
					biCol4 = int.Parse(MySQL.Reader["numero_jugadores"].ToString());
					biMaxCol4 = calcularMaximoJugadores(biCol4);
				} else {
					bCol3 = "00:00";
					bCol4 = "0";
					biCol4 = 0;
					biMaxCol4 = 0;
				}
				TotalJugadores += biCol2+biCol4;
				TotalMaxJugadores += biMaxCol2+biMaxCol4;
				
				Tiquete += impTiquete.Imprimir(bCol1, bCol2, bCol3, bCol4);
			}
			
			Tiquete += "\n\n" + "Total jugadores: "+TotalJugadores+"\n" + "Total Max. jugadores: "+TotalMaxJugadores;

			Tiquete += "\n\n" + "F._________________________\n"+auth.nombre+"\n\n";
			
			impTiquete.Tiquete(Tiquete,"9999");
			impTiquete.Tiquete(Tiquete,"9999","PDF","CZ-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			Historial.Registrar ("Corte Z impreso");

		}
		private int calcularMaximoJugadores(int Jugadores)
		{
			switch (Jugadores) {
				case 0:
					return 0;
				case 1:
					return 2;
				case 2:
					return 4;
				case 3:
					return 4;
				case 4:
					return 4;
				case 5:
					return 6;
				case 6:
					return 6;
				case 7:
					return 8;
				case 8:
					return 8;
				case 9:
					return 10;
				case 10:
					return 10;
				case 11:
					return 12;
				case 12:
					return 12;
				case 13:
					return 13;
				default:
					return 0;
			}
		}	
		protected virtual void OnTxtEfectivoTextInserted (object o, Gtk.TextInsertedArgs args)
		{	
			CalcularRemesa ();
		}
		
		void CalcularRemesa ()
		{
			double Cheque = 0.00;
			double Efectivo = 0.00;
			
			if (double.TryParse(txtCheque.Text, out Cheque) && double.TryParse(txtEfectivo.Text, out Efectivo))
				txtMontoRemesar.Text = (Efectivo + Cheque).ToString("0.00");
		}
		protected virtual void OnTxtChequeTextInserted (object o, Gtk.TextInsertedArgs args)
		{
			CalcularRemesa ();
		}
		
		
	}
}

