using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using SovetechAPIData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SovetechAPIData.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        [HttpGet("people")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<SwapiResult> GetSwapiPeople()
        {
            SwapiResult swapiResult = new SwapiResult();

            using (var httpClient = new HttpClient())
            {
                var apiEndPoint = "https://swapi.dev/api/people/";
                httpClient.BaseAddress = new Uri(apiEndPoint);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(apiEndPoint);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                    swapiResult = JsonConvert.DeserializeObject<SwapiResult>(jsonContent);

                    do
                    {
                        var swapiMesage = await httpClient.GetAsync(swapiResult.next);

                        if (swapiMesage.IsSuccessStatusCode)
                        {
                            var swapiJsonContent = await swapiMesage.Content.ReadAsStringAsync();
                            var people = JsonConvert.DeserializeObject<SwapiResult>(swapiJsonContent);

                            swapiResult.results.AddRange(people.results);

                            swapiResult.next = people.next;
                        }

                    } while (swapiResult.next != null);

                }
            }

            return swapiResult;
        }
    }
}