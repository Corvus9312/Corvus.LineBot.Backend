using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Corvus.LineBot.Backend.Enums;

public class GptEmun
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        assistant, 
        user, 
        system
    }
}
