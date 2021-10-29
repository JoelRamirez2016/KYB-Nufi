using System.Collections.Generic;

namespace Nufi.kyb.v2.Models
{
    public class Acuerdo
    {
        public string acuerdo { get; set; }
        public string fecha { get; set; }
    }

    public class Expediente
    {
        public string expediente { get; set; }
        public string actor { get; set; }
        public string demandado { get; set; }
        public string entidad { get; set; }
        public string juzgado { get; set; }
        public string tipo { get; set; }
        public string fuero { get; set; }
        public string fecha { get; set; }    
        public Acuerdo[] acuerdos { get; set; }
    }

    public class Entidad
    {
        public string entidad { get; set; }
        public Expediente[] expedientes { get; set; }
    }

    public class AntecedentesPMN
    {
        public int numero_resultados { get; set; }
        public Entidad[] resultados { get; set; }
    }
} 
