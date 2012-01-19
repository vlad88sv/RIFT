using System;
using System.IO;
using System.Diagnostics;

// Simple clase que toca música vía mpg123.
// El objetivo es abstraer el concepto de "On/Off" para la tocar la música de un directorio
// en forma aleatoria. Para sonar la música solo se crea la clase,
// y ejecutar "IniciarMusica" para actualizar la lista e iniciar mpg123 con la lista,
// llamar a "PararMusica" para destruir el proceso, terminando la música.

namespace Taquilla
{
	public class Musica
	{
		public ProcessStartInfo mpg123 = new ProcessStartInfo();
		public Process mpg123Proc = new Process();
		public Musica ()
		{
		}
		
		public void IniciarMusica ()
		{
			string[] filePaths = Directory.GetFiles(@Environment.GetEnvironmentVariable("HOME")+"/lobby/", "*.mp3", SearchOption.AllDirectories);		
			
			if (filePaths.Length == 0) return;
			
			string canciones = "'" + string.Join("' '",filePaths) + "'";

        	mpg123.FileName = "mpg123";
        	mpg123.Arguments = "--random --quiet " + canciones;
			mpg123.UseShellExecute = true;
	        mpg123Proc = Process.Start(mpg123);
		}
		
		public void PararMusica ()
		{
			if (!mpg123Proc.HasExited)
				mpg123Proc.Kill();
		}
	}
}