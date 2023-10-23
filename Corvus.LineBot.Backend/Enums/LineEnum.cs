namespace Corvus.LineBot.Backend.Enums;

public class LineEnum
{
    public enum EventType
    {
        message,
        unsend,
        follow,
        unfollow,
        join,
        leave,
        memberJoined,
        memberLeft,
        postback,
        videoPlayComplete
    }

    public enum MessageType
    {
        text,
        image,
        video,
        audio,
        file,
        location,
        sticker
    }
}
