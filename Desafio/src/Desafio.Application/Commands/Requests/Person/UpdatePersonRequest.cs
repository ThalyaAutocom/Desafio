﻿using Desafio.Domain;
using MediatR;
using System.ComponentModel;

namespace Desafio.Application;

public class UpdatePersonRequest : IRequest<bool>
{
    private string _document;

    public string ShortId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Document
    {
        get => _document;
        set => _document = value.GetOnlyDocumentNumber();
    }
    public string City { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string AlternativeCode { get; set; } = string.Empty;
    public bool Enable { get; set; } = true;
    [DefaultValue(false)]
    public bool CanBuy { get; set; }
}
