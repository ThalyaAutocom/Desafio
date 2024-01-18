using Desafio.Domain;

namespace Desafio.Application;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string ShortId { get; set; } = string.Empty;
    public bool Enable { get; set; }
    public EUserLevel UserLevel { get; set; }
}
