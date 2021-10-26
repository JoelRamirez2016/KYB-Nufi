namespace Nufi.kyb.v2.Models
{
	public class ActaConstitutiva
 	{
		public string razon_social { get; set; } 
		public string marca { get; set; }
		public string nacionalidad { get; set; }
		public string rfc { get; set; }
		public int numero_escritura { get; set; }
		public string fecha_constitucion { get; set; }
		public int folio_mercantil { get; set; }
		public string fecha_inscripcion_prp { get; set; }
		public string sector { get; set; }
		public string giro_mercantil { get; set; }
		public string objeto_social { get; set; }
		public PersonaFisica[] representantes_legales { get; set; }
		public Domicilio domicilio_fiscal { get; set; }
		public Domicilio domicilio_comercial { get; set; }
		public PersonaFisica[] socios_fisicos { get; set; }
		public PersonaMoral[] socios_morales { get; set; }
	}
}

