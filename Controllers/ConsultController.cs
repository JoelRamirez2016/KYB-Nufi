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
				new Dato("Delegación / Municipio", actaConstitutiva.domicilio_fiscal.municipio),
			};

			var secciones = new Seccion[]
			{
				new Seccion("Información General", datosGenerales),
				new Seccion("Información Domiciliaria", datosDomiciliarios)
			};

			GeneralPage General = new GeneralPage(secciones);
            return View(General);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

