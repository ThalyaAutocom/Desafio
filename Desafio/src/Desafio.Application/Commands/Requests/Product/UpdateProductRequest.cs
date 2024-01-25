﻿using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UpdateProductRequest : IRequest<bool>
{
    private string _acronym = string.Empty;

    public string ShortId { get; set; } = string.Empty;
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
    public bool Enable { get; set; } = true;
    [DefaultValue(false)]
    public bool Sellable { get; set; }

}
