namespace Corvus.LineBot.Backend.Models;

public class GptResVM
{
    public string id { get; set; } = null!;
    public string @object { get; set; } = null!;
    public int created { get; set; }
    public string model { get; set; } = null!;
    public List<Choice> choices { get; set; } = new();

    public Usage usage { get; set; } = null!;
}

public class Choice
{
    public int index { get; set; }

    public GptMessage message { get; set; } = null!;

    public string finish_reason { get; set; } = null!;
}

public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}