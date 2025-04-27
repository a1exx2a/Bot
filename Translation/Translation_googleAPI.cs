using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

class TranslationGo_input
{

    public static async Task<string> TranslateText(string sourceLang, string targetLang,string text)
    {
        HttpClient httpClient = new HttpClient();
        var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLang}&tl={targetLang}&dt=t&q={Uri.EscapeDataString(text)}";
        var response = await httpClient.GetStringAsync(url);

        var result = JsonSerializer.Deserialize<List<object>>(response);
        return result?[0]?.ToString()?.Split('"')[1] ?? "Ошибка: Нет перевода";
    }
}