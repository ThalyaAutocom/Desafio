using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class SellableProductRequest
{
    public Guid Id { get; set; }
    public bool Sellable { get; set; } 
    
}
