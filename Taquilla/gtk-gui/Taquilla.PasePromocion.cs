
// This file has been generated by the GUI designer. Do not modify.
namespace Taquilla
{
	public partial class PasePromocion
	{
		private global::Gtk.Entry txtIngreso;
		private global::Gtk.HBox hbox1;
		private global::Gtk.RadioButton rdoPase;
		private global::Gtk.RadioButton rdoPromocion;
		private global::Gtk.RadioButton rdoEvento;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Taquilla.PasePromocion
			this.Name = "Taquilla.PasePromocion";
			this.Title = "Válidar pase, código promocional o certificado de evento";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			// Internal child Taquilla.PasePromocion.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.txtIngreso = new global::Gtk.Entry ();
			this.txtIngreso.CanFocus = true;
			this.txtIngreso.Name = "txtIngreso";
			this.txtIngreso.IsEditable = true;
			this.txtIngreso.InvisibleChar = '•';
			w1.Add (this.txtIngreso);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(w1 [this.txtIngreso]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Homogeneous = true;
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.rdoPase = new global::Gtk.RadioButton ("Pase");
			this.rdoPase.CanFocus = true;
			this.rdoPase.Name = "rdoPase";
			this.rdoPase.DrawIndicator = true;
			this.rdoPase.UseUnderline = true;
			this.rdoPase.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.hbox1.Add (this.rdoPase);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.rdoPase]));
			w3.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.rdoPromocion = new global::Gtk.RadioButton ("Código promoción");
			this.rdoPromocion.CanFocus = true;
			this.rdoPromocion.Name = "rdoPromocion";
			this.rdoPromocion.DrawIndicator = true;
			this.rdoPromocion.UseUnderline = true;
			this.rdoPromocion.Group = this.rdoPase.Group;
			this.hbox1.Add (this.rdoPromocion);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.rdoPromocion]));
			w4.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.rdoEvento = new global::Gtk.RadioButton ("Certificado evento");
			this.rdoEvento.CanFocus = true;
			this.rdoEvento.Name = "rdoEvento";
			this.rdoEvento.DrawIndicator = true;
			this.rdoEvento.UseUnderline = true;
			this.rdoEvento.Group = this.rdoPase.Group;
			this.hbox1.Add (this.rdoEvento);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.rdoEvento]));
			w5.Position = 2;
			w1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox1]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Internal child Taquilla.PasePromocion.ActionArea
			global::Gtk.HButtonBox w7 = this.ActionArea;
			w7.Name = "dialog1_ActionArea";
			w7.Spacing = 10;
			w7.BorderWidth = ((uint)(5));
			w7.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w7 [this.buttonCancel]));
			w8.Expand = false;
			w8.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w9 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w7 [this.buttonOk]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 411;
			this.DefaultHeight = 126;
			this.buttonOk.HasDefault = true;
			this.Show ();
			this.txtIngreso.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnTxtIngresoKeyPressEvent);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnButtonOkClicked);
		}
	}
}
