using Corvus.LineBot.Backend.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static Corvus.LineBot.Backend.Enums.GptEmun;

namespace Corvus.LineBot.Backend.Services;

public class GptService
{
    private readonly string _key;

    public GptService(IConfiguration config)
    {
        _key = config.GetSection("GptApiKey").Value;
    }

    public async Task<string> PostGPT(string message)
    {
        var messages = new List<GptMessage>
        {
            new GptMessage
            {
                role = $"{Role.user}",
                content = message
            }
        };

        // 建立 HttpClient 物件
        HttpClient httpClient = new();

        // 加入 API Key 到 HTTP 請求標頭中
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _key);

        // 設定要發送的資料
        var requestDatas = new GptReqVM
        {
            messages = messages,
            temperature = 0.7,
            max_tokens = 60,
            top_p = 1,
            frequency_penalty = 0,
            presence_penalty = 0
        };

        string requestJson = JsonSerializer.Serialize(requestDatas);
        var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        // 發送 POST 請求
        var response = await httpClient.PostAsync($"https://api.openai.com/v1/chat/completions", content);

        // 讀取回應資料
        var responseJson = await response.Content.ReadAsStringAsync();

        // 解析回應資料
        var personData = JsonSerializer.Deserialize<GptResVM>(responseJson) ?? throw new NullReferenceException("respons msg is null");

        return personData.choices.FirstOrDefault()?.message.content ?? string.Empty;
    }
}
