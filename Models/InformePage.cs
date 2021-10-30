using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Nufi.kyb.v2.Services;
using Nufi.kyb.v2.Models;

namespace Nufi.kyb.v2.Models
{
    public class InformPage
    {
        public InformPage(SuperSeccion[] general,
                SuperSeccion[] antecedentes,
                SuperSeccion[] representantes,
                SuperSeccion[] sociosFisicos,
                SuperSeccion[] sociosMorales,
                SuperSeccion[] adjuntos)
        {
            GeneralSections = general;
            AntecedentesSections = antecedentes;
            RepresentantesSecitions = representantes;
            SociosFisicosSections = sociosFisicos;
            SociosMoralesSections = sociosMorales;
            AdjuntosSections = adjuntos;
        }
        public InformPage() {}

        public SuperSeccion[] GeneralSections { get; set; } = new SuperSeccion[]{};
        public SuperSeccion[] AntecedentesSections { get; set; } = new SuperSeccion[]{};
        public SuperSeccion[] RepresentantesSecitions { get; set; } = new SuperSeccion[]{};
        public SuperSeccion[] SociosFisicosSections { get; set; } = new SuperSeccion[]{};
        public SuperSeccion[] SociosMoralesSections { get; set; } = new SuperSeccion[]{};
        public SuperSeccion[] AdjuntosSections { get; set; } = new SuperSeccion[]{};
    }
}

