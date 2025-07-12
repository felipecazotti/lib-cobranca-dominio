namespace Cobranca.Lib.Dominio.Models;

public class ResponseModel<T> : ResponseModel
{
    public T? Data { get; set; }
}

public class ResponseModel
{
    public required string Codigo { get; set; }
    public string? Messagem { get; set; }
}
