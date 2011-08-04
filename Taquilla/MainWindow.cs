using System;
using System.Globalization;
using Gtk;
using GLib;

public partial class MainWindow : Gtk.Window
{
	
	Impresora.Imprimidor impTiquete = new Impresora.Imprimidor();
	
	public enum Operacion
	{
		Normal,
		Movimiento,
		Cancelacion,
		Pase
		
	}

	public struct Promocion
	{
		public bool Activa ;
		public string CodigoPromocion;
		public string ID_tipo_boleto;
		public string subtipo;
		public string precio;
		public string porcentaje;
		public string representacion;
		public string infinito;
		public string cantidad;
		public string descripcion;
	}

	public Promocion promocion;

	Gtk.ListStore cmb = new Gtk.ListStore(typeof (string), //Código barra
	                                      typeof (string)  // Texto humano
	                                      );
	
	Gtk.ListStore tree = new Gtk.ListStore(typeof (string), //Hora
	                                       typeof (string), //e01
	                                       typeof (string), //e02
	                                       typeof (string), //e03
	                                       typeof (string), //e04
	                                       typeof (string), //e05
	                                       typeof (string), //e06
	                                       typeof (string), //e07
	                                       typeof (string), //e08
	                                       typeof (string), //e09
	                                       typeof (string), //e10
	                                       typeof (string), //e11
	                                       typeof (string), //e12
	                                       typeof (string), //e13
	                                       typeof (string), //Ocupados
	                                       typeof (string) 	//Disponibles
	                                       );
	
	Gtk.ListStore listaStore = new Gtk.ListStore (typeof (string), // ID artículo
	                                              typeof (int), // Cantidad
	                                              typeof (string), // Descripción
	                                              typeof (double)); // Precio
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		this.Build ();
		Iniciar();
		ActualizarInterfaz();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		if (auth.Autorizado)
		{
			// Registramos en el log el suceso
			Historial.Registrar("SESION FINALIZADA [TIPO=SALIDA]");
		}
		Application.Quit ();
		a.RetVal = true;
	}
	
	private void Iniciar()
	{
		this.Fullscreen();
		
		/* Proceso que inicia del programa */
		CrearTreeTiquetes();
		
		/* Reloj */
		GLib.Timeout.Add (1000, new GLib.TimeoutHandler (ActualizarInterfaz));
		
		hbControles2.Sensitive = false;
		controles.Sensitive = false;
		
		treeTiquetes.RulesHint = true;
		treeTiquetes.EnableGridLines = TreeViewGridLines.Both;
				
		txtCodigoPromocion.Entry.Activated += handleTxtCodigoPromocionActivated;
		CellRendererText textRenderer = new CellRendererText ();
		txtCodigoPromocion.PackStart (textRenderer, true); 
		txtCodigoPromocion.AddAttribute (textRenderer, "text", 1);
		txtCodigoPromocion.Model = cmb;
		
		cmb.AppendValues("003", "I.V.A Exento");

		// Cafetería
		cafeteria.ConstruirLista(ref tvLista, ref listaStore);		
		cafeteria.CargarListaCafeteria(ref listaStore);

	}
	
	bool ActualizarInterfaz()
	{
		lblHoraActual.Markup = "<span  color='red'>"+DateTime.Now.ToString("HH:mm:ss")+"</span>"; 
		lblHoraSiguienteJuego.Markup = "<span  color='red'>" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (15 + (DateTime.Now.Minute - (DateTime.Now.Minute % 15))) == 60 ? DateTime.Now.Hour + 1 : DateTime.Now.Hour, (15 + (DateTime.Now.Minute - (DateTime.Now.Minute % 15))) == 60 ? 0 : (15 + (DateTime.Now.Minute - (DateTime.Now.Minute % 15)))  , 0).ToString("HH:mm:ss") + "</span>";
		lblFechaActual.Markup = "<span color='red'>"+DateTime.Now.ToString("D")+"</span>";
		return true;
	}
	
	private void CrearTreeTiquetes()
	{
		treeTiquetes.Model = tree;	
		treeTiquetes.AppendColumn("Inicio",new Gtk.CellRendererText(),"markup",0);
		treeTiquetes.AppendColumn("01",new Gtk.CellRendererText(),"markup",1);
		treeTiquetes.AppendColumn("02",new Gtk.CellRendererText(),"markup",2);
		treeTiquetes.AppendColumn("03",new Gtk.CellRendererText(),"markup",3);
		treeTiquetes.AppendColumn("04",new Gtk.CellRendererText(),"markup",4);
		treeTiquetes.AppendColumn("05",new Gtk.CellRendererText(),"markup",5);
		treeTiquetes.AppendColumn("06",new Gtk.CellRendererText(),"markup",6);
		treeTiquetes.AppendColumn("07",new Gtk.CellRendererText(),"markup",7);
		treeTiquetes.AppendColumn("08",new Gtk.CellRendererText(),"markup",8);
		treeTiquetes.AppendColumn("09",new Gtk.CellRendererText(),"markup",9);
		treeTiquetes.AppendColumn("10",new Gtk.CellRendererText(),"markup",10);
		treeTiquetes.AppendColumn("11",new Gtk.CellRendererText(),"markup",11);
		treeTiquetes.AppendColumn("12",new Gtk.CellRendererText(),"markup",12);
		treeTiquetes.AppendColumn("13",new Gtk.CellRendererText(),"markup",13);
		treeTiquetes.AppendColumn("O",new Gtk.CellRendererText(),"markup",14);
		treeTiquetes.AppendColumn("D",new Gtk.CellRendererText(),"markup",15);
	}	

	private void CargarTiquetesDelDia()
	{
		double benchmark = DateTime.Now.TimeOfDay.TotalMilliseconds;
		
		// Desprendemos el modelo del treeTiquetes, para hacer que se actualice solo al final
		treeTiquetes.Visible = false;
		treeTiquetes.Model = null;
		
		string fechaDiaTrabajoFMySQL = calDiaTrabajo.Date.ToString("yyyy-MM-dd");
		
		lblFechaSeleccionada.Markup = "<span color='red'>"+calDiaTrabajo.Date.ToString("D")+"</span>";
		
		Gtk.TreeIter iter = new Gtk.TreeIter();
		
		// Reiniciemos el TreeTiquetes
		tree.Clear();
		
		for (int h = 0; h < 65; h++)
				tree.AppendValues (DateTime.Parse("08:00").AddMinutes(15.0*(double)h).ToString("hh:mm tt",CultureInfo.InvariantCulture),"-","-","-","-","-","-","-","-","-","-","-","-","-","0","13");
						
		string c = "SELECT `fecha_juego`, DATE_FORMAT(`fecha_juego`,'%h:%i %p') AS `hora`, `numero_jugador`, `representacion` FROM `tickets` LEFT JOIN `tipo_boleto` USING(`ID_tipo_boleto`) WHERE DATE(`fecha_juego`) = '" + fechaDiaTrabajoFMySQL + "' AND ID_tipo_boleto <> 11 ORDER BY `fecha_juego` ASC, `numero_jugador` ASC";
		if (!MySQL.consultar(c))
			return;
		
		Console.WriteLine("S0:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		tree.GetIterFirst(out iter);	
		
		while (MySQL.Reader.Read())
		{
			Console.WriteLine("Hora-> |"+ tree.GetValue(iter, 0).ToString() + "|" + MySQL.Reader["hora"].ToString() + "| No. Jugador->"+MySQL.Reader["numero_jugador"].ToString());
			
			// Encontremos la primera hora para partir de ahi				
			while (tree.GetValue(iter, 0).ToString() != MySQL.Reader["hora"].ToString())
			{
				tree.IterNext(ref iter);
			}
			// Encontramos la hora(?). Avanzar el lector hasta que ya no.
			if (tree.GetValue(iter, 0).ToString() == MySQL.Reader["hora"].ToString())
				tree.SetValue(iter, int.Parse(MySQL.Reader["numero_jugador"].ToString()), MySQL.Reader["representacion"].ToString());			
		}

		Console.WriteLine("S1:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		// Eventos para hoy?. Si por favor.
		c = "SELECT `eventos`.`ID_evento`, `eventos`.`precio_evento`, `eventos`.`precio_comida`, `eventos`.`precio_cafeteria`, `eventos`.`patrocinado_por`, `eventos`.`nombre_evento`, `eventos`.`notas`, `eventos`.`fecha_evento`,DATE_FORMAT(`eventos`.`hora_inicio`,'%h:%i %p') AS 'fhora_inicio', DATE_FORMAT(`eventos`.`hora_final`,'%h:%i %p') AS 'fhora_final', `eventos`.`agregado_por_usuario` FROM `rift3`.`eventos` WHERE `eventos`.`fecha_evento`='"+fechaDiaTrabajoFMySQL+"' ORDER BY `eventos`.`hora_inicio`";
		MySQL.consultar(c);		

		tree.GetIterFirst(out iter);
		// Recorremos un evento a la vez
		while (MySQL.Reader.Read()) {
			// Encontremos la hora de inicio del evento
			while (tree.GetValue(iter, 0).ToString() != MySQL.Reader["fhora_inicio"].ToString()) {
				tree.IterNext(ref iter);
			}

			while (tree.GetValue(iter, 0).ToString() != MySQL.Reader["fhora_final"].ToString()) {
				for(int i = 1; i < 14; i++)
				{
					tree.SetValue(iter, i, "E");
				}
				tree.IterNext(ref iter);
			}
		}
		
		Console.WriteLine("S2:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		c = "SELECT COUNT(*) AS 'pases' FROM `tickets` WHERE DATE(`fecha_vendido`) = '" + fechaDiaTrabajoFMySQL + "' AND ID_tipo_boleto = 11";
		if (MySQL.consultar(c))
		{
			MySQL.Reader.Read();
			lblPasesGenerados.Markup = "<span color='red'>"+MySQL.Reader["pases"].ToString()+"</span>";
		} else {
			lblPasesGenerados.Markup = "<span color='red'>0</span>";
		}
		
		Console.WriteLine("S3:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		// Ya cargó todos los tiquetes, calcular disponibilidades.
		tree.GetIterFirst(out iter);
		do {
			CalcularDisponibilidad(iter);
		} while (tree.IterNext(ref iter));
		
		Console.WriteLine("S4:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		MostrarPrecioBase();
		
		// Restauramos la visibilidad de la lista (offscreen rendering)
		treeTiquetes.Model = tree;
		treeTiquetes.Visible = true;
		Console.WriteLine("S5:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		treeTiquetes.SetCursor(new Gtk.TreePath("60"), treeTiquetes.Columns[0], false);
		treeTiquetes.SetCursor(new Gtk.TreePath("28"), treeTiquetes.Columns[0], false);
	}

	void MostrarPrecioBase ()
	{
		// Precio base del día
		string fechaDiaTrabajoFMySQL = calDiaTrabajo.Date.ToString("yyyy-MM-dd"), PrecioTiquete = "", c = "";
		c = "SELECT precio FROM precios_diarios WHERE dia = DATE_FORMAT('"+fechaDiaTrabajoFMySQL+"','%w')";
		MySQL.consultar(c);
		MySQL.Reader.Read();
		PrecioTiquete = MySQL.Reader["precio"].ToString();
		
		// Vemos si aplica promocion
		if (promocion.Activa)
		{
			if (promocion.subtipo == "precio")
			{
				PrecioTiquete = promocion.precio;
			}
			else
			{
				double Porcentaje = double.Parse(promocion.porcentaje);
				double dPrecioTiquete = double.Parse(PrecioTiquete);
				PrecioTiquete = (dPrecioTiquete*Porcentaje).ToString("0.00");
			}
		}
		lblPrecioDia.Markup = "<span color='red'>$"+PrecioTiquete+"</span>";
		lblPromocionActivada.Markup = "<span color='red'>"+promocion.descripcion+"</span>";
	}
	protected virtual void OnCalDiaTrabajoDaySelected (object sender, System.EventArgs e)
	{
		CargarTiquetesDelDia();
	}
	
	protected virtual void OnTreeTiquetesKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
	{
		
		Gtk.TreeIter iter = new Gtk.TreeIter();
		treeTiquetes.Selection.GetSelected(out iter);
		string fechaDiaTrabajoFMySQL = calDiaTrabajo.Date.ToString("yyyy-MM-dd");
		string fechaDiaTrabajoMasJuegoFMySQL = calDiaTrabajo.Date.ToString("yyyy-MM-dd") + " " + DateTime.Parse(treeTiquetes.Model.GetValue(iter,0).ToString()).ToString("HH:mm");
		string fechaDiaTrabajoMasJuego = calDiaTrabajo.Date.ToString("yyyy-MM-dd") + " " + treeTiquetes.Model.GetValue(iter,0).ToString();
		string TipoTransaccion = "normal";
		string diaNumero = "";
		string Representacion = "<b><span color='red'>+</span></b>";
		
		if (args.Event.Key == Gdk.Key.T)
		{
			MySQL.consultar("SELECT (COALESCE(SUM(precio_grabado),0)+COALESCE((SELECT SUM(`eventos`.`precio_evento` + `eventos`.`precio_comida` + `eventos`.`precio_cafeteria`) FROM `rift3`.`eventos` WHERE DATE(`eventos`.`fecha_evento`)='"+fechaDiaTrabajoFMySQL+"'),0)) AS total FROM tickets WHERE DATE(fecha_juego) = '"+fechaDiaTrabajoFMySQL+"'");
			MySQL.Reader.Read();
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Total juegos vendidos para hoy (contando reservaciones de dias anteriores)\n$"+MySQL.Reader["total"].ToString());
			Mensaje.Title="Total vendido";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}

		// Dinero recibido este día (ventas realizadas hoy para cualquier fecha)
		if (args.Event.Key == Gdk.Key.t)
		{
			MySQL.consultar("SELECT (COALESCE(SUM(precio_grabado),0)+COALESCE((SELECT SUM(`eventos`.`precio_evento` + `eventos`.`precio_comida` + `eventos`.`precio_cafeteria`) FROM `rift3`.`eventos` WHERE DATE(`eventos`.`fecha_vendido`)='"+fechaDiaTrabajoFMySQL+"'),0)) AS total FROM tickets WHERE DATE(fecha_vendido) = '"+fechaDiaTrabajoFMySQL+"'");
			MySQL.Reader.Read();
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Dinero recibido en este dìa (ventas realizadas hoy, para juegos en cualquier fecha)\n$"+MySQL.Reader["total"].ToString());
			Mensaje.Title="Total ingresos";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}	
		
		/**************************************************************************************/
		
		uint colPos = 0;
		if (!((args.Event.KeyValue >= 65470 && args.Event.KeyValue <= 65481) || args.Event.Key == Gdk.Key.KP_Add))
			return;
		
		if (args.Event.Key == Gdk.Key.KP_Add)
			colPos = 13;
		else
			colPos = args.Event.KeyValue - 65469; 

		string c = "", ID_ticket = "", PrecioTiquete = "", TipoBoleto = "1";
		Operacion TipoOperacion = Operacion.Normal;
		
		// Precio base del día
		c = "SELECT precio, DATE_FORMAT('"+fechaDiaTrabajoFMySQL+"','%w') AS 'diaN' FROM precios_diarios WHERE dia = DATE_FORMAT('"+fechaDiaTrabajoFMySQL+"','%w')";
		MySQL.consultar(c);
		MySQL.Reader.Read();
		PrecioTiquete = MySQL.Reader["precio"].ToString();
		diaNumero = MySQL.Reader["diaN"].ToString();

		// Vemos si aplica promocion
		if (promocion.Activa)
		{
			TipoBoleto = promocion.ID_tipo_boleto;
			if (promocion.subtipo == "precio")
			{
				PrecioTiquete = promocion.precio;
			}
			else
			{
				double Porcentaje = double.Parse(promocion.porcentaje);
				double dPrecioTiquete = double.Parse(PrecioTiquete);
				PrecioTiquete = (dPrecioTiquete*Porcentaje).ToString("0.00");
			}
			promocion.Activa = false;
			txtCodigoPromocion.Entry.Text = "";
			TipoTransaccion = "promo."+promocion.descripcion;
			Representacion = promocion.representacion;
		}
		
		// Encontramos el tipo de operación:
		if (txtClaveCancelacion.Text != "" && txtIMovimientoDeTiquete.Text != "")
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Error: ¿movimiento o cancelación?.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}
		if (txtClaveCancelacion.Text != "")
			TipoOperacion = Operacion.Cancelacion;
		else if (txtIMovimientoDeTiquete.Text != "")
			TipoOperacion = Operacion.Movimiento;
		else if (txtPase.Text != "")
			TipoOperacion = Operacion.Pase;
		else
			TipoOperacion = Operacion.Normal;

		// Han pasado mas de 30 minutos desde la hora de juego deseada a la actual?
		int TiempoAdecuado = DateTime.Parse(fechaDiaTrabajoMasJuego,CultureInfo.InvariantCulture).CompareTo(DateTime.Now.AddMinutes(-30.00));
		TiempoAdecuado = 1;
		
		// Restricción para operación Normal/Pase
		if((TipoOperacion == MainWindow.Operacion.Normal || TipoOperacion == MainWindow.Operacion.Pase) && (treeTiquetes.Model.GetValue(iter, (int)colPos).ToString() != "-" && treeTiquetes.Model.GetValue(iter, (int)colPos).ToString() != "C"))
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, este tiquete ya esta vendido.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}

		// Restricción para operación Normal - Ya se pasó la hora de venta
		else if(TipoOperacion == MainWindow.Operacion.Normal && TiempoAdecuado == -1)
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, ya no se puede vender para esta hora (ya pasó mas de 30 minutos).");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}

		// Restricción para operación cancelación
		else if(TipoOperacion == MainWindow.Operacion.Cancelacion && treeTiquetes.Model.GetValue(iter, (int)colPos).ToString() == "-")
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede cancelar. No esta vendido.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}

		// Restricción para operación cancelación - un tiquete cancelado no se puede volver a cancelar
		else if(TipoOperacion == MainWindow.Operacion.Cancelacion && treeTiquetes.Model.GetValue(iter, (int)colPos).ToString() == "C")
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede cancelar. Ya esta cancelado.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}

		// Restricción para operación cancelación - un tiquete de evento no puede ser cancelado unitariamente
		else if(TipoOperacion == MainWindow.Operacion.Cancelacion && treeTiquetes.Model.GetValue(iter, (int)colPos).ToString() == "E")
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede cancelar. Es un tiquete de evento.\nTendrá que modificar el evento si es necesario cambiar algo de esta fila de juego.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}

		// Restricción para operación pase
		else if(TipoOperacion == MainWindow.Operacion.Pase)
		{
			// ID no utilizado
			c = "SELECT COUNT(*) AS pase FROM tickets WHERE ID_pase='"+(txtPase.Text)+"'";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["pase"].ToString() != "0")
			{
				txtPase.Text = "";
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, este pase ya fue utilizado.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}
	
			// Pase no existente
			c = "SELECT COUNT(*) AS pase FROM tickets WHERE ID_tipo_boleto=11 && ID_ticket='"+(txtPase.Text)+"'";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["pase"].ToString() == "0")
			{
				txtPase.Text = "";
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no existe tal pase.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}
		}

		// Restricción para operación cancelación - clave
		else if(TipoOperacion == MainWindow.Operacion.Cancelacion)
		{
			c = "SELECT COUNT(*) AS autorizados FROM usuarios WHERE clave=SHA1('"+txtClaveCancelacion.Text+"') AND nivel='gerente'";
			MySQL.consultar(c);
			MySQL.Reader.Read();
			
			if (MySQL.Reader["autorizados"].ToString() == "0")
			{
				txtClaveCancelacion.Text = "";
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede cancelar. Esta clave no esta registrada.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}
		}

		// Restricción para operación movimiento/cancelación
		else if(TipoOperacion == MainWindow.Operacion.Movimiento && treeTiquetes.Model.GetValue(iter, (int)colPos).ToString() != "-")
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede mover para el puesto especificado. Ya esta ocupado.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}
		
		// Ok esta desocupado, verifiquemos lo que quiere mover...
		else if (TipoOperacion == MainWindow.Operacion.Movimiento || TipoOperacion == MainWindow.Operacion.Cancelacion)
		{
			string CodigoTiquete = txtIMovimientoDeTiquete.Text;
			
			// Comprobemos que exista ese tiquete
			MySQL.consultar ("SELECT COUNT(*) AS cuenta FROM tickets WHERE ID_ticket="+CodigoTiquete);
			MySQL.Reader.Read();
			if (MySQL.Reader["cuenta"].ToString() == "0"  )
			{
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Operación inválida, código de tiquete no encontrado");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				return;
			}
		}
		
		// Ya todo terminó
		switch (TipoOperacion)
		{
			case Operacion.Pase:
				// Registramos en el log el suceso
				Historial.Registrar("Pase utilizado: "+txtPase.Text + " -> " + fechaDiaTrabajoMasJuegoFMySQL + " :: " + colPos.ToString());
		
				// Insertarlo como ticket gratis
				c = "INSERT INTO tickets (`fecha_juego`,`numero_jugador`,`precio_grabado`,`fecha_vendido`,`ID_tipo_boleto`, `ID_pase`) VALUES('"+fechaDiaTrabajoMasJuegoFMySQL+"', " + colPos  + " , '0.00', NOW(), 5, '" + txtPase.Text + "')";
				MySQL.consultar(c);

				if (MySQL.consultar("SELECT ID_ticket FROM tickets WHERE `fecha_juego` = '"+fechaDiaTrabajoMasJuegoFMySQL + "' AND numero_jugador = " + colPos.ToString() ))
				{
					MySQL.Reader.Read();
					ID_ticket = MySQL.Reader["ID_ticket"].ToString();
				}
				Representacion = "G";
				txtPase.Text = "";
			break;		
			
			case Operacion.Movimiento:
				// Registramos en el log el suceso
				Historial.Registrar("Ticket movido: "+txtIMovimientoDeTiquete.Text + " -> " + fechaDiaTrabajoMasJuegoFMySQL + " :: " + colPos.ToString());
		
				// Modificar la hora del tiquete
				MySQL.consultar("UPDATE tickets SET fecha_juego='"+fechaDiaTrabajoMasJuegoFMySQL+"', numero_jugador='"+colPos+"' WHERE ID_ticket="+txtIMovimientoDeTiquete.Text);
			
				txtIMovimientoDeTiquete.Text = "";
			break;
			
			case Operacion.Normal:
				// Registramos en el log el suceso
				Historial.Registrar("Ticket vendido");

				c = "INSERT INTO tickets (`fecha_juego`,`numero_jugador`,`precio_grabado`,`fecha_vendido`,`ID_tipo_boleto`) VALUES('"+fechaDiaTrabajoMasJuegoFMySQL+"', " + colPos  + " , '"+PrecioTiquete+"', NOW(), "+TipoBoleto+") ON DUPLICATE KEY UPDATE `fecha_juego`=VALUES(fecha_juego),`numero_jugador`=VALUES(numero_jugador),`precio_grabado`=VALUES(precio_grabado),`fecha_vendido`=VALUES(fecha_vendido), `ID_tipo_boleto`=VALUES(ID_tipo_boleto)";
				MySQL.consultar(c);

				if (MySQL.consultar("SELECT ID_ticket FROM tickets WHERE `fecha_juego` = '"+fechaDiaTrabajoMasJuegoFMySQL + "' AND numero_jugador = " + colPos.ToString() ))
				{
					MySQL.Reader.Read();
					ID_ticket = MySQL.Reader["ID_ticket"].ToString();
				}
			break;
								
			case Operacion.Cancelacion:
				// Registramos en el log el suceso
				Historial.Registrar("Ticket CANCELADO :: Juego->" + fechaDiaTrabajoMasJuegoFMySQL + " | No. equipo->" + colPos.ToString());		

				MySQL.consultar("UPDATE tickets SET ID_tipo_boleto=2, precio_grabado=0.00 WHERE `fecha_juego` = '"+fechaDiaTrabajoMasJuegoFMySQL + "' AND numero_jugador = " + colPos.ToString());
				treeTiquetes.Model.SetValue(iter, (int)colPos,"-");
				txtClaveCancelacion.Text = "";
			break;
		}

		string Tiquete = "";
		
		// Ahora vamos con la impresion y otras cositas
		switch (TipoOperacion)
		{
			case MainWindow.Operacion.Movimiento:
				CargarTiquetesDelDia();
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Movimiento exitoso. No olvide sellar el tiquete y marcar la nueva hora.");
				Mensaje.Title="Éxito";
				Mensaje.Run();
				Mensaje.Destroy();
			break;
				
			case MainWindow.Operacion.Cancelacion:
				CargarTiquetesDelDia();
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Cancelado con éxito.");
				Mensaje.Title="Éxito";
				Mensaje.Run();
				Mensaje.Destroy();
			break;
				
			case Operacion.Normal:
							
				Tiquete += impTiquete.Imprimir("FECHA DE JUEGO:",calDiaTrabajo.Date.ToString("dd/MM/yyyy"));
				Tiquete += impTiquete.Imprimir("HORA DE JUEGO:", DateTime.Parse(treeTiquetes.Model.GetValue(iter,0).ToString()).ToString("hh:mm tt",CultureInfo.InvariantCulture));
			
				//Tiquete += impTiquete.Imprimir("EQUIPO:",(colPos < 7 ?"AZUL" : "ROJO"));
				//Tiquete += impTiquete.Imprimir("JUGADOR:", colPos.ToString());
			
				Tiquete += "\n";
				Tiquete += impTiquete.Imprimir("1 Juego RIFT", "$" + PrecioTiquete + " G"); 
				//Tiquete += impTiquete.Imprimir("SUBTOTAL", PrecioTiquete + " G"); 
				//Tiquete += impTiquete.Imprimir("TOTAL", PrecioTiquete + " G");
				/*
				Tiquete += "\n\n" +
						   "Nombre:_________________________\n";
				Tiquete += "ID:_____________________________\n";
				*/
				//Console.WriteLine(Tiquete);
				impTiquete.Tiquete(Tiquete,ID_ticket);
			break;
			
			case Operacion.Pase:
		
				Tiquete += impTiquete.Imprimir("FECHA DE JUEGO:",calDiaTrabajo.Date.ToString("dd/MM/yyyy"));
				Tiquete += impTiquete.Imprimir("HORA DE JUEGO:",treeTiquetes.Model.GetValue(iter,0).ToString());
			
				Tiquete += impTiquete.Imprimir("EQUIPO:",(colPos < 7 ?"AZUL" : "ROJO"));
				Tiquete += impTiquete.Imprimir("JUGADOR:", colPos.ToString());
			
				Tiquete += impTiquete.Imprimir("CODIGO:", ID_ticket);			
				Tiquete += "\n";
				Tiquete += impTiquete.Imprimir("PASE UTILIZADO","PASE UTILIZADO");
				impTiquete.Tiquete(Tiquete,ID_ticket);
				break;

		}
		
		// Paso todas las restricciones y operaciones en DB, actualizar el treeTiquetes
		switch (TipoOperacion)
		{
			case Operacion.Normal:
			case Operacion.Pase:
				treeTiquetes.Model.SetValue(iter, (int)colPos, Representacion);
				CalcularDisponibilidad(iter);
			break;
			case Operacion.Movimiento:	
				TipoTransaccion = "movimiento";
				CargarTiquetesDelDia();
			break;
			case Operacion.Cancelacion:
				TipoTransaccion = "cancelacion";
				treeTiquetes.Model.SetValue(iter, (int)colPos, "C");
			break;	
		}
		Historial.Transaccion(TipoTransaccion+":"+diaNumero);
	}
	
	private void CalcularDisponibilidad(Gtk.TreeIter iter)
	{
		int Ocupados = 0;
		for(int i = 1; i < 14; i++)
		{
			if (tree.GetValue(iter, i).ToString() != "-" && tree.GetValue(iter, i).ToString() != "C")
				Ocupados++;
		}
		tree.SetValue(iter, 14, Ocupados.ToString());
		tree.SetValue(iter, 15, (13-Ocupados).ToString());
	}
		
	protected virtual void OnCmbCorteZClicked (object sender, System.EventArgs e)
	{	
		Taquilla.CorteZ cortez = new Taquilla.CorteZ(calDiaTrabajo.Date.ToString("yyyy-MM-dd"));
		
		cortez.Show();
	}
	
	protected virtual void OnCumpleanosClicked (object sender, System.EventArgs e)
	{
		Taquilla.Eventos eventos = new Taquilla.Eventos();
		eventos.Show();
	}
	
	protected virtual void OnRegistroClicked (object sender, System.EventArgs e)
	{
		Taquilla.Registro registro = new Taquilla.Registro(calDiaTrabajo.Date.ToString("yyyy-MM-dd"));
		registro.Show();
	}
	
	protected virtual void CorroborarCodigo()
	{
		MessageDialog Mensaje = null;
		// Corroborar en tabla "codigo_promociones" si existe tal código y notificar al cajero de lo que producirá usarlo.
		string c = "SELECT `ID_tipo_boleto`, `descripcion`, `subtipo`, `precio`, `porcentaje`, `representacion`, `cantidad`, `infinito` FROM codigo_promociones LEFT JOIN tipo_boleto USING(ID_tipo_boleto) WHERE `codigo_barra` = '"+txtCodigoPromocion.ActiveText+"' LIMIT 1";
		MySQL.consultar (c);
		if (!MySQL.Reader.Read())
		{
			Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Código de promoción no encontrado");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			promocion.descripcion = "";
			promocion.CodigoPromocion = "";
			promocion.cantidad = "";
			promocion.ID_tipo_boleto = "";
			promocion.infinito = "";
			promocion.porcentaje = "";
			promocion.precio = "";
			promocion.representacion = "";
			promocion.subtipo = "";
			promocion.Activa = false;
			MostrarPrecioBase();
			return;
			
		}

		Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Descripción de cupón: "+MySQL.Reader["descripcion"].ToString()+"\n"+"Cantidad cupones disponibles: "+MySQL.Reader["cantidad"].ToString()+"\n");
		Mensaje.Title="Promoción encontrada";
		Mensaje.Run();
		Mensaje.Destroy();
		
		promocion.descripcion = MySQL.Reader["descripcion"].ToString();
		promocion.CodigoPromocion = txtCodigoPromocion.ActiveText;
		promocion.cantidad = MySQL.Reader["cantidad"].ToString();
		promocion.ID_tipo_boleto = MySQL.Reader["ID_tipo_boleto"].ToString();
		promocion.infinito = MySQL.Reader["infinito"].ToString();
		promocion.porcentaje = MySQL.Reader["porcentaje"].ToString();
		promocion.precio = MySQL.Reader["precio"].ToString();
		promocion.representacion = MySQL.Reader["representacion"].ToString();
		promocion.subtipo = MySQL.Reader["subtipo"].ToString();
		promocion.Activa = true;
		MostrarPrecioBase();
	}
	
	protected virtual void OnCmdCancelarPromoClicked (object sender, System.EventArgs e)
	{
		promocion.Activa = false;
		txtCodigoPromocion.Entry.Text = "";
	}
	
	protected virtual void OnCmdActualizarClicked (object sender, System.EventArgs e)
	{
		CargarTiquetesDelDia();
	}
	
	void handleTxtCodigoPromocionActivated(object sender, System.EventArgs e)
	{
		CorroborarCodigo();
	}	
	
	protected virtual void OnCmdEstadisticasClicked (object sender, System.EventArgs e)
	{
		Taquilla.Estadísticas estadisticas = new Taquilla.Estadísticas();
		
		estadisticas.Show();
	}
	
	protected virtual void OnCmdVerEventosClicked (object sender, System.EventArgs e)
	{
		Taquilla.VisorEventos visoreventos = new Taquilla.VisorEventos(calDiaTrabajo.Date.ToString("yyyy-MM-dd"));
		
		visoreventos.Show();
	}	
	protected virtual void OnCmdSesionClicked (object sender, System.EventArgs e)
	{
		string c = "";
		if (auth.Autorizado == true)
		{
			Historial.Registrar ("SESION FINALIZADA");
			cmdSesion.Label = "Iniciar";
			auth.Autorizado = false;
			auth.ID_usuario = "0";
			hbControles2.Sensitive = false;
			controles.Sensitive = false;
			Cafeteria.Sensitive = false;
			hbControles.Sensitive = true;
			return;
		}
		
		c = "SELECT COUNT(*) AS autorizados, ID_usuario, usuario, nombre FROM usuarios WHERE usuario='"+txtUsuario.Text+"' AND clave=SHA1('"+txtClave.Text+"')";
		if (MySQL.consultar(c))
		{
			MySQL.Reader.Read();
			
			if (MySQL.Reader["autorizados"].ToString() == "0")
			{
				txtClaveCancelacion.Text = "";
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede autorizar. Esta clave no esta registrada.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
				auth.ID_usuario = "0";
				auth.Autorizado = false;
				auth.nombre = "";
				return;
			}
	
			txtUsuario.Text = "";
			txtClave.Text = "";
			auth.ID_usuario = MySQL.Reader["ID_usuario"].ToString();
			auth.Autorizado = true;
			auth.nombre = MySQL.Reader["nombre"].ToString();
			hbControles.Sensitive = false;
			hbControles2.Sensitive = true;
			controles.Sensitive = true;
			Cafeteria.Sensitive = true;
			cmdSesion.Label = "Finalizar";
			
			// Carguemos los tiquetes
			CargarTiquetesDelDia();
			
			// Registramos en el log el suceso
			Historial.Registrar("SESION INICIADA");
		}
	}
	protected virtual void OnPasesClicked (object sender, System.EventArgs e)
	{
		Taquilla.pases Pases = new Taquilla.pases();
		
		Pases.Show();
	}


	
	protected virtual void OnTvListaKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
	{
		Console.WriteLine(args.Event.KeyValue);
		
		Gtk.TreeIter iter;
		if (!tvLista.Selection.GetSelected (out iter))
			return;
		
		int CantidadActual = int.Parse(tvLista.Model.GetValue (iter, cafeteria.tvLista_Col_Cantidad).ToString());
		
		switch (args.Event.KeyValue)
		{
			// Aumentar la cantidad del producto
			case 65451:
				tvLista.Model.SetValue(iter,cafeteria.tvLista_Col_Cantidad,++CantidadActual);
				break;
			
			// Disminuir la cantidad de producto
			case 65453:
				if (CantidadActual>0)
					tvLista.Model.SetValue(iter,cafeteria.tvLista_Col_Cantidad,--CantidadActual);
				break;
			
			//Eliminar el producto de la lista
			case 65535:
				listaStore.Remove(ref iter);
			break;
		}
		
		CalcularVentaTotal();
		CalcularVuelto();
	}
	
	
	protected virtual void OnTxtEfectivoChanged (object sender, System.EventArgs e)
	{
		CalcularVuelto();
	}
	
	protected virtual void OnBtnCancelarClicked (object sender, System.EventArgs e)
	{
		cafeteria.CargarListaCafeteria(ref listaStore);
	}
	
	protected virtual void OnBtnImprimirClicked (object sender, System.EventArgs e)
	{
		long ID_ticket = DateTime.Now.Ticks;
		string tiquete = "";
		
		if (txtEfectivo.Text == "")
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Error: por favor especifíque la cantidad en efectivo recibida.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			txtEfectivo.GrabFocus();
			return;
		}
		
		TreeIter iter = new TreeIter();
		double Total = 0.00;
		double Precio = 0.00;
		
		if (tvLista.Model.GetIterFirst(out iter)) {
			do {
				if (tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString() != "0")
				{
					MySQL.consultar("INSERT INTO cafeteria_transacciones (`ID_articulo`,  `cantidad`, `precio_grabado`, `fecha`, `ID_ticket`) " +
						"VALUES("+
					             tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_ID_articulo).ToString()+","+
					             tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString()+","+
					             tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Precio).ToString()+","+
					             "NOW(),"+
					             ID_ticket+
					    ")");
					
					Precio = double.Parse(tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Precio).ToString());
					tiquete += impTiquete.Imprimir(tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString() + " " + tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Descripcion).ToString(), "$"+Precio.ToString("0.00"));
					Total += (double)(int.Parse(tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString()) * double.Parse(tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Precio).ToString()));
				}
			} while(tvLista.Model.IterNext(ref iter));
		}
		
		// Verificamos si compró algo
		
		if (Total == 0.00)
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Error: por favor marque un producto.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			txtEfectivo.GrabFocus();
			return;
		}
		
		tiquete += impTiquete.Imprimir("TOTAL:","$"+Total.ToString("0.00"));
		tiquete += impTiquete.Imprimir("\n",1);
	    tiquete += impTiquete.Imprimir("Efectivo", "$" + double.Parse(txtEfectivo.Text).ToString("0.00"));
		tiquete += impTiquete.Imprimir("Cambio:", "$" + (double.Parse(txtEfectivo.Text) - CalcularVentaTotal()).ToString("0.00"));

		impTiquete.Tiquete(tiquete,"0000");
		
		listaStore.Clear();
		CalcularVentaTotal();
		CalcularVuelto();
		cafeteria.CargarListaCafeteria(ref listaStore);
		txtEfectivo.Text = "0.00";
		lblTotal.Text = "$0.00";
		lblVuelto.Text = "$0.00";
	}
	
	protected virtual void OnCmdStockClicked (object sender, System.EventArgs e)
	{
		Taquilla.compras Compras = new Taquilla.compras();
		Compras.Modal = true;
		Compras.Show();
	}
	
	private void CalcularVuelto()
	{
		try {lblVuelto.Markup = "<span size='xx-large'>Vuelto: </span><span size='xx-large' color='blue'>$" + (double.Parse(txtEfectivo.Text) - CalcularVentaTotal()).ToString("0.00")+"</span>";	}
		catch {lblVuelto.Markup = "<span size='xx-large'>Vuelto:  </span><span size='xx-large' color='blue'>$0.00</span>";}
	}	

	private double CalcularVentaTotal()
	{
		TreeIter iter = new TreeIter();
		double Total = 0.00;
		if (listaStore.GetIterFirst(out iter)) {
			do {
				Total += (double)(int.Parse(listaStore.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString()) * double.Parse(listaStore.GetValue(iter,cafeteria.tvLista_Col_Precio).ToString()));
			} while(listaStore.IterNext(ref iter));
		}
		
		lblTotal.Markup  = "<span size='xx-large'>Total: </span><span size='xx-large' color='red'>$"+Total.ToString("0.00")+"</span>";
		
		return Total;
	}

}
