namespace Corvus.LineBot.Backend.Models;

public class PostMessageReqVM
{
    public string To { get; set; } = null!;

    public string Message { get; set; } = null!;
}
