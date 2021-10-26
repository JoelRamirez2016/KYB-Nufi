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
	}
}
