using System.Text.Json.Serialization;

namespace Desafio.Application;

public class LoginUserResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Token { get; set; }
    public double Expiration { get; set; }
}
