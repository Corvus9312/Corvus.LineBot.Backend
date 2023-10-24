using Corvus.LineBot.Backend.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static Corvus.LineBot.Backend.Enums.GptEmun;

namespace Corvus.LineBot.Backend.Services;

public class GptService
{
    private readonly string _key;
    private readonly ChatDataService _chatData;

    public GptService(IConfiguration config, ChatDataService chatData)
    {
        _key = config.GetSection("GptApiKey").Value;
        _chatData = chatData;
    }

    public async Task<string> PostGPT(string userID, string message)
    {
        var chatData = _chatData.GetChatDatas().SingleOrDefault(x => x.UserID.Equals(userID)) ?? new() { UserID = userID };

        var messages = chatData.GptMessages;
        messages.Add(new GptMessage
        {
            role = $"{Role.user}",
            content = message
        });

        HttpClient httpClient = new();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _key);

        var requestDatas = new GptReqVM
        {
            messages = messages,
            temperature = 0.7,
            max_tokens = 300,
            top_p = 1,
            frequency_penalty = 0,
            presence_penalty = 0
        };

        var result = string.Empty; 
        GptResVM resData = new();
        var isFrist = true;
        while (isFrist || (!resData.choices.FirstOrDefault()?.finish_reason.Equals("stop") ?? true))
        {
            isFrist = false;

            string requestJson = JsonSerializer.Serialize(requestDatas);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"https://api.openai.com/v1/chat/completions", content);

            var responseJson = await response.Content.ReadAsStringAsync();

            resData = JsonSerializer.Deserialize<GptResVM>(responseJson) ?? throw new NullReferenceException("respons msg is null");

            result += $"{resData.choices.FirstOrDefault()?.message.content}";

            requestDatas.messages.Add(new GptMessage { role = $"{Role.assistant}", content = resData.choices.FirstOrDefault()?.message.content ?? string.Empty });
        }

        chatData.GptMessages = requestDatas.messages;
        chatData.LastModifyTime = DateTime.Now;

        _chatData.SetChatDataMessage(chatData);

        return result;
    }
}
