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
	public class ConsumePlanetApi
	{
		public ConsumePlanetApi(IWebHostEnvironment webHostEnvironment,
				IHttpClientFactory clientFactory)
		{
			WebHostEnvironment = webHostEnvironment;
			_clientFactory = clientFactory;
		}
		public IWebHostEnvironment WebHostEnvironment { get; }
		private readonly IHttpClientFactory _clientFactory;
		public Planet planet { get; set; }
		public string planetString;

		public async Task<Planet> GetPlanet()
		//public async Task<string> GetPlanet()
        {
			var request = new HttpRequestMessage(HttpMethod.Get,
					"https://swapi-api.hbtn.io/api/planets/1");
			var client = _clientFactory.CreateClient();
			client.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json"));

			var response = await client.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStreamAsync();
				planet = await JsonSerializer.DeserializeAsync<Planet>(responseStream);
				System.Console.WriteLine("success");
				System.Console.WriteLine(planet.climate);
				using (var reader = new StreamReader(responseStream, Encoding.UTF8))
				{
					planetString = reader.ReadToEnd();
				}
				System.Console.WriteLine(planetString);
			}
			else
			{
				planet = new Planet();
				//planet = "";
			}
			return planet;
        }
	}
}
