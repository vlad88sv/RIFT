using System;
using Gtk;

public static class cafeteria
{
	public const int tvLista_Col_ID_articulo = 0;
	public const int tvLista_Col_Cantidad = 1;
	public const int tvLista_Col_Descripcion = 2;
	public const int tvLista_Col_Precio = 3;

	public static void CargarListaCafeteria(ref Gtk.ListStore listaStore)
	{
	listaStore.Clear();
	MySQL.consultar("SELECT `ID_articulo`, `descripcion`, `precio` FROM cafeteria_articulos");
	/*
	// Miremos primero si no han agregado este articulo a la lista
	TreeIter iter = new TreeIter();
	Boolean Encontrado = false;
	if (MySQL.Reader.Read() && tvLista.Model.GetIterFirst(ref iter)) {
		do {
			if (tvLista.Model.GetValue(iter,0).ToString() == MySQL.Reader["ID_articulo"].ToString())
			{
				int CantidadActual = int.Parse(tvLista.Model.GetValue (iter, 1).ToString());
				tvLista.Model.SetValue(iter,1,++CantidadActual);
				Encontrado = true;
			}
		} while(tvLista.Model.IterNext(ref iter));

	}
	
	if (!Encontrado && MySQL.Reader.HasRows)
			listaStore.AppendValues (MySQL.Reader["ID_articulo"].ToString(),MySQL.Reader["codigo_barra"].ToString(), 0, MySQL.Reader["descripcion"].ToString(),double.Parse(MySQL.Reader["precio"].ToString()));		
	*/

	while (MySQL.Reader.Read())
		listaStore.AppendValues (MySQL.Reader["ID_articulo"].ToString(), 0, MySQL.Reader["descripcion"].ToString(),double.Parse(MySQL.Reader["precio"].ToString()));		
	}
	
	public static void ConstruirLista(ref Gtk.TreeView tvLista, ref Gtk.ListStore listaStore)
	{
		tvLista.AppendColumn ("ID", new Gtk.CellRendererText (), "markup", 			0);
		tvLista.AppendColumn ("Cantidad", new Gtk.CellRendererText (), "markup", 	1);
		tvLista.AppendColumn ("Descripcion", new Gtk.CellRendererText (), "markup", 2);
		tvLista.AppendColumn ("Precio", new Gtk.CellRendererText (), "markup", 		3);
		tvLista.Model = listaStore;
	}
}