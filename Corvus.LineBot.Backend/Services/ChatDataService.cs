using Corvus.LineBot.Backend.Models;

namespace Corvus.LineBot.Backend.Services;

public class ChatDataService
{
    private List<ChatDataModel> ChatDatas { get; set; } = new();

    public List<ChatDataModel> GetChatDatas() => ChatDatas = ChatDatas.Where(x => x.LastModifyTime > DateTime.Now.AddHours(-1)).ToList();

    public void SetChatDataMessage(ChatDataModel chatData)
    {
        var data = ChatDatas.SingleOrDefault(x => x.UserID.Equals(chatData.UserID));

        if (data is null)
            AddChatData(chatData);
        else
            data = chatData;
    }

    public void AddChatData(ChatDataModel chatData) => ChatDatas.Add(chatData);
}


public class ChatDataModel
{
    public string UserID { get; set; } = null!;

    public List<GptMessage> GptMessages { get; set; } = new();

    public DateTime LastModifyTime { get; set; } = DateTime.MinValue;
}