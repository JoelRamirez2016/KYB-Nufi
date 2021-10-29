using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

using Nufi.kyb.v2.Models;
using Nufi.kyb.v2.Services;


namespace Nufi.kyb.v2.Services
{
    public class CreatePageService
    {
        public NufiApiService ApiService;
        public IWebHostEnvironment WebHostEnvironment { get; }
        public CreatePageService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public InformPage CreateInformPage(ActaConstitutiva actaConstitutiva, SATRequest sat, string rfc)
        {
            SuperSeccion[] generalSection = CreateGeneralSections(actaConstitutiva, sat, rfc);
            InformPage generalPage = new InformPage(generalSection,
                    new SuperSeccion[]{},
                    new SuperSeccion[]{},
                    new SuperSeccion[]{},
                    new SuperSeccion[]{},
                    new SuperSeccion[]{});
            return generalPage;
        }
        
        public SuperSeccion[] CreateGeneralSections(ActaConstitutiva actaConstitutiva, SATRequest sat, string rfc)
        {
            var datosGenerales = new Dato[]
            {
                new Dato("Razón Social", actaConstitutiva.razon_social),
                new Dato("Marca", actaConstitutiva.marca),
                new Dato("Nacionalidad", actaConstitutiva.nacionalidad),
                new Dato("Registro Federal de Contribuyentes (RFC)", actaConstitutiva.rfc),
                new Dato("Fecha de Constitución", actaConstitutiva.fecha_constitucion),
                new Dato("Número de Escritura Constitutiva", actaConstitutiva.numero_escritura.ToString()),
                new Dato("Folio Mercantil", actaConstitutiva.folio_mercantil.ToString()),
                new Dato("Fecha de inscripción de PRP", actaConstitutiva.fecha_inscripcion_prp),
                new Dato("Sector", actaConstitutiva.sector),
                new Dato("Giro Mercantíl", actaConstitutiva.giro_mercantil),
                new Dato("Objeto Social", actaConstitutiva.objeto_social)
            };
            var datosDomiciliarios = new Dato[]
            {
                new Dato("Calle", actaConstitutiva.domicilio_fiscal.calle),
                new Dato("Número Exterior", actaConstitutiva.domicilio_fiscal.num_ext.ToString()),
                new Dato("Número Interior", actaConstitutiva.domicilio_fiscal.num_int.ToString()),
                new Dato("Entre Calles", actaConstitutiva.domicilio_fiscal.entre_calles),
                new Dato("Colonia", actaConstitutiva.domicilio_fiscal.colonia),
                new Dato("Código Postal", actaConstitutiva.domicilio_fiscal.codigo_postal.ToString()),
                new Dato("Entidad Federativa / Demarcación", actaConstitutiva.domicilio_fiscal.entidad_federativa),
                new Dato("País", actaConstitutiva.domicilio_fiscal.pais),
                new Dato("Delegación / Municipio", actaConstitutiva.domicilio_fiscal.municipio)
            };

            // SAT API
            List<Seccion> seccionesSatLista = new List<Seccion>();
            string tituloSeccionSat = "Servicio de Administración Tributaria (SAT)";
            SuperSeccion superSeccionSat;
            if (sat.data is not null)
            {
                int i = 0;
                foreach (var registro in sat.data)
                {
                    i++;

                    var datosSat = new Dato[]
                    {
                        new Dato("Registro Federal de Contribuyentes (RFC)", registro.rfc),
                        new Dato("Número y Fecha de Oficio Global Presuntos", registro.fecha_oficio_global_presuncion),
                        new Dato("Publicación Página SAT Presuntos", registro.fecha_publi_sat_presuntos),
                        new Dato("Publicación Diario Oficial de la Federación Presuntos", registro.fecha_publi_dof_presuntos),
                        new Dato("Número y Fecha de Oficio Global Desvirtuados", registro.fecha_oficio_global_desvirtuaron),
                        new Dato("Publicación Página SAT Desvirtuados", registro.fecha_publi_sat_desvirtuados),
                        new Dato("Publicación Diario Oficial de la Federación Desvirtuados", registro.fecha_publi_dof_desvirtuaron),
                        new Dato("Número y Fecha de Oficio Global Definitivos", registro.fecha_oficio_global_definitivos),
                        new Dato("Publicación Página SAT Definitivos", registro.fecha_publi_sat_definitivos),
                        new Dato("Publicación Diario Oficial de la Federación Definitivos", registro.fecha_publi_dof_definitivos),
                        new Dato("Número y Fecha de Oficio Global Favorables", registro.fecha_oficio_global_favorable),
                        new Dato("Publicación Página SAT Favorables", registro.fecha_publi_sat_favorable),
                        new Dato("Publicación Diario Oficial de la Federación Favorables", registro.fecha_publi_dof_favorable)
                    };
                    seccionesSatLista.Add(new Seccion("Registro " + i.ToString(), datosSat));
                }
                Seccion[] seccionesSat = seccionesSatLista.ToArray();
                superSeccionSat = new SuperSeccion(true,
                                                   tituloSeccionSat, 
                                                   seccionesSat,
                                                   null);
            }
            else
            {
                var datosSat = new Dato[]
                {
                    new Dato("Registro Federal de Contribuyentes (RFC)", rfc),
                    new Dato("Resultado", sat.message)
                };
                superSeccionSat = new SuperSeccion(false,
                                                   tituloSeccionSat,
                                                   null,
                                                   datosSat);
            }

            SuperSeccion[] superSecciones = new SuperSeccion[]
            {
                new SuperSeccion(false, "Información General", null, datosGenerales),
                new SuperSeccion(false, "Información Domiciliaria", null, datosDomiciliarios),
                superSeccionSat
            };
            return superSecciones;
        }
    }
}
