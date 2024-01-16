using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class EnabledPersonRequest
{
    public Guid Id { get; set; }
    public bool Enable { get; set; } 
    
}
