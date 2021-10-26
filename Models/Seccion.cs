namespace Nufi.kyb.v2.Models
{
	public class Seccion
	{
		public Seccion (string titulo, Dato[] datos)
		{
			this.titulo = titulo;
			this.datos = datos;
		}
		public string titulo {get; set;}
		public Dato[] datos {get; set;}
	}
}

