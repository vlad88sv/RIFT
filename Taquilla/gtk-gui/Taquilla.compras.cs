
// This file has been generated by the GUI designer. Do not modify.
namespace Taquilla
{
	public partial class compras
	{
		private global::Gtk.HBox hbox2;
		private global::Gtk.VBox vbox2;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label label5;
		private global::Gtk.Entry txtComprador;
		private global::Gtk.Label label6;
		private global::Gtk.Entry txtTotalCompra;
		private global::Gtk.HSeparator hseparator2;
		private global::Gtk.Label label1;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TextView txtDetalle;
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		private global::Gtk.TreeView tvLista;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.HButtonBox dialog1_ActionArea1;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button cmdGrabar;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Taquilla.compras
			this.Name = "Taquilla.compras";
			this.Title = "Compras [ingresos]";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child Taquilla.compras.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.WidthRequest = 653;
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = "Comprador";
			this.hbox3.Add (this.label5);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label5]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.txtComprador = new global::Gtk.Entry ();
			this.txtComprador.CanFocus = true;
			this.txtComprador.Name = "txtComprador";
			this.txtComprador.IsEditable = true;
			this.txtComprador.InvisibleChar = '•';
			this.hbox3.Add (this.txtComprador);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.txtComprador]));
			w3.Position = 1;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = "Total compra ($)";
			this.hbox3.Add (this.label6);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label6]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.txtTotalCompra = new global::Gtk.Entry ();
			this.txtTotalCompra.CanFocus = true;
			this.txtTotalCompra.Name = "txtTotalCompra";
			this.txtTotalCompra.IsEditable = true;
			this.txtTotalCompra.InvisibleChar = '•';
			this.hbox3.Add (this.txtTotalCompra);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.txtTotalCompra]));
			w5.Position = 3;
			this.vbox2.Add (this.hbox3);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox3]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator2 = new global::Gtk.HSeparator ();
			this.hseparator2.Name = "hseparator2";
			this.vbox2.Add (this.hseparator2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator2]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = "Ingresar detalle de la factura";
			this.vbox2.Add (this.label1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.label1]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.txtDetalle = new global::Gtk.TextView ();
			this.txtDetalle.CanFocus = true;
			this.txtDetalle.Name = "txtDetalle";
			this.GtkScrolledWindow.Add (this.txtDetalle);
			this.vbox2.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
			w10.Position = 3;
			this.hbox2.Add (this.vbox2);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.vbox2]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.tvLista = new global::Gtk.TreeView ();
			this.tvLista.CanFocus = true;
			this.tvLista.Name = "tvLista";
			this.tvLista.EnableSearch = false;
			this.GtkScrolledWindow1.Add (this.tvLista);
			this.hbox2.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.GtkScrolledWindow1]));
			w13.Position = 1;
			w1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox2]));
			w14.Position = 0;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			w1.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(w1 [this.hseparator1]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.dialog1_ActionArea1 = new global::Gtk.HButtonBox ();
			this.dialog1_ActionArea1.Name = "dialog1_ActionArea1";
			this.dialog1_ActionArea1.Spacing = 10;
			this.dialog1_ActionArea1.BorderWidth = ((uint)(5));
			this.dialog1_ActionArea1.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			w1.Add (this.dialog1_ActionArea1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(w1 [this.dialog1_ActionArea1]));
			w16.PackType = ((global::Gtk.PackType)(1));
			w16.Position = 3;
			w16.Expand = false;
			w16.Fill = false;
			// Internal child Taquilla.compras.ActionArea
			global::Gtk.HButtonBox w17 = this.ActionArea;
			w17.Name = "dialog1_ActionArea";
			w17.Spacing = 10;
			w17.BorderWidth = ((uint)(5));
			w17.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			// Container child buttonCancel.Gtk.Container+ContainerChild
			global::Gtk.Alignment w18 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w19 = new global::Gtk.HBox ();
			w19.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w20 = new global::Gtk.Image ();
			w20.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
			w19.Add (w20);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w22 = new global::Gtk.Label ();
			w22.LabelProp = "_Cancelar";
			w22.UseUnderline = true;
			w19.Add (w22);
			w18.Add (w19);
			this.buttonCancel.Add (w18);
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w26 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w17 [this.buttonCancel]));
			w26.Expand = false;
			w26.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.cmdGrabar = new global::Gtk.Button ();
			this.cmdGrabar.CanDefault = true;
			this.cmdGrabar.CanFocus = true;
			this.cmdGrabar.Name = "cmdGrabar";
			this.cmdGrabar.UseUnderline = true;
			// Container child cmdGrabar.Gtk.Container+ContainerChild
			global::Gtk.Alignment w27 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w28 = new global::Gtk.HBox ();
			w28.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w29 = new global::Gtk.Image ();
			w29.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			w28.Add (w29);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w31 = new global::Gtk.Label ();
			w31.LabelProp = "_Guardar";
			w31.UseUnderline = true;
			w28.Add (w31);
			w27.Add (w28);
			this.cmdGrabar.Add (w27);
			this.AddActionWidget (this.cmdGrabar, 0);
			global::Gtk.ButtonBox.ButtonBoxChild w35 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w17 [this.cmdGrabar]));
			w35.Position = 1;
			w35.Expand = false;
			w35.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 1028;
			this.DefaultHeight = 300;
			this.Show ();
			this.tvLista.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnTvListaKeyPressEvent);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.cmdGrabar.Clicked += new global::System.EventHandler (this.OnCmdGrabarClicked);
		}
	}
}
