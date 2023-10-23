using isRock.LineBot;

namespace Corvus.LineBot.Backend.Helpers;

public class LineBotHelper
{
    private readonly string _channelAccessToken;
    private readonly string _developerLineID;
    private readonly string _developerUserID;

    public LineBotHelper(IConfiguration config)
    {
        _channelAccessToken = config.GetSection("LineAccessToken").Value;
        _developerLineID = config.GetSection("DeveloperLineID").Value;
        _developerUserID = config.GetSection("DeveloperUserID").Value;
    }

    public string GetDeveloperLineID() => _developerLineID;

    public string GetDeveloperUserID() => _developerUserID;

    public string GetChannelAccessToken() => _channelAccessToken;

    public string PushMessage(string ToUserID, MessageBase Message)
    {
        return GetBotInstance().PushMessage(ToUserID, Message);
    }

    public string PushMessage(string ToUserID, List<MessageBase> Messages)
    {
        return GetBotInstance().PushMessage(ToUserID, Messages);
    }

    public string PushMessagesWithJSON(string ToUserID, string JSONMessages)
    {
        return Utility.PushMessagesWithJSON(ToUserID, JSONMessages, _channelAccessToken);
    }

    public string PushMessage(string ToUserID, ImagemapMessage Message)
    {
        return Utility.PushImageMapMessage(ToUserID, Message, _channelAccessToken);
    }

    public string PushMessage(string ToUserID, ButtonsTemplate Message)
    {
        return Utility.PushTemplateMessage(ToUserID, Message, _channelAccessToken);
    }

    public string PushMessage(string ToUserID, ConfirmTemplate Message)
    {
        return Utility.PushTemplateMessage(ToUserID, Message, _channelAccessToken);
    }

    public string PushMessage(string ToUserID, CarouselTemplate Message)
    {
        return Utility.PushTemplateMessage(ToUserID, Message, _channelAccessToken);
    }

    public string PushMessage(string ToUserID, string TextMessage)
    {
        if (TextMessage.Length < 0)
        {
            throw new Exception("訊息內容不正確");
        }

        if (TextMessage.Length > 1800)
        {
            throw new Exception("訊息內容太長");
        }

        return Utility.PushMessage(ToUserID, TextMessage, _channelAccessToken);
    }

    public string PushMessage(string ToUserID, Uri ContentUrl)
    {
        return Utility.PushImageMessage(ToUserID, ContentUrl.ToString(), ContentUrl.ToString(), _channelAccessToken);
    }

    public string PushMessage(string ToUserID, Uri originalContentUrl, Uri previewImageUrl)
    {
        return Utility.PushImageMessage(ToUserID, originalContentUrl.ToString(), previewImageUrl.ToString(), _channelAccessToken);
    }

    public string PushMessage(string ToUserID, int packageId, int stickerId)
    {
        return Utility.PushStickerMessage(ToUserID, packageId, stickerId, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, MessageBase Message)
    {
        return GetBotInstance().ReplyMessage(ReplyToken, Message);
    }

    public string ReplyMessage(string ReplyToken, List<MessageBase> Messages)
    {
        return GetBotInstance().ReplyMessage(ReplyToken, Messages);
    }

    public string ReplyMessageWithJSON(string ReplyToken, string JSONMessages)
    {
        return Utility.ReplyMessageWithJSON(ReplyToken, JSONMessages, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, ImagemapMessage ImagemapMessage)
    {
        return Utility.ReplyImageMapMessage(ReplyToken, ImagemapMessage, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, ConfirmTemplate ConfirmTemplate)
    {
        return Utility.ReplyTemplateMessage(ReplyToken, ConfirmTemplate, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, CarouselTemplate CarouselTemplate)
    {
        return Utility.ReplyTemplateMessage(ReplyToken, CarouselTemplate, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, ImageCarouselTemplate ImageCarouselTemplate)
    {
        return Utility.ReplyTemplateMessage(ReplyToken, ImageCarouselTemplate, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, ButtonsTemplate ButtonsTemplate)
    {
        return Utility.ReplyTemplateMessage(ReplyToken, ButtonsTemplate, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, string Message)
    {
        if (Message.Length < 0)
        {
            throw new Exception("訊息內容不正確");
        }

        if (Message.Length > 1800)
        {
            throw new Exception("訊息內容太長");
        }

        return Utility.ReplyMessage(ReplyToken, Message, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, int packageId, int stickerId)
    {
        return Utility.ReplyStickerMessage(ReplyToken, packageId, stickerId, _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, Uri ContentUrl)
    {
        return Utility.ReplyImageMessage(ReplyToken, ContentUrl.ToString(), ContentUrl.ToString(), _channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, Uri originalContentUrl, Uri previewImageUrl)
    {
        return Utility.ReplyImageMessage(ReplyToken, originalContentUrl.ToString(), previewImageUrl.ToString(), _channelAccessToken);
    }

    private Bot GetBotInstance()
    {
        if (string.IsNullOrEmpty(_channelAccessToken))
        {
            throw new Exception("ChannelAccessToken cannot be empty.");
        }

        return new Bot(_channelAccessToken);
    }

    public byte[] GetUserUploadedContent(string ContentID)
    {
        return GetBotInstance().GetUserUploadedContent(ContentID);
    }

    public LineUserInfo GetUserInfo(string UserUid)
    {
        return GetBotInstance().GetUserInfo(UserUid);
    }

    public GroupSummary GetGroupSummary(string groupId)
    {
        return GetBotInstance().GetGroupSummary(groupId);
    }

    public int GetMembersInGroupCount(string groupId)
    {
        return GetBotInstance().GetMembersInGroupCount(groupId);
    }

    public int GetMembersInRoomCount(string roomId)
    {
        return GetBotInstance().GetMembersInRoomCount(roomId);
    }
}
