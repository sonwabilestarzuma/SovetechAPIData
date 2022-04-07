using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SovetechAPIData.Models;
using SovetechAPIData.Services;
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
    public class ChuckNorrisController : ControllerBase
    {
        [HttpGet("categories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<List<string>> GetCategory()
        {
            var response = new PagedResponse<Joke>();

            List<string> jokes = new List<string>();

            using (var httpClient = new HttpClient())
            {
                var apiEndPoint = "https://api.chucknorris.io/jokes/categories";
                httpClient.BaseAddress = new Uri(apiEndPoint);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(apiEndPoint);


                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonContent);

                    foreach (string item in data)
                    {
                        jokes.Add(item);
                    }
                }
            }
            return jokes;
        }


        [HttpGet("random")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<Joke> RandomJokesFromCategory(string category)
        {
            Joke jokeResult = new Joke();
            string Url = "https://api.chucknorris.io/jokes/random?category=" + category;

            using (var httpClient = new HttpClient())
            {
                var apiEndPoint = Url;
                httpClient.BaseAddress = new Uri(apiEndPoint);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await httpClient.GetAsync(apiEndPoint);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                    jokeResult = JsonConvert.DeserializeObject<Joke>(jsonContent);
                }

            }

            return jokeResult;
        }
    }
}