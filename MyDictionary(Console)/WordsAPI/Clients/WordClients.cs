using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyDictionary_Console_.WordsAPI.Models;
using MyDictionary_Console_.Constants;
using System.Net.Http;

namespace MyDictionary_Console_.WordsAPI.Clients
{
    internal class WordClients
    {
        private static string _ApiKey;
        private static string _ApiHost;
        private HttpClient _httpClient;
        public WordClients()
        {
            _ApiKey = Constant.ApiKey;
            _ApiHost = Constant.WordsApiHost;
            _httpClient = new HttpClient();
        }
        public async Task<SynonimModel> GetSynonim(string word)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://wordsapiv1.p.rapidapi.com/words/{word}/examples"),
                Headers =
                {
                    { "X-RapidAPI-Key", _ApiKey },
                    { "X-RapidAPI-Host", _ApiHost },
                },
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SynonimModel>(result);
        }
        public async Task<DefinitionModel> GetDefinition(string word)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://wordsapiv1.p.rapidapi.com/words/{word}/definitions"),
                Headers =
                {
                    { "X-RapidAPI-Key", _ApiKey },
                    { "X-RapidAPI-Host", _ApiHost },
                },
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DefinitionModel>(result);
        }
        public async Task<ExamplesModel> GetExample(string word)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://wordsapiv1.p.rapidapi.com/words/{word}/examples"),
                Headers =
                {
                    { "X-RapidAPI-Key", _ApiKey },
                    { "X-RapidAPI-Host", _ApiHost },
                },
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExamplesModel>(result);
        }
        

    }
}
