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
        public IActionResult Informe(string razonSocial, string rfc, string marca)
        {
            var today = DateTime.Today;
            string dateFormat = "dd-MM-yyyy";                        
            actaConstitutiva = ApiService.GetActaConstitutiva(razonSocial, rfc, marca).Result;
            SATRequest sat = ApiService.GetSAT(razonSocial, rfc).Result;
            IMPIRequest impi = ApiService.GetIMPI(marca).Result;
            var antecedentes = ApiService.GetAntecedentesPersonaMoralNacional(razonSocial,  "01-01-" + today.Year, today.ToString(dateFormat)).Result;

            InformPage page = CreatePageService.CreateInformPage(actaConstitutiva, sat, impi, rfc, marca, antecedentes);
            //InformPage page = CreatePageService.CreateInformPage(null, null, null, null, null, null);
            return View(page);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
