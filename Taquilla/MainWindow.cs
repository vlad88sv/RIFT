using System;
using System.Globalization;
using Gtk;
using GLib;

public partial class MainWindow : Gtk.Window
{
	
	Gtk.ListStore tree = new Gtk.ListStore(typeof (string), //Hora
	                                       typeof (string), //Ocupados
	                                       typeof (string), //Disponibles
	                                       typeof (string)  //notas
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
		this.Maximize();
		
		/* Proceso que inicia del programa */
	
		OnCalDiaTrabajoDaySelected(null,null);
		
		CrearTreeTiquetes();
		
		/* Reloj */
		GLib.Timeout.Add (1000, new GLib.TimeoutHandler (ActualizarInterfaz));
		
		hbControles2.Sensitive = false;
		controles.Sensitive = false;
		vbControlesAdmin.Sensitive = false;
		
		treeTiquetes.RulesHint = true;
		treeTiquetes.EnableGridLines = TreeViewGridLines.Both;

		// Cafetería
		cafeteria.ConstruirLista(ref tvLista, ref listaStore);		
		cafeteria.CargarListaCafeteria(ref listaStore);
	}
	
	bool ActualizarInterfaz()
	{
		lblHoraActual.Markup = "<span  color='red' size='xx-large'>"+DateTime.Now.ToString("HH:mm:ss")+"</span>"; 
		lblFechaActual.Markup = "<span color='red'>"+DateTime.Now.ToString("D")+"</span>";
		return true;
	}
	
	private void CrearTreeTiquetes()
	{
		treeTiquetes.Model = tree;	
		treeTiquetes.AppendColumn("Inicio",new Gtk.CellRendererText(),"markup",0);
		treeTiquetes.AppendColumn("O",new Gtk.CellRendererText(),"markup",1);
		treeTiquetes.AppendColumn("D",new Gtk.CellRendererText(),"markup",2);
		treeTiquetes.AppendColumn("Notas",new Gtk.CellRendererText(),"markup",3);
	}	

	private void CargarTiquetesDelDia()
	{
		double benchmark = DateTime.Now.TimeOfDay.TotalMilliseconds;
		
		// Desprendemos el modelo del treeTiquetes, para hacer que se actualice solo al final
		treeTiquetes.Visible = false;
		treeTiquetes.Model = null;
		Gtk.TreeIter iter = new Gtk.TreeIter();
		
		// Reiniciemos el TreeTiquetes
		tree.Clear();
		
		for (int h = 0; h < 65; h++)
				tree.AppendValues (DateTime.Parse("08:00").AddMinutes(15.0*(double)h).ToString("HH:mm",CultureInfo.InvariantCulture),"0",global.maximo_jugadores.ToString());
						
		string c = "SELECT COUNT(*) AS 'vendidos', DATE(`fecha_juego`) AS 'fecha', DATE_FORMAT(`fecha_juego`,'%H:%i') AS `hora` FROM `tickets` LEFT JOIN `tipo_boleto` USING(`ID_tipo_boleto`) WHERE `tickets`.`ID_tipo_boleto` IN ("+tiquete.ID_tipo_normal+","+tiquete.ID_tipo_gratis+") AND DATE(`fecha_juego`) = '"+ global.fechaDiaTrabajoFMySQL +"' GROUP BY `fecha_juego` ORDER BY `fecha_juego` ASC, `numero_jugador` ASC";
		if (!MySQL.consultar(c))
			return;
		
		Console.WriteLine("S0:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		tree.GetIterFirst(out iter);	
		
		while (MySQL.Reader.Read())
		{
			// Encontremos la primera hora para partir de ahi				
			while (tree.GetValue(iter, 0).ToString() != MySQL.Reader["hora"].ToString())
				tree.IterNext(ref iter);
			tree.SetValue(iter, 1, MySQL.Reader["vendidos"].ToString());			
		}

		Console.WriteLine("S1:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		// Eventos para hoy?.
		c = "SELECT `eventos`.`ID_evento`, `eventos`.`precio_evento`, `eventos`.`patrocinado_por`, `eventos`.`nombre_evento`, `eventos`.`notas`, `eventos`.`fecha_evento`,DATE_FORMAT(`eventos`.`hora_inicio`,'%H:%i') AS 'fhora_inicio', DATE_FORMAT(`eventos`.`hora_final`,'%H:%i') AS 'fhora_final', `eventos`.`agregado_por_usuario` FROM `rift3`.`eventos` WHERE `eventos`.`fecha_evento`='"+global.fechaDiaTrabajoFMySQL+"' ORDER BY `eventos`.`hora_inicio`";
		MySQL.consultar(c);		

		tree.GetIterFirst(out iter);
		// Recorremos un evento a la vez
		while (MySQL.Reader.Read()) {
			// Encontremos la hora de inicio del evento
			while (tree.GetValue(iter, 0).ToString() != MySQL.Reader["fhora_inicio"].ToString())
				tree.IterNext(ref iter);

			while (tree.GetValue(iter, 0).ToString() != MySQL.Reader["fhora_final"].ToString()) {
				tree.SetValue(iter, 1, global.maximo_jugadores.ToString());
				tree.SetValue(iter, 3, MySQL.Reader["patrocinado_por"].ToString() + " ~> " + MySQL.Reader["nombre_evento"].ToString());
				Console.WriteLine(MySQL.Reader["patrocinado_por"].ToString() + " ~> " + MySQL.Reader["nombre_evento"].ToString());
				tree.IterNext(ref iter);
			}
		}
			
		Console.WriteLine("S3:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
		
		// Ya cargó todos los tiquetes, calcular disponibilidades.
		tree.GetIterFirst(out iter);
		do {
			CalcularDisponibilidad(iter);
		} while (tree.IterNext(ref iter));
		
		Console.WriteLine("S4:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
			
		// Restauramos la visibilidad de la lista (offscreen rendering)
		treeTiquetes.Model = tree;
		treeTiquetes.Visible = true;
		Console.WriteLine("S5:"+(DateTime.Now.TimeOfDay.TotalMilliseconds-benchmark));
	}

	protected virtual void OnCalDiaTrabajoDaySelected (object sender, System.EventArgs e)
	{
		global.fechaDiaTrabajoFMySQL = calDiaTrabajo.Date.ToString("yyyy-MM-dd");
		global.fechaDiaTrabajo = calDiaTrabajo.Date.ToString("dd/MM/yyyy");
		
		// Precio base del día y número de día
		MySQL.consultar("SELECT precio, DATE_FORMAT('"+global.fechaDiaTrabajoFMySQL+"','%w') AS 'diaN' FROM precios_diarios WHERE dia = DATE_FORMAT('"+global.fechaDiaTrabajoFMySQL+"','%w')");
		MySQL.Reader.Read();
		tiquete.PrecioBase = double.Parse(MySQL.Reader["precio"].ToString());
		global.diaNumero = int.Parse(MySQL.Reader["diaN"].ToString());

		CargarTiquetesDelDia();
		
		// Ayudante de precios
		string AyudantePrecios = "";
		
		AyudantePrecios = "<span>1 juego (15 minutos)</span>\n";
		for (int i = 1; i < 14; i++)
		{
			AyudantePrecios += i + " Jugador = $" + (tiquete.PrecioBase * i).ToString("0.00") + "\n";
		}
		lbl15.Markup = AyudantePrecios;

		AyudantePrecios = "<span>2 juego (30 minutos)</span>\n";
		for (int i = 1; i < 14; i++)
		{
			AyudantePrecios += i + " Jugador = $" + (tiquete.PrecioBase * i * 2).ToString("0.00") + "\n";
		}
		lbl30.Markup = AyudantePrecios;
		
		AyudantePrecios = "<span>3 juego (45 minutos)</span>\n";
		for (int i = 1; i < 14; i++)
		{
			AyudantePrecios += i + " Jugador = $" + (tiquete.PrecioBase * i * 3).ToString("0.00") + "\n";
		}
		lbl45.Markup = AyudantePrecios;
		
		AyudantePrecios = "<span>4 juego (60 minutos)</span>\n";
		for (int i = 1; i < 14; i++)
		{
			AyudantePrecios += i + " Jugador = $" + (tiquete.PrecioBase * i * 4).ToString("0.00") + "\n";
		}
		lbl60.Markup = AyudantePrecios;
	}
	
	protected virtual void OnTreeTiquetesKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
	{
		Resultadotiquete Resultado = new Resultadotiquete();
		Gtk.TreeIter iter = new Gtk.TreeIter();
		treeTiquetes.Selection.GetSelected(out iter);
		global.fechaDiaTrabajoMasJuegoFMySQL = global.fechaDiaTrabajoFMySQL.ToString() + " " + DateTime.Parse(tree.GetValue(iter,0).ToString()).ToString("HH:mm:00");
		global.fechaDiaTrabajoMasJuego = global.fechaDiaTrabajoFMySQL + " " + tree.GetValue(iter,0).ToString();
		global.HoraJuego = DateTime.Parse(treeTiquetes.Model.GetValue(iter,0).ToString()).ToString("HH:mm");
		MessageDialog Mensaje;
					
		// Estará lleno el cupo para la hora seleccionada?
		if (tree.GetValue(iter, 1).ToString() == global.maximo_jugadores.ToString())
		{
			Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, ya no se puede agregar jugadores para este juego (ya esta lleno).");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}
		
		// Han pasado mas de 30 minutos desde la hora de juego deseada a la actual?
		if (DateTime.Parse(global.fechaDiaTrabajoMasJuego,CultureInfo.InvariantCulture).CompareTo(DateTime.Now.AddMinutes(-30.00)) == -1)
		{
			Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, ya no se puede agregar jugadores para este juego (ya pasaron mas de 30 minutos).");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}
		
		switch (args.Event.Key)
		{
			case (Gdk.Key.KP_Add):
				// Vender un tiquete a precio normal del dia
				Resultado = tiquete.EjecutarValidacionTiqueteNormal();
			break;

			case (Gdk.Key.c):
				// Dialogo para código de promoción
				Resultado = tiquete.EjecutarValidacionPasePromocion(true);
			break;
			
			case (Gdk.Key.p):
				// Dialogo para pase
				Resultado = tiquete.EjecutarValidacionPasePromocion(false);
			break;
			
			case (Gdk.Key.m):
				// Dialogo para mover < solicitar el código de tiquete
				Resultado = tiquete.EjecutarValidacionMovimiento();
			break;
			
			case (Gdk.Key.i):
				// Dialogo para mover < solicitar el código de tiquete
				Resultado = tiquete.EjecutarValidacionIVAExento();
			break;
			
			default:
				return;
		}
		
		if (Resultado.error == false)
		{
			tree.SetValue(iter, 1, (int.Parse(tree.GetValue(iter, 1).ToString())+1).ToString() );
			CalcularDisponibilidad(iter);
			// Grabamos la transacción
			Historial.Transaccion(Resultado.transaccion+":"+global.diaNumero);
		}
	}
	
	private void CalcularDisponibilidad(Gtk.TreeIter iter)
	{
		tree.SetValue(iter, 2, (global.maximo_jugadores-int.Parse(tree.GetValue(iter, 1).ToString())).ToString());
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

	protected virtual void OnCmdEstadisticasClicked (object sender, System.EventArgs e)
	{
		if (auth.nivel != "gerente")
		{
			MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "global.Auth :: su usuario no posee autorización para acceder este módulo.");
			Mensaje.Title="Error";
			Mensaje.Run();
			Mensaje.Destroy();
			return;
		}
		
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
			vbControlesAdmin.Sensitive = false;
			tblSesion.Sensitive = true;
			return;
		}
		
		c = "SELECT COUNT(*) AS autorizados, ID_usuario, usuario, nombre, nivel FROM usuarios WHERE usuario='"+txtUsuario.Text+"' AND clave=SHA1('"+txtClave.Text+"')";
		if (MySQL.consultar(c))
		{
			MySQL.Reader.Read();
			
			if (MySQL.Reader["autorizados"].ToString() == "0")
			{
				MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Lo siento, no se puede autorizar. Esta clave no esta registrada.");
				Mensaje.Title="Error";
				Mensaje.Run();
				Mensaje.Destroy();
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
			hbControles2.Sensitive = true;
			controles.Sensitive = true;
			Cafeteria.Sensitive = true;
			vbControlesAdmin.Sensitive = true;
			tblSesion.Sensitive = false;
			cmdSesion.Label = "Finalizar\n"+MySQL.Reader["usuario"].ToString();
			
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
					tiquete += Imprimidor.Imprimir(tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString() + " " + tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Descripcion).ToString(), "$"+Precio.ToString("0.00"));
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
		
		tiquete += Imprimidor.Imprimir("TOTAL:","$"+Total.ToString("0.00"));
		tiquete += Imprimidor.Imprimir("\n",1);
	    tiquete += Imprimidor.Imprimir("Efectivo", "$" + double.Parse(txtEfectivo.Text).ToString("0.00"));
		tiquete += Imprimidor.Imprimir("Cambio:", "$" + (double.Parse(txtEfectivo.Text) - CalcularVentaTotal()).ToString("0.00"));

		tiquete += Imprimidor.Imprimir("\nREF:"+ID_ticket.ToString(),1);
		
		Imprimidor.Tiquete(tiquete,"0000");
		
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

	protected virtual void OnBtnAnularClicked (object sender, System.EventArgs e)
	{
		MessageDialog Mensaje = null;
		string Tiquete = "", REF = "", c = "";
		
		REF = txtEfectivo.Text;
		
		c = "UPDATE `cafeteria_transacciones` SET `precio_grabado`= 0.00, `cancelado` = 1 WHERE ID_ticket = '"+REF+"'";
		if (MySQL.consultar(c))
		{
			if( MySQL.Reader.RecordsAffected > 0 )
			{
				Console.WriteLine("RA:" + MySQL.Reader.RecordsAffected);
				Tiquete += Imprimidor.Imprimir("CANCELACION CAFETERIA",1);
				Tiquete += Imprimidor.Imprimir("REF: "+REF,1);
				Imprimidor.Tiquete(Tiquete, "666");
		
				Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Tiquete anulado");
				Mensaje.Title="Éxito";
				Mensaje.Run();
				Mensaje.Destroy();
				txtEfectivo.Text = "";
				return;	
			}
		}
		
		Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Tiquete no pudo ser anulado.");
		Mensaje.Title="Error";
		Mensaje.Run();
		Mensaje.Destroy();
		txtEfectivo.GrabFocus();
		return;	
		
	}	
	
	protected virtual void OnCmdActualizarClicked (object sender, System.EventArgs e)
	{
		CargarTiquetesDelDia();
	}
	
	
}
