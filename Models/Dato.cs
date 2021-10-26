namespace Nufi.kyb.v2.Models
{
	public class Dato
	{
		public Dato (string label, string texto)
		{
			this.label = label;
			this.texto = texto;
		}
		public string label { get; set; }
		public string texto { get; set; }

	}
}

