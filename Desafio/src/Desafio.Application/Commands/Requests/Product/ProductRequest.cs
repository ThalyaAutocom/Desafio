using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class ProductRequest
{
    private string _acronym = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Acronym
    {
        get => _acronym;
        set => _acronym = value?.ToUpper();
    }
    public decimal Price { get; set; }
    public decimal StoredQuantity { get; set; }
    public string BarCode { get; set; } = string.Empty;
}
