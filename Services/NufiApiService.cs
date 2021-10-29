using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Nufi.kyb.v2.Models;

namespace Nufi.kyb.v2.Services
{
    public class NufiApiService
    {
        public NufiApiService(IWebHostEnvironment webHostEnvironment,
                IHttpClientFactory clientFactory)
        {
            WebHostEnvironment = webHostEnvironment;
            _clientFactory = clientFactory;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly IHttpClientFactory _clientFactory;
        public ActaConstitutiva actaConstitutiva { get; set; }
        public SATRequest satRequest { get; set; }

        public async Task<ActaConstitutiva> GetActaConstitutiva(
                string razonSocial,
                string rfc,
                string marca)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                    "https://stoplight.io/mocks/alfredpianist/kyb-api/23013508/actas_constitutivas/consultar");
            request.Content = new StringContent(JsonSerializer.Serialize(
                        new Dictionary<string, string>(){
                        {"razon_social", razonSocial},
                        {"rfc", rfc},
                        {"marca", marca}}
                        ));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                actaConstitutiva = await JsonSerializer.DeserializeAsync<ActaConstitutiva>(responseStream);
            }
            else
            {
                actaConstitutiva = new ActaConstitutiva();
            }
            return actaConstitutiva;
        }

        public async Task<SATRequest> GetSAT(
                string nombre,
                string rfc)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                    "https://nufi.azure-api.net/contribuyentes/v1/obtener_contribuyente");
            request.Content = new StringContent(JsonSerializer.Serialize(
                        new Dictionary<string, string>(){
                        {"nombre", nombre},
                        {"rfc", rfc}}
                        ));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7bafe13ba4d9450f88a39922bdec4f03");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                satRequest = await JsonSerializer.DeserializeAsync<SATRequest>(responseStream);
            }
            else
            {
                satRequest = new SATRequest();
            }            
        // Console.WriteLine(JsonSerializer.Serialize(satRequest, new JsonSerializerOptions { WriteIndented = true }));
            return satRequest;
        }
    
        public async Task<RUGRequest> GetRUG(
            string descripcion_de_bienes,
            string nombre_otorgante,
            string folio_electronico_otorgante,
            string numero_garantia_o_asiento,
            string curp_otorgante,
            string rfc_otorgante)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                    "https://nufi.azure-api.net/consulta_rug/consultas/garantias_inmobiliarias");
            request.Content = new StringContent(JsonSerializer.Serialize(
                        new Dictionary<string, string>(){
                            {"descripcion_de_bienes", descripcion_de_bienes},
                            {"nombre_otorgante", nombre_otorgante},
                            {"folio_electronico_otorgante", folio_electronico_otorgante},
                            {"numero_garantia_o_asiento", numero_garantia_o_asiento},
                            {"curp_otorgante", curp_otorgante},
                            {"rfc_otorgante", rfc_otorgante}}
                        ));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7bafe13ba4d9450f88a39922bdec4f03");

            var response = await client.SendAsync(request);
            var rugRequest = new RUGRequest();

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                rugRequest = await JsonSerializer.DeserializeAsync<RUGRequest>(responseStream);
            }          
        // Console.WriteLine(JsonSerializer.Serialize(satRequest, new JsonSerializerOptions { WriteIndented = true }));
            return rugRequest;
        }

        public async Task<IMPIRequest> GetIMPI(string marca)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                    "https://nufi.azure-api.net/trademark/v1/find");
            request.Content = new StringContent(JsonSerializer.Serialize(
                        new Dictionary<string, string>(){{"name", marca}}));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7bafe13ba4d9450f88a39922bdec4f03");

            var response = await client.SendAsync(request);
            var impiRequest = new IMPIRequest();

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                impiRequest = await JsonSerializer.DeserializeAsync<IMPIRequest>(responseStream);
            }          
            return impiRequest;
        }

        public async Task<AntecedentesPMNRequest> GetAntecedentesPersonaMoralNacional(
            string nombre, string fecha_inicio, string fecha_fin)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                    "https://nufi.azure-api.net/antecedentes_judiciales/v2/persona_moral_nacional");
            request.Content = new StringContent(JsonSerializer.Serialize(
                        new Dictionary<string, string>(){
                            {"nombre", nombre},
                            {"fecha_inicio", fecha_inicio},
                            {"fecha_fin", fecha_fin}}
                        ));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("NUFI-API-KEY", "7bafe13ba4d9450f88a39922bdec4f03");

            var response = await client.SendAsync(request);
            var AntecedentesPMNrequest = new AntecedentesPMNRequest();

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                AntecedentesPMNrequest = await JsonSerializer.DeserializeAsync<AntecedentesPMNRequest>(responseStream);
                Console.WriteLine(JsonSerializer.Serialize(AntecedentesPMNrequest, new JsonSerializerOptions { WriteIndented = true }));
            }          
            return AntecedentesPMNrequest;
        }
    }
}
