using System;
using Gtk;

public class Resultadotiquete
{
	public bool error = false;
	public double precio_grabado 	= 0.00;
	public string transaccion 		= "";
	public string ID_tipo_boleto 	= "";
	public string ID_ticket			= "";
	public string extra 			= "";
}

public static class tiquete
{
	public static double PrecioBase  		= 0.00;
	public const string ID_tipo_normal 		= "1";
	public const string ID_tipo_cancelado 	= "2";
	public const string ID_tipo_movimiento 	= "4";
	public const string ID_tipo_gratis 		= "5";
	public const string ID_tipo_digicel 	= "6";
	public const string ID_tipo_iva 		= "9";
	public const string ID_tipo_pase 		= "11";
	
	public static Resultadotiquete GrabarImprimirTiquete(Resultadotiquete Resultado, string complemento) {	
		string c = "INSERT INTO tickets (`fecha_juego`,`numero_jugador`,`precio_grabado`,`fecha_vendido`,`ID_tipo_boleto`,`ID_pase`) SELECT '" + global.fechaDiaTrabajoMasJuegoFMySQL+"', (SELECT COALESCE(MAX(`numero_jugador`)+1,0) FROM `tickets` WHERE `fecha_juego` = '" + global.fechaDiaTrabajoMasJuegoFMySQL+"'), '" + Resultado.precio_grabado + "', NOW(), '" + Resultado.ID_tipo_boleto + "', '" + Resultado.extra + "'";
		MySQL.consultar(c);

		if (MySQL.consultar("SELECT MAX(ID_ticket) AS LAST_ID_ticket FROM tickets"))
		{
			MySQL.Reader.Read();
			Resultado.ID_ticket = MySQL.Reader["LAST_ID_ticket"].ToString();
		}
	
		ImprimirTiquete(Resultado, complemento);
		return Resultado;
	}
	
	public static Resultadotiquete EjecutarValidacionPasePromocion(bool Promocion)
	{
		Resultadotiquete Resultado = new Resultadotiquete();
		Taquilla.PasePromocion wPasePromocion = new Taquilla.PasePromocion(Promocion);
		wPasePromocion.Modal = true;
		Gtk.ResponseType Respuesta = (Gtk.ResponseType) wPasePromocion.Run();

		switch (Respuesta)
		{
			case ResponseType.Close:
				Resultado.error = true;
			break;
			case ResponseType.Ok:
				Resultado = wPasePromocion.Resultado;
				GrabarImprimirTiquete(Resultado,"\n"+Imprimidor.Imprimir("PROMOCION/PASE UTILIZADO",1));
			break;
			default:
				Console.WriteLine(Respuesta.ToString());
			break;
		}
		wPasePromocion.Destroy();

		return Resultado;
	}

	public static Resultadotiquete EjecutarValidacionMovimiento(){
		Resultadotiquete Resultado = new Resultadotiquete();
		Taquilla.movimientoTiquete wmovimientoTiquete = new Taquilla.movimientoTiquete();
		wmovimientoTiquete.Modal = true;
		Gtk.ResponseType Respuesta = (Gtk.ResponseType) wmovimientoTiquete.Run();
		
		switch (Respuesta)
		{
			case ResponseType.DeleteEvent:
			case ResponseType.Cancel:
				Resultado.error = true;
			break;
			case ResponseType.Ok:
				Resultado = wmovimientoTiquete.Resultado;
				GrabarImprimirTiquete(Resultado,"\n"+Imprimidor.Imprimir("CAMBIO DE HORA **GRATIS**",1));
			break;
			default:
				Console.WriteLine(Respuesta.ToString());
			break;
		}
		wmovimientoTiquete.Destroy();

		return Resultado;
	}
	
	public static Resultadotiquete EjecutarValidacionCancelacion(){
		Resultadotiquete Resultado = new Resultadotiquete();
		return Resultado;
	}
	
	public static Resultadotiquete EjecutarValidacionTiqueteNormal(){
		Resultadotiquete Resultado = new Resultadotiquete();
		Resultado.ID_tipo_boleto = tiquete.ID_tipo_normal;
		Resultado.precio_grabado = tiquete.PrecioBase;
		Resultado.transaccion = "normal";
		return GrabarImprimirTiquete(Resultado, "");
	}
	
	public static Resultadotiquete EjecutarValidacionIVAExento(){
		Resultadotiquete Resultado = new Resultadotiquete();
		Resultado.ID_tipo_boleto = tiquete.ID_tipo_iva;
		Resultado.precio_grabado = (tiquete.PrecioBase / 1.13);
		Resultado.transaccion = "I.V.A Exento";
		return GrabarImprimirTiquete(Resultado, "\n"+Imprimidor.Imprimir("** I.V.A. Exento **", 1));
	}
	
	private static void ImprimirTiquete(Resultadotiquete Resultado, string complemento) {
		string Tiquete = "";
		Tiquete += Imprimidor.Imprimir("FECHA DE JUEGO:",global.fechaDiaTrabajo);
		Tiquete += Imprimidor.Imprimir("HORA DE JUEGO:",global.HoraJuego);
		Tiquete += Imprimidor.Imprimir("\n1 Juego RIFT", "$" + Resultado.precio_grabado.ToString("0.00") + " G");
		Tiquete += complemento;
		Imprimidor.Tiquete(Tiquete,Resultado.ID_ticket);
	}
}