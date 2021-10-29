using System;
using System.Web.Mvc;
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
            ActaConstitutiva actaConstitutiva = ApiService.GetActaConstitutiva(razonSocial, rfc, marca).Result;
            SATRequest sat = ApiService.GetSAT("ADAME SILVA AURELIO", rfc).Result;
            //var impi = ApiService.GetIMPI("claro").Result;

            Page generalPage = CreatePageService.CreateGeneralPage(actaConstitutiva, sat, rfc);

            CreatePageService.SavePage(generalPage, GeneralPageJsonFile);

            return RedirecToAction("LoadConsult");
        }

        public IActionResult LoadConsult(Page page)
        {
            return View(page);
        }

        //Get method for general page
        [HttpGet]
        public IActionResult General()
        {
            Page generalPage = CreatePageService.LoadPage(GeneralPageJsonFile);
            return RedirecToAction(generalPage);
        }

        public IActionResult Antecedentes()
        {
            Page Antecedentes = new Page();
            return View(Antecedentes);
        }

        public IActionResult RepresentantesLegales()
        {
            Page representantes = new Page();
            return View(representantes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

