namespace Nufi.kyb.v2.Models
{
	public class PersonaMoral
	{
        public string razon_social { get; set; }
        public string rfc { get; set; }
        public double participacion_social { get; set; }
        public string giro_mercantil { get; set; }
        public string nacionalidad { get; set; }
        public PersonaFisica[] socios { get; set; }
        public Domicilio domicilio { get; set; }
    }
}
