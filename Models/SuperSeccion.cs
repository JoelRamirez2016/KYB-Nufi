namespace Nufi.kyb.v2.Models
{
    public class SuperSeccion
        {
            public SuperSeccion(string titulo, Seccion[] secciones)
            {
                this.titulo = titulo;
                this.secciones = secciones;
            }
            public string titulo { get; set; }
            public Seccion[] secciones { get; set; }
        }
}

