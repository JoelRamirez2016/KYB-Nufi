using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nufi.kyb.v2.Models;
using Nufi.kyb.v2.Services;

namespace Nufi.kyb.v2.Controllers
{
    public class ConsultController : Controller
    {
        private readonly ILogger<ConsultController> _logger;
        public ActaConstitutiva actaConstitutiva { get; set; }
        public NufiApiService ApiService;
        public SATRequest sat { get; set; }

        public ConsultController(ILogger<ConsultController> logger, NufiApiService apiService)
        {
            _logger = logger;
            ApiService = apiService;
        }

        public IActionResult General()
        {
            string razonSocial = Request.Query["razonSocialField"];
            string rfc = Request.Query["rfcField"];
            string marca = Request.Query["marcaField"];

            // Acta Constitutiva API
            actaConstitutiva = ApiService.GetActaConstitutiva(razonSocial, rfc, marca).Result;
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
            sat = ApiService.GetSAT("ADAME SILVA AURELIO", rfc).Result;
            var seccionesSat = new Seccion[] { };
            List<Seccion> seccionesSatLista = new List<Seccion>();
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
                    seccionesSat = seccionesSatLista.ToArray();
                }
            }
            else
            {
                var datosSat = new Dato[]
                {
                    new Dato("Registro Federal de Contribuyentes (RFC)", rfc),
                    new Dato("Resultado", sat.message)
                };
                seccionesSatLista.Add(new Seccion("Resultado", datosSat));
                seccionesSat = seccionesSatLista.ToArray();
            }

            var impi = ApiService.GetIMPI("claro").Result;

            // Secciones para vista
            var superSecciones = new SuperSeccion[]
            {
                new SuperSeccion(false, "Información General", datosGenerales),
                new SuperSeccion(false, "Información Domiciliaria", datosDomiciliarios),
                new SuperSeccion(true, "Servicio de Administración Tributaria (SAT)", seccionesSat)
            };
            GeneralPage General = new GeneralPage(superSecciones);
            return View(General);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

