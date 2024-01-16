using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class CanBuyPersonRequest
{
    public Guid Id { get; set; }
    [DefaultValue(false)]
    public bool CanBuy { get; set; }
    
}
