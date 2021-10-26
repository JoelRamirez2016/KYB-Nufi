namespace Nufi.kyb.v2.Models
{
	public class Domicilio
	{
        public string calle { get; set; }
        public int num_ext { get; set; }
        public int num_int { get; set; }
        public string colonia { get; set; }
        public int codigo_postal { get; set; }
        public string entre_calles { get; set; }
        public string entidad_federativa { get; set; }
        public string pais { get; set; }
        public string municipio { get; set; }
	}
}
