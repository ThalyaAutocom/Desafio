﻿namespace Desafio.Domain;

public class ErrorResult
{
    public List<string> Messages { get; set; } = new();
    public string Exception { get; set; }
    public string ErrorId { get; set; }
    public int StatusCode { get; set; }
}
