using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class TranslationYa_input
{

    public static async Task<string> TranslateText(string text, string sourceLang, string targetLang)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Api-Key", Config.apiKey);
            var requestBody = new
            {
                targetLanguageCode = targetLang,
                sourceLanguageCode = sourceLang,
                texts = new[] { text }
            };
            var json = JsonConvert.SerializeObject(requestBody);
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(Config.apiUrl, content);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
            return jsonResponse.translations[0].text;
        }
    }
}