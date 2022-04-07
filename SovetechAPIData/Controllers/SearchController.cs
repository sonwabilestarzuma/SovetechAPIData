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
    public class SearchController : ControllerBase
    {
        [HttpGet("search")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<SearchResult> SearchResultAsync(string search)
        {
            SearchResult searchResult = new SearchResult();

            searchResult.chuckResult = await SearchChuckJokes(search);
            searchResult.swapiResult = await SearchSwapiPeople(search);

            return searchResult;
        }


        [HttpGet("searchChuck")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ChuckResult> SearchChuckJokes(string search)
        {
            ChuckResult jokeResult = new ChuckResult();
            string Url = "https://api.chucknorris.io/jokes/search?query=" + search;

            using (var httpClient = new HttpClient())
            {
                var apiEndPoint = Url;
                httpClient.BaseAddress = new Uri(apiEndPoint);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await httpClient.GetAsync(apiEndPoint);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                    jokeResult = JsonConvert.DeserializeObject<ChuckResult>(jsonContent);
                }

            }

            return jokeResult;
        }


        [HttpGet("searchSwapi")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<SwapiResult> SearchSwapiPeople(string search)
        {
            SwapiResult swapiResult = new SwapiResult();

            using (var httpClient = new HttpClient())
            {
                var apiEndPoint = "https://swapi.dev/api/people/?search=" + search;
                httpClient.BaseAddress = new Uri(apiEndPoint);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(apiEndPoint);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                    swapiResult = JsonConvert.DeserializeObject<SwapiResult>(jsonContent);

                    while (swapiResult.next != null)
                    {
                        var swapiMesage = await httpClient.GetAsync(swapiResult.next);

                        if (swapiMesage.IsSuccessStatusCode)
                        {
                            var swapiJsonContent = await swapiMesage.Content.ReadAsStringAsync();
                            var people = JsonConvert.DeserializeObject<SwapiResult>(swapiJsonContent);

                            swapiResult.results.AddRange(people.results);

                            swapiResult.next = people.next;
                        }
                    }

                }
            }

            return swapiResult;
        }
    }
}