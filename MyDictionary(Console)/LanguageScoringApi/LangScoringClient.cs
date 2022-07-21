using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyDictionary_Console_.Constants;
using Newtonsoft.Json;
namespace MyDictionary_Console_.LanguageScoringApi
{
    internal class LangScoringClient
    {
        private static string _ApiKey;
        private static string _ApiHost;
        private HttpClient _httpClient;
        public LangScoringClient()
        {
            _ApiKey = Constant.ApiKey;
            _ApiHost = Constant.LangScoringHost;
            _httpClient = new HttpClient();
        }
        public async Task<LangScoringModel> GetWordScore(string word)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://twinword-language-scoring.p.rapidapi.com/word/?entry={word}"),
                Headers =
                {
                    { "X-RapidAPI-Key", _ApiKey },
                    { "X-RapidAPI-Host", _ApiHost },
                },
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LangScoringModel>(result);
        }
        public async Task<LangScoringModel> GetTextScore(string word)
        {
             
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://twinword-language-scoring.p.rapidapi.com/text/"),
                Headers =
                {
                    { "X-RapidAPI-Key", _ApiKey },
                    { "X-RapidAPI-Host", _ApiHost },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "text", word},
                }),
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LangScoringModel>(result);
        }
    }
}
