namespace Nufi.kyb.v2.Models
{
	public class PersonaFisica
	{
        public string nombres { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string nacionalidad { get; set; }
        public string fecha_nacimiento { get; set; }
        public string rfc { get; set; }
        public string genero { get; set; }
        public string pais_residencia { get; set; }
        public string pais_nacimiento { get; set; }
        public string entidad_federativa_nacimiento { get; set; }
        public string actividad_economica { get; set; }
        public string telefono { get; set; }
        public string correo_electronico { get; set; }
        public string curp { get; set; }
        public Domicilio domicilio { get; set; }
    }
}
