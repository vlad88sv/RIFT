
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.HBox hbContenedor;
	private global::Gtk.VBox vbPanelIzquierdo;
	private global::Gtk.HBox hbox1;
	private global::Gtk.VBox controles;
	private global::Gtk.Calendar calDiaTrabajo;
	private global::Gtk.VBox vbox2;
	private global::Gtk.HBox hbox6;
	private global::Gtk.Button cmdFinalizar;
	private global::Gtk.Label lblRegistradoComo;
	private global::Gtk.Label lblRegistradoComo1;
	private global::Gtk.HBox hbox7;
	private global::Gtk.Button Cumpleanos;
	private global::Gtk.Button Pases;
	private global::Gtk.Button cmdStock;
	private global::Gtk.Button cmbCorteZ;
	private global::Gtk.HSeparator hseparator2;
	private global::Gtk.Label lblFechaActual;
	private global::Gtk.HBox hbox8;
	private global::Gtk.Label lblHoraActual;
	private global::Gtk.Label lblPrecioDelDia;
	private global::Gtk.HBox hbControles2;
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	private global::Gtk.TreeView treeTiquetes;
	private global::Gtk.VSeparator vseparator1;
	private global::Gtk.VBox Cafeteria;
	private global::Gtk.VBox vbox1;
	private global::Gtk.Table table1;
	private global::Gtk.CheckButton checkbutton1;
	private global::Gtk.HBox hbox5;
	private global::Gtk.Button cmdRiftIniciar;
	private global::Gtk.Button cmdRiftAbortar;
	private global::Gtk.Button cmdRiftFinalizar;
	private global::Gtk.Label label4;
	private global::Gtk.Label label5;
	private global::Gtk.HSeparator hseparator1;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TreeView tvLista;
	private global::Gtk.HBox hbox4;
	private global::Gtk.Label lblVuelto;
	private global::Gtk.Label lblTotal;
	private global::Gtk.HBox hbox3;
	private global::Gtk.Label label1;
	private global::Gtk.Entry txtEfectivo;
	private global::Gtk.HBox hbox2;
	private global::Gtk.Button btnAnular;
	private global::Gtk.Button btnCancelar;
	private global::Gtk.Button btnImprimir;
	
	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.WidthRequest = 1366;
		this.HeightRequest = 768;
		this.Name = "MainWindow";
		this.Title = "RIFT Taquilla - Versión 4.2";
		this.Icon = global::Stetic.IconLoader.LoadIcon (this, "stock_notes", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(1));
		this.BorderWidth = ((uint)(2));
		this.AllowShrink = true;
		this.DefaultHeight = 600;
		this.Gravity = ((global::Gdk.Gravity)(5));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hbContenedor = new global::Gtk.HBox ();
		this.hbContenedor.WidthRequest = 800;
		this.hbContenedor.HeightRequest = 600;
		this.hbContenedor.Name = "hbContenedor";
		// Container child hbContenedor.Gtk.Box+BoxChild
		this.vbPanelIzquierdo = new global::Gtk.VBox ();
		this.vbPanelIzquierdo.WidthRequest = 600;
		this.vbPanelIzquierdo.Name = "vbPanelIzquierdo";
		// Container child vbPanelIzquierdo.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.controles = new global::Gtk.VBox ();
		this.controles.WidthRequest = 207;
		this.controles.Name = "controles";
		this.controles.Spacing = 6;
		// Container child controles.Gtk.Box+BoxChild
		this.calDiaTrabajo = new global::Gtk.Calendar ();
		this.calDiaTrabajo.WidthRequest = 202;
		this.calDiaTrabajo.HeightRequest = 184;
		this.calDiaTrabajo.CanFocus = true;
		this.calDiaTrabajo.Name = "calDiaTrabajo";
		this.calDiaTrabajo.DisplayOptions = ((global::Gtk.CalendarDisplayOptions)(35));
		this.controles.Add (this.calDiaTrabajo);
		global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.controles [this.calDiaTrabajo]));
		w1.Position = 0;
		w1.Expand = false;
		w1.Fill = false;
		this.hbox1.Add (this.controles);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.controles]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox6 = new global::Gtk.HBox ();
		this.hbox6.Name = "hbox6";
		this.hbox6.Spacing = 6;
		// Container child hbox6.Gtk.Box+BoxChild
		this.cmdFinalizar = new global::Gtk.Button ();
		this.cmdFinalizar.CanFocus = true;
		this.cmdFinalizar.Name = "cmdFinalizar";
		this.cmdFinalizar.UseUnderline = true;
		// Container child cmdFinalizar.Gtk.Container+ContainerChild
		global::Gtk.Alignment w3 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w4 = new global::Gtk.HBox ();
		w4.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w5 = new global::Gtk.Image ();
		w5.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-close", global::Gtk.IconSize.Menu);
		w4.Add (w5);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w7 = new global::Gtk.Label ();
		w7.LabelProp = "Cerrar sesión";
		w7.UseUnderline = true;
		w4.Add (w7);
		w3.Add (w4);
		this.cmdFinalizar.Add (w3);
		this.hbox6.Add (this.cmdFinalizar);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.cmdFinalizar]));
		w11.PackType = ((global::Gtk.PackType)(1));
		w11.Position = 0;
		w11.Expand = false;
		w11.Fill = false;
		// Container child hbox6.Gtk.Box+BoxChild
		this.lblRegistradoComo = new global::Gtk.Label ();
		this.lblRegistradoComo.Name = "lblRegistradoComo";
		this.lblRegistradoComo.LabelProp = "Cajero";
		this.hbox6.Add (this.lblRegistradoComo);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.lblRegistradoComo]));
		w12.PackType = ((global::Gtk.PackType)(1));
		w12.Position = 1;
		// Container child hbox6.Gtk.Box+BoxChild
		this.lblRegistradoComo1 = new global::Gtk.Label ();
		this.lblRegistradoComo1.Name = "lblRegistradoComo1";
		this.lblRegistradoComo1.LabelProp = "Registrado como";
		this.hbox6.Add (this.lblRegistradoComo1);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.lblRegistradoComo1]));
		w13.PackType = ((global::Gtk.PackType)(1));
		w13.Position = 2;
		w13.Expand = false;
		w13.Fill = false;
		this.vbox2.Add (this.hbox6);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox6]));
		w14.Position = 0;
		w14.Expand = false;
		w14.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox7 = new global::Gtk.HBox ();
		this.hbox7.Name = "hbox7";
		this.hbox7.Homogeneous = true;
		this.hbox7.Spacing = 6;
		// Container child hbox7.Gtk.Box+BoxChild
		this.Cumpleanos = new global::Gtk.Button ();
		this.Cumpleanos.CanFocus = true;
		this.Cumpleanos.Name = "Cumpleanos";
		this.Cumpleanos.UseUnderline = true;
		// Container child Cumpleanos.Gtk.Container+ContainerChild
		global::Gtk.Alignment w15 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w16 = new global::Gtk.HBox ();
		w16.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w17 = new global::Gtk.Image ();
		w17.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
		w16.Add (w17);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w19 = new global::Gtk.Label ();
		w19.LabelProp = "_Eventos";
		w19.UseUnderline = true;
		w16.Add (w19);
		w15.Add (w16);
		this.Cumpleanos.Add (w15);
		this.hbox7.Add (this.Cumpleanos);
		global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.Cumpleanos]));
		w23.Position = 0;
		w23.Expand = false;
		w23.Fill = false;
		// Container child hbox7.Gtk.Box+BoxChild
		this.Pases = new global::Gtk.Button ();
		this.Pases.CanFocus = true;
		this.Pases.Name = "Pases";
		this.Pases.UseUnderline = true;
		// Container child Pases.Gtk.Container+ContainerChild
		global::Gtk.Alignment w24 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w25 = new global::Gtk.HBox ();
		w25.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w26 = new global::Gtk.Image ();
		w26.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "stock_new-text", global::Gtk.IconSize.Menu);
		w25.Add (w26);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w28 = new global::Gtk.Label ();
		w28.LabelProp = "_Pases";
		w28.UseUnderline = true;
		w25.Add (w28);
		w24.Add (w25);
		this.Pases.Add (w24);
		this.hbox7.Add (this.Pases);
		global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.Pases]));
		w32.Position = 1;
		w32.Expand = false;
		w32.Fill = false;
		// Container child hbox7.Gtk.Box+BoxChild
		this.cmdStock = new global::Gtk.Button ();
		this.cmdStock.CanFocus = true;
		this.cmdStock.Name = "cmdStock";
		this.cmdStock.UseUnderline = true;
		// Container child cmdStock.Gtk.Container+ContainerChild
		global::Gtk.Alignment w33 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w34 = new global::Gtk.HBox ();
		w34.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w35 = new global::Gtk.Image ();
		w35.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "stock_insert-note", global::Gtk.IconSize.Menu);
		w34.Add (w35);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w37 = new global::Gtk.Label ();
		w37.LabelProp = "Compras";
		w37.UseUnderline = true;
		w34.Add (w37);
		w33.Add (w34);
		this.cmdStock.Add (w33);
		this.hbox7.Add (this.cmdStock);
		global::Gtk.Box.BoxChild w41 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.cmdStock]));
		w41.Position = 2;
		w41.Expand = false;
		w41.Fill = false;
		// Container child hbox7.Gtk.Box+BoxChild
		this.cmbCorteZ = new global::Gtk.Button ();
		this.cmbCorteZ.CanFocus = true;
		this.cmbCorteZ.Name = "cmbCorteZ";
		this.cmbCorteZ.UseUnderline = true;
		// Container child cmbCorteZ.Gtk.Container+ContainerChild
		global::Gtk.Alignment w42 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w43 = new global::Gtk.HBox ();
		w43.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w44 = new global::Gtk.Image ();
		w44.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "stock_copy", global::Gtk.IconSize.Menu);
		w43.Add (w44);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w46 = new global::Gtk.Label ();
		w46.LabelProp = "Corte _Z";
		w46.UseUnderline = true;
		w43.Add (w46);
		w42.Add (w43);
		this.cmbCorteZ.Add (w42);
		this.hbox7.Add (this.cmbCorteZ);
		global::Gtk.Box.BoxChild w50 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.cmbCorteZ]));
		w50.Position = 3;
		w50.Expand = false;
		w50.Fill = false;
		this.vbox2.Add (this.hbox7);
		global::Gtk.Box.BoxChild w51 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox7]));
		w51.Position = 1;
		w51.Expand = false;
		w51.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hseparator2 = new global::Gtk.HSeparator ();
		this.hseparator2.Name = "hseparator2";
		this.vbox2.Add (this.hseparator2);
		global::Gtk.Box.BoxChild w52 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator2]));
		w52.Position = 2;
		w52.Expand = false;
		w52.Fill = false;
		w52.Padding = ((uint)(13));
		// Container child vbox2.Gtk.Box+BoxChild
		this.lblFechaActual = new global::Gtk.Label ();
		this.lblFechaActual.Name = "lblFechaActual";
		this.lblFechaActual.LabelProp = "Fecha de hoy";
		this.vbox2.Add (this.lblFechaActual);
		global::Gtk.Box.BoxChild w53 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.lblFechaActual]));
		w53.Position = 3;
		w53.Expand = false;
		w53.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox8 = new global::Gtk.HBox ();
		this.hbox8.Name = "hbox8";
		this.hbox8.Homogeneous = true;
		this.hbox8.Spacing = 6;
		// Container child hbox8.Gtk.Box+BoxChild
		this.lblHoraActual = new global::Gtk.Label ();
		this.lblHoraActual.Name = "lblHoraActual";
		this.lblHoraActual.LabelProp = "Hora actual";
		this.hbox8.Add (this.lblHoraActual);
		global::Gtk.Box.BoxChild w54 = ((global::Gtk.Box.BoxChild)(this.hbox8 [this.lblHoraActual]));
		w54.Position = 0;
		w54.Expand = false;
		w54.Fill = false;
		// Container child hbox8.Gtk.Box+BoxChild
		this.lblPrecioDelDia = new global::Gtk.Label ();
		this.lblPrecioDelDia.Name = "lblPrecioDelDia";
		this.lblPrecioDelDia.LabelProp = "Precio del día";
		this.hbox8.Add (this.lblPrecioDelDia);
		global::Gtk.Box.BoxChild w55 = ((global::Gtk.Box.BoxChild)(this.hbox8 [this.lblPrecioDelDia]));
		w55.Position = 1;
		w55.Expand = false;
		w55.Fill = false;
		this.vbox2.Add (this.hbox8);
		global::Gtk.Box.BoxChild w56 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox8]));
		w56.Position = 4;
		w56.Expand = false;
		w56.Fill = false;
		this.hbox1.Add (this.vbox2);
		global::Gtk.Box.BoxChild w57 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox2]));
		w57.Position = 1;
		this.vbPanelIzquierdo.Add (this.hbox1);
		global::Gtk.Box.BoxChild w58 = ((global::Gtk.Box.BoxChild)(this.vbPanelIzquierdo [this.hbox1]));
		w58.Position = 0;
		w58.Expand = false;
		w58.Fill = false;
		// Container child vbPanelIzquierdo.Gtk.Box+BoxChild
		this.hbControles2 = new global::Gtk.HBox ();
		this.hbControles2.Name = "hbControles2";
		this.hbControles2.Spacing = 6;
		// Container child hbControles2.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.treeTiquetes = new global::Gtk.TreeView ();
		this.treeTiquetes.CanFocus = true;
		this.treeTiquetes.Name = "treeTiquetes";
		this.treeTiquetes.EnableSearch = false;
		this.treeTiquetes.RulesHint = true;
		this.GtkScrolledWindow1.Add (this.treeTiquetes);
		this.hbControles2.Add (this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w60 = ((global::Gtk.Box.BoxChild)(this.hbControles2 [this.GtkScrolledWindow1]));
		w60.Position = 0;
		this.vbPanelIzquierdo.Add (this.hbControles2);
		global::Gtk.Box.BoxChild w61 = ((global::Gtk.Box.BoxChild)(this.vbPanelIzquierdo [this.hbControles2]));
		w61.Position = 1;
		this.hbContenedor.Add (this.vbPanelIzquierdo);
		global::Gtk.Box.BoxChild w62 = ((global::Gtk.Box.BoxChild)(this.hbContenedor [this.vbPanelIzquierdo]));
		w62.Position = 0;
		w62.Expand = false;
		w62.Fill = false;
		// Container child hbContenedor.Gtk.Box+BoxChild
		this.vseparator1 = new global::Gtk.VSeparator ();
		this.vseparator1.Name = "vseparator1";
		this.hbContenedor.Add (this.vseparator1);
		global::Gtk.Box.BoxChild w63 = ((global::Gtk.Box.BoxChild)(this.hbContenedor [this.vseparator1]));
		w63.Position = 1;
		w63.Expand = false;
		w63.Fill = false;
		w63.Padding = ((uint)(3));
		// Container child hbContenedor.Gtk.Box+BoxChild
		this.Cafeteria = new global::Gtk.VBox ();
		this.Cafeteria.WidthRequest = 350;
		this.Cafeteria.Name = "Cafeteria";
		// Container child Cafeteria.Gtk.Box+BoxChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.BorderWidth = ((uint)(3));
		// Container child vbox1.Gtk.Box+BoxChild
		this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.checkbutton1 = new global::Gtk.CheckButton ();
		this.checkbutton1.CanFocus = true;
		this.checkbutton1.Name = "checkbutton1";
		this.checkbutton1.Label = "Música";
		this.checkbutton1.Active = true;
		this.checkbutton1.DrawIndicator = true;
		this.checkbutton1.UseUnderline = true;
		this.table1.Add (this.checkbutton1);
		global::Gtk.Table.TableChild w64 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkbutton1]));
		w64.TopAttach = ((uint)(1));
		w64.BottomAttach = ((uint)(2));
		w64.LeftAttach = ((uint)(1));
		w64.RightAttach = ((uint)(2));
		w64.XOptions = ((global::Gtk.AttachOptions)(1));
		w64.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.hbox5 = new global::Gtk.HBox ();
		this.hbox5.Name = "hbox5";
		this.hbox5.Homogeneous = true;
		this.hbox5.Spacing = 6;
		// Container child hbox5.Gtk.Box+BoxChild
		this.cmdRiftIniciar = new global::Gtk.Button ();
		this.cmdRiftIniciar.CanFocus = true;
		this.cmdRiftIniciar.Name = "cmdRiftIniciar";
		this.cmdRiftIniciar.UseUnderline = true;
		this.cmdRiftIniciar.Label = "Iniciar";
		this.hbox5.Add (this.cmdRiftIniciar);
		global::Gtk.Box.BoxChild w65 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.cmdRiftIniciar]));
		w65.Position = 0;
		w65.Expand = false;
		w65.Fill = false;
		// Container child hbox5.Gtk.Box+BoxChild
		this.cmdRiftAbortar = new global::Gtk.Button ();
		this.cmdRiftAbortar.CanFocus = true;
		this.cmdRiftAbortar.Name = "cmdRiftAbortar";
		this.cmdRiftAbortar.UseUnderline = true;
		this.cmdRiftAbortar.Label = "Abortar";
		this.hbox5.Add (this.cmdRiftAbortar);
		global::Gtk.Box.BoxChild w66 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.cmdRiftAbortar]));
		w66.Position = 1;
		w66.Expand = false;
		w66.Fill = false;
		// Container child hbox5.Gtk.Box+BoxChild
		this.cmdRiftFinalizar = new global::Gtk.Button ();
		this.cmdRiftFinalizar.CanFocus = true;
		this.cmdRiftFinalizar.Name = "cmdRiftFinalizar";
		this.cmdRiftFinalizar.UseUnderline = true;
		this.cmdRiftFinalizar.Label = "Finalizar";
		this.hbox5.Add (this.cmdRiftFinalizar);
		global::Gtk.Box.BoxChild w67 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.cmdRiftFinalizar]));
		w67.Position = 2;
		w67.Expand = false;
		w67.Fill = false;
		this.table1.Add (this.hbox5);
		global::Gtk.Table.TableChild w68 = ((global::Gtk.Table.TableChild)(this.table1 [this.hbox5]));
		w68.TopAttach = ((uint)(1));
		w68.BottomAttach = ((uint)(2));
		w68.XOptions = ((global::Gtk.AttachOptions)(4));
		w68.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.label4 = new global::Gtk.Label ();
		this.label4.Name = "label4";
		this.label4.LabelProp = "Control de juego";
		this.table1.Add (this.label4);
		global::Gtk.Table.TableChild w69 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
		w69.XOptions = ((global::Gtk.AttachOptions)(4));
		w69.YOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.label5 = new global::Gtk.Label ();
		this.label5.Name = "label5";
		this.label5.LabelProp = "Lobby";
		this.label5.Justify = ((global::Gtk.Justification)(2));
		this.table1.Add (this.label5);
		global::Gtk.Table.TableChild w70 = ((global::Gtk.Table.TableChild)(this.table1 [this.label5]));
		w70.LeftAttach = ((uint)(1));
		w70.RightAttach = ((uint)(2));
		w70.YOptions = ((global::Gtk.AttachOptions)(4));
		this.vbox1.Add (this.table1);
		global::Gtk.Box.BoxChild w71 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.table1]));
		w71.Position = 0;
		w71.Expand = false;
		w71.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hseparator1 = new global::Gtk.HSeparator ();
		this.hseparator1.Name = "hseparator1";
		this.vbox1.Add (this.hseparator1);
		global::Gtk.Box.BoxChild w72 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hseparator1]));
		w72.Position = 1;
		w72.Expand = false;
		w72.Fill = false;
		w72.Padding = ((uint)(19));
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.tvLista = new global::Gtk.TreeView ();
		this.tvLista.CanFocus = true;
		this.tvLista.Name = "tvLista";
		this.tvLista.EnableSearch = false;
		this.GtkScrolledWindow.Add (this.tvLista);
		this.vbox1.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w74 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
		w74.Position = 2;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox4 = new global::Gtk.HBox ();
		this.hbox4.Name = "hbox4";
		this.hbox4.Homogeneous = true;
		this.hbox4.Spacing = 6;
		// Container child hbox4.Gtk.Box+BoxChild
		this.lblVuelto = new global::Gtk.Label ();
		this.lblVuelto.Name = "lblVuelto";
		this.lblVuelto.LabelProp = "<span size='xx-large'>Vuelto:  </span><span size='xx-large' color='blue'>$0.00</span>";
		this.lblVuelto.UseMarkup = true;
		this.hbox4.Add (this.lblVuelto);
		global::Gtk.Box.BoxChild w75 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.lblVuelto]));
		w75.Position = 0;
		w75.Expand = false;
		w75.Fill = false;
		// Container child hbox4.Gtk.Box+BoxChild
		this.lblTotal = new global::Gtk.Label ();
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.LabelProp = "<span size='xx-large'>Total: </span><span size='xx-large' color='red'>$0.00</span>";
		this.lblTotal.UseMarkup = true;
		this.hbox4.Add (this.lblTotal);
		global::Gtk.Box.BoxChild w76 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.lblTotal]));
		w76.Position = 1;
		w76.Expand = false;
		w76.Fill = false;
		this.vbox1.Add (this.hbox4);
		global::Gtk.Box.BoxChild w77 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox4]));
		w77.Position = 3;
		w77.Expand = false;
		w77.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox3 = new global::Gtk.HBox ();
		this.hbox3.Name = "hbox3";
		this.hbox3.Spacing = 6;
		// Container child hbox3.Gtk.Box+BoxChild
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = "Efectivo recibido";
		this.hbox3.Add (this.label1);
		global::Gtk.Box.BoxChild w78 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label1]));
		w78.Position = 0;
		w78.Expand = false;
		w78.Fill = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.txtEfectivo = new global::Gtk.Entry ();
		this.txtEfectivo.CanFocus = true;
		this.txtEfectivo.Name = "txtEfectivo";
		this.txtEfectivo.IsEditable = true;
		this.txtEfectivo.InvisibleChar = '•';
		this.hbox3.Add (this.txtEfectivo);
		global::Gtk.Box.BoxChild w79 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.txtEfectivo]));
		w79.Position = 1;
		this.vbox1.Add (this.hbox3);
		global::Gtk.Box.BoxChild w80 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox3]));
		w80.Position = 4;
		w80.Expand = false;
		w80.Fill = false;
		w80.Padding = ((uint)(5));
		this.Cafeteria.Add (this.vbox1);
		global::Gtk.Box.BoxChild w81 = ((global::Gtk.Box.BoxChild)(this.Cafeteria [this.vbox1]));
		w81.Position = 0;
		// Container child Cafeteria.Gtk.Box+BoxChild
		this.hbox2 = new global::Gtk.HBox ();
		this.hbox2.Name = "hbox2";
		this.hbox2.Spacing = 6;
		// Container child hbox2.Gtk.Box+BoxChild
		this.btnAnular = new global::Gtk.Button ();
		this.btnAnular.CanFocus = true;
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.UseUnderline = true;
		// Container child btnAnular.Gtk.Container+ContainerChild
		global::Gtk.Alignment w82 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w83 = new global::Gtk.HBox ();
		w83.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w84 = new global::Gtk.Image ();
		w84.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
		w83.Add (w84);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w86 = new global::Gtk.Label ();
		w86.LabelProp = "_Anular";
		w86.UseUnderline = true;
		w83.Add (w86);
		w82.Add (w83);
		this.btnAnular.Add (w82);
		this.hbox2.Add (this.btnAnular);
		global::Gtk.Box.BoxChild w90 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.btnAnular]));
		w90.Position = 0;
		w90.Expand = false;
		w90.Fill = false;
		// Container child hbox2.Gtk.Box+BoxChild
		this.btnCancelar = new global::Gtk.Button ();
		this.btnCancelar.CanFocus = true;
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.UseUnderline = true;
		// Container child btnCancelar.Gtk.Container+ContainerChild
		global::Gtk.Alignment w91 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w92 = new global::Gtk.HBox ();
		w92.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w93 = new global::Gtk.Image ();
		w93.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
		w92.Add (w93);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w95 = new global::Gtk.Label ();
		w95.LabelProp = "_Cancelar";
		w95.UseUnderline = true;
		w92.Add (w95);
		w91.Add (w92);
		this.btnCancelar.Add (w91);
		this.hbox2.Add (this.btnCancelar);
		global::Gtk.Box.BoxChild w99 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.btnCancelar]));
		w99.Position = 1;
		// Container child hbox2.Gtk.Box+BoxChild
		this.btnImprimir = new global::Gtk.Button ();
		this.btnImprimir.CanFocus = true;
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.UseUnderline = true;
		// Container child btnImprimir.Gtk.Container+ContainerChild
		global::Gtk.Alignment w100 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w101 = new global::Gtk.HBox ();
		w101.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w102 = new global::Gtk.Image ();
		w102.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-ok", global::Gtk.IconSize.Menu);
		w101.Add (w102);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w104 = new global::Gtk.Label ();
		w104.LabelProp = "_Imprimir";
		w104.UseUnderline = true;
		w101.Add (w104);
		w100.Add (w101);
		this.btnImprimir.Add (w100);
		this.hbox2.Add (this.btnImprimir);
		global::Gtk.Box.BoxChild w108 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.btnImprimir]));
		w108.Position = 2;
		this.Cafeteria.Add (this.hbox2);
		global::Gtk.Box.BoxChild w109 = ((global::Gtk.Box.BoxChild)(this.Cafeteria [this.hbox2]));
		w109.Position = 1;
		w109.Expand = false;
		w109.Fill = false;
		this.hbContenedor.Add (this.Cafeteria);
		global::Gtk.Box.BoxChild w110 = ((global::Gtk.Box.BoxChild)(this.hbContenedor [this.Cafeteria]));
		w110.PackType = ((global::Gtk.PackType)(1));
		w110.Position = 2;
		w110.Expand = false;
		w110.Fill = false;
		this.Add (this.hbContenedor);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1368;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.calDiaTrabajo.DaySelected += new global::System.EventHandler (this.OnCalDiaTrabajoDaySelected);
		this.cmdFinalizar.Clicked += new global::System.EventHandler (this.OnCmdFinalizarClicked);
		this.Cumpleanos.Clicked += new global::System.EventHandler (this.OnCumpleanosClicked);
		this.Pases.Clicked += new global::System.EventHandler (this.OnPasesClicked);
		this.cmdStock.Clicked += new global::System.EventHandler (this.OnCmdStockClicked);
		this.cmbCorteZ.Clicked += new global::System.EventHandler (this.OnCmbCorteZClicked);
		this.treeTiquetes.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnTreeTiquetesKeyPressEvent);
		this.cmdRiftIniciar.Clicked += new global::System.EventHandler (this.OnCmdRiftIniciarClicked);
		this.cmdRiftAbortar.Clicked += new global::System.EventHandler (this.OnCmdRiftAbortarClicked);
		this.cmdRiftFinalizar.Clicked += new global::System.EventHandler (this.OnCmdRiftFinalizarClicked);
		this.checkbutton1.Toggled += new global::System.EventHandler (this.OnCheckbutton1Toggled);
		this.tvLista.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnTvListaKeyPressEvent);
		this.txtEfectivo.Changed += new global::System.EventHandler (this.OnTxtEfectivoChanged);
		this.btnAnular.Clicked += new global::System.EventHandler (this.OnBtnAnularClicked);
		this.btnCancelar.Clicked += new global::System.EventHandler (this.OnBtnCancelarClicked);
		this.btnImprimir.Clicked += new global::System.EventHandler (this.OnBtnImprimirClicked);
	}
}
