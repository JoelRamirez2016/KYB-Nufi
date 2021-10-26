namespace Nufi.kyb.v2.Models
{
    public class SuperSeccion
        {
            public SuperSeccion(bool isSuper, string titulo, Seccion[] secciones)
            {
				this.isSuper = isSuper;
                this.titulo = titulo;
                this.secciones = secciones;
            }
            public SuperSeccion(bool isSuper, string titulo, Dato[] datos)
            {
				this.isSuper = isSuper;
                this.titulo = titulo;
				this.datos = datos;
            }
            public bool isSuper { get; set; }
            public string titulo { get; set; } 
            public Seccion[] secciones { get; set; } = {};
			public Dato[] datos {get; set; } = {};
        }
}

