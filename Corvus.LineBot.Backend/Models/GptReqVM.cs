namespace Corvus.LineBot.Backend.Models;

public class GptReqVM
{
    public string model { get; set; } = "gpt-3.5-turbo";
    public List<GptMessage> messages { get; set; } = null!;
    public double temperature { get; set; }
    public int max_tokens { get; set; }
    public int top_p { get; set; }
    public int frequency_penalty { get; set; }
    public int presence_penalty { get; set; }
    public string? stop { get; set; }
}
