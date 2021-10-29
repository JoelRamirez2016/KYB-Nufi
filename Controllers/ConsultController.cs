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
    public class ConsultController: Controller
    {
        private readonly ILogger<ConsultController> _logger;
        public NufiApiService ApiService;
        public CreatePageService CreatePageService { get; set; }
        public string GeneralPageJsonFile { get; } = "generalPage";
        ActaConstitutiva actaConstitutiva;

        public ConsultController(ILogger<ConsultController> logger,
                NufiApiService apiService,
                CreatePageService createPageService)
        {
            _logger = logger;
            ApiService = apiService;
            CreatePageService = createPageService;
        }

        // Post method for General Page
        [HttpPost]
        public IActionResult General(string razonSocial, string rfc, string marca)
        {
            actaConstitutiva = ApiService.GetActaConstitutiva(razonSocial, rfc, marca).Result;
            SATRequest sat = ApiService.GetSAT("ADAME SILVA AURELIO", rfc).Result;
            IMPIRequest impi = ApiService.GetIMPI(marca).Result;

            Page generalPage = CreatePageService.CreateGeneralPage(actaConstitutiva, sat, impi, rfc, marca);
            // CreatePageService.SavePage(generalPage, GeneralPageJsonFile);
            return View(generalPage);
        }

        //Get method for general page
        [HttpGet]
        public IActionResult General()
        {
            Page generalPage = CreatePageService.LoadPage(GeneralPageJsonFile);
            return View(generalPage);
        }

        public IActionResult Antecedentes() {
            var antecedentes = ApiService.GetAntecedentesPersonaMoralNacional("victor hugo",  "01-01-2020", "01-08-2020").Result;
            SuperSeccion superSeccionAnt;
            List<Seccion> seccionesLista = new List<Seccion>();

            if (antecedentes.data is not null)
            {

                foreach (var registro in antecedentes.data.resultados)
                {

                    var datos = new Dato[]
                    {
                        new Dato("Actor", "texto"),
                        new Dato("Demandado", "texto"),
                        new Dato("Fecha", "texto"),
                        new Dato("Fuero", "texto"),
                        new Dato("Juzgado", "texto"),
                        new Dato("Tipo", "texto"),

                    };
                    seccionesLista.Add(new Seccion("Entidad: " + registro.entidad, datos));
                }
                Seccion[] secciones = seccionesLista.ToArray();
                superSeccionAnt = new SuperSeccion(true,
                                                   "Antecedentes", 
                                                   secciones,
                                                   null);

            }
            else
            {
                var datos = new Dato[]
                {
                    new Dato("Registro Federal de Contribuyentes (RFC)", "0000"),
                    new Dato("Resultado", "nada")
                };
                superSeccionAnt = new SuperSeccion(false,
                                                   "nothin",
                                                   null,
                                                   datos);
            }

            SuperSeccion[] superSecciones = new SuperSeccion[]
            {
                superSeccionAnt
            };
            Page AntcPage = new Page(superSecciones);
            return View(AntcPage);
        }

        public IActionResult RepresentantesLegales() 
        {
            List<SuperSeccion> listaSuperSecciones = new List<SuperSeccion>();
            Console.WriteLine("acta:");
            Console.WriteLine(actaConstitutiva);
            foreach(var persona in actaConstitutiva.representantes_legales)
            {
                var datos = new Dato[]
                {
                    new Dato("nombres", "Alfredo"),
                    new Dato("apellido_paterno", "Delgado"),
                    new Dato("apellido_materno", "Moreno"),
                    new Dato("nacionalidad", "Colombiana"),
                    new Dato("fecha_nacimiento", "04-10-1991"),
                    new Dato("rfc", "1143840144"),
                    new Dato("genero", "Masculino"),
                    new Dato("pais_residencia", "Colombia"),
                    new Dato("pais_nacimiento", "Colombia"),
                    new Dato("entidad_federativa_nacimiento", "Valle del Cauca"),
                    new Dato("actividad_economica", "Musico"),
                    new Dato("telefono", "3006605195"),
                    new Dato("correo_electronico", "2917@holbertonschool.com"),
                    new Dato("curp", "1143840144"),
                    new Dato("domicilio", "No Data")
                };
                listaSuperSecciones.Add(new SuperSeccion(false, "Alfredo", null, datos));
            }
            Page RlPage = new Page(listaSuperSecciones.ToArray());
            return View(RlPage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}