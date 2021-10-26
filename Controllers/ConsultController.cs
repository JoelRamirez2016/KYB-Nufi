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
		public SATData[] sat { get; set; }

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

            sat = ApiService.GetSAT("ADAME SILVA AURELIO", "AASA420925JN5").Result;
			//System.Console.WriteLine("El sat es");
			//System.Console.WriteLine(sat.Length);
			//System.Console.WriteLine(sat[0].rfc);
			//System.Console.WriteLine(sat[1]);

			List<Seccion> seccionesSatLista = new List<Seccion>();
				//System.Console.WriteLine("La longitud de secionesSat es:");
				//System.Console.WriteLine(seccionesSat.Length);
            int i = 0;
            foreach (var registro in sat)
            {
                i++;
                var datosSat = new Dato[]
                {
                        new Dato("Registro Federal de Contribuyentes (RFC)", registro.rfc),
                        new Dato("Número y Fecha de Oficio Global de Presunción", registro.fecha_oficio_global_presuncion),
                        new Dato("Publicación Página SAT Presuntos", registro.fecha_publi_sat_presuntos),
                        new Dato("Publicación Diario Oficial de la Federación Presuntos", registro.fecha_publi_dof_presuntos),
                };
				//System.Console.WriteLine("Los datos sat son");
				//System.Console.WriteLine(datosSat);
				//System.Console.WriteLine(datosSat.Length);
				//System.Console.WriteLine(datosSat[0]);
				//System.Console.WriteLine(datosSat[0].texto);
                seccionesSatLista.Add(new Seccion("Registro " + i.ToString(), datosSat));
            }
			var seccionesSat = seccionesSatLista.ToArray();

			System.Console.WriteLine("Las secciones son:");
			System.Console.WriteLine(seccionesSat.Length);

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

