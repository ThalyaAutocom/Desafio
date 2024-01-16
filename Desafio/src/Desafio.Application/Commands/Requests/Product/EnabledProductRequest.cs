using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class EnabledProductRequest
{
    public Guid Id { get; set; }
    public bool Enable { get; set; } = true;
}
