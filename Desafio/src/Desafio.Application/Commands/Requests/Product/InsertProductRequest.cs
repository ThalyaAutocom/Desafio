using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class InsertProductRequest
{
    private string _acronym;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Acronym
    {
        get => _acronym;
        set => _acronym = value?.ToUpper();
    }
    public decimal Price { get; set; }
    public decimal StoredQuantity { get; set; }
    public bool Enable { get; set; } = true;
    [DefaultValue(false)]
    public bool Sellable { get; set; } 
    public string BarCode { get; set; } = string.Empty;
}
