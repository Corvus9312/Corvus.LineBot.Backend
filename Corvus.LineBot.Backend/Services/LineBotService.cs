using Corvus.LineBot.Backend.Helpers;
using Corvus.LineBot.Backend.Models;
using isRock.LineBot;
using static Corvus.LineBot.Backend.Enums.LineEnum;

namespace Corvus.LineBot.Backend.Services;

public class LineBotService
{
    private readonly LineBotHelper _linebot;
    private readonly GptService _gpt;

    public LineBotService(LineBotHelper lineBotHelper, GptService gpt)
    {
        _linebot = lineBotHelper;
        _gpt = gpt;
    }

    public async Task WebHook(HttpRequest req)
    {
        var token = _linebot.GetChannelAccessToken();
        var bot = new Bot(token);

        using StreamReader reader = new(req.Body);
        var reqBody = await reader.ReadToEndAsync();

        var rcvMsg = Utility.Parsing(reqBody);
        foreach (var e in rcvMsg.events)
        {
            var replyToken = e.replyToken;
            // var userId = e.source.userId;
            // var userName = bot.GetUserInfo(userId).displayName;
            var userMsg = e.message.text;

            var replayMsg = string.Empty;
            var msgType = (MessageType)Enum.Parse(typeof(MessageType), e.message.type);

            // message type explain:https://developers.line.biz/en/docs/messaging-api/message-types/
            switch (msgType)
            {
                case MessageType.text:
                    if (userMsg.ToLower().StartsWith("gpt:"))
                    {
                        var msg = userMsg.Remove(0, 4);
                        replayMsg = await _gpt.PostGPT(msg);
                    }
                    else
                    {
                        replayMsg = $"收到文字：{userMsg}";
                    }
                    break;
                case MessageType.image:
                    var fileBody = _linebot.GetUserUploadedContent(e.message.id);
                    replayMsg = $"收到圖片：大小為：{fileBody.Length} bytes。";
                    break;
                case MessageType.video:
                    break;
                case MessageType.audio:
                    break;
                case MessageType.file:
                    break;
                case MessageType.location:
                    break;
                case MessageType.sticker:
                    replayMsg = $"你收到了貼圖，貼圖為：{e.message.stickerId}";
                    break;
                default:
                    break;
            }

            bot.ReplyMessage(replyToken, replayMsg);
        }
    }

    public string PushMessage(PostMessageReqVM req) => _linebot.PushMessage(req.To, req.Message);
}
