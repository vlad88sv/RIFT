
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

		private global::Gtk.HSeparator hseparator2;

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
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label5]));
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
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.txtComprador]));
			w3.Position = 1;
			this.vbox2.Add (this.hbox3);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox3]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator2 = new global::Gtk.HSeparator ();
			this.hseparator2.Name = "hseparator2";
			this.vbox2.Add (this.hseparator2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hseparator2]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			this.hbox2.Add (this.vbox2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.vbox2]));
			w6.Position = 0;
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
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.GtkScrolledWindow1]));
			w8.Position = 1;
			w1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(w1[this.hbox2]));
			w9.Position = 0;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			w1.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(w1[this.hseparator1]));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.dialog1_ActionArea1 = new global::Gtk.HButtonBox ();
			this.dialog1_ActionArea1.Name = "dialog1_ActionArea1";
			this.dialog1_ActionArea1.Spacing = 10;
			this.dialog1_ActionArea1.BorderWidth = ((uint)(5));
			this.dialog1_ActionArea1.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			w1.Add (this.dialog1_ActionArea1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(w1[this.dialog1_ActionArea1]));
			w11.PackType = ((global::Gtk.PackType)(1));
			w11.Position = 3;
			w11.Expand = false;
			w11.Fill = false;
			// Internal child Taquilla.compras.ActionArea
			global::Gtk.HButtonBox w12 = this.ActionArea;
			w12.Name = "dialog1_ActionArea";
			w12.Spacing = 10;
			w12.BorderWidth = ((uint)(5));
			w12.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			// Container child buttonCancel.Gtk.Container+ContainerChild
			global::Gtk.Alignment w13 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w14 = new global::Gtk.HBox ();
			w14.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w15 = new global::Gtk.Image ();
			w15.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
			w14.Add (w15);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w17 = new global::Gtk.Label ();
			w17.LabelProp = "_Cancelar";
			w17.UseUnderline = true;
			w14.Add (w17);
			w13.Add (w14);
			this.buttonCancel.Add (w13);
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w21 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w12[this.buttonCancel]));
			w21.Expand = false;
			w21.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.cmdGrabar = new global::Gtk.Button ();
			this.cmdGrabar.CanDefault = true;
			this.cmdGrabar.CanFocus = true;
			this.cmdGrabar.Name = "cmdGrabar";
			this.cmdGrabar.UseUnderline = true;
			// Container child cmdGrabar.Gtk.Container+ContainerChild
			global::Gtk.Alignment w22 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w23 = new global::Gtk.HBox ();
			w23.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w24 = new global::Gtk.Image ();
			w24.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			w23.Add (w24);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w26 = new global::Gtk.Label ();
			w26.LabelProp = "_Guardar";
			w26.UseUnderline = true;
			w23.Add (w26);
			w22.Add (w23);
			this.cmdGrabar.Add (w22);
			this.AddActionWidget (this.cmdGrabar, 0);
			global::Gtk.ButtonBox.ButtonBoxChild w30 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w12[this.cmdGrabar]));
			w30.Position = 1;
			w30.Expand = false;
			w30.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 1284;
			this.DefaultHeight = 300;
			this.Show ();
			this.tvLista.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnTvListaKeyPressEvent);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.cmdGrabar.Clicked += new global::System.EventHandler (this.OnCmdGrabarClicked);
		}
	}
}
