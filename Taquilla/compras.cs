using System;
using Gtk;
namespace Taquilla
{
	public partial class compras : Gtk.Dialog
	{
		Gtk.ListStore listaStore = new Gtk.ListStore (typeof (string), typeof (int), typeof (string), typeof (double));

		public compras ()
		{
			this.Build ();
			tvLista.Model = listaStore;
			cafeteria.ConstruirLista(ref tvLista, ref listaStore);
			cafeteria.CargarListaCafeteria(ref listaStore);
			
		}
		
		protected virtual void OnButtonCancelClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
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
	}
	
	protected virtual void OnCmdGrabarClicked (object sender, System.EventArgs e)
	{
		TreeIter iter = new TreeIter();
		
		if (tvLista.Model.GetIterFirst(out iter)) {
			do {
				if (tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString() != "0")
				{
					MySQL.consultar("INSERT INTO `cafeteria_stock` (`ID_articulo`, `stock`, `fecha`, `ingresado_por`) " +
					"VALUES(" +
			                "'" + tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_ID_articulo).ToString() + "'," +
			                "'" + tvLista.Model.GetValue(iter,cafeteria.tvLista_Col_Cantidad).ToString() + "'," +
			                "NOW()," +
			                auth.ID_usuario +
			                ")");
				}
			} while(tvLista.Model.IterNext(ref iter));
		}
		MySQL.consultar("INSERT INTO `cafeteria_ingresos` (`ID_ingreso`, `descripcion`, `comprador`, `ingresado_por`, `fechatiempo`, `total_compra`) " +
		"VALUES(" +
		"0, " +
		"'"+txtDetalle.Buffer.Text+"'," +
		"'"+txtComprador.Text+"'," +
		""+auth.ID_usuario+"," +
		"NOW()," +
		"'"+txtTotalCompra.Text+"'" +
		")");

		Gtk.MessageDialog Mensaje = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close, "Ingreso realizado.\nEsta ventana se cerrará");
		Mensaje.Title="Éxito";
		Mensaje.Run();
		Mensaje.Destroy();
		this.Destroy();
		return;	
	}
}
}
