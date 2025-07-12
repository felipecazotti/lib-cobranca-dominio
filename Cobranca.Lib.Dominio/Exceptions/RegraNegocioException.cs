using System.Net;
using Cobranca.Lib.Dominio.Models;

namespace Cobranca.Lib.Dominio.Exceptions;

public class RegraNegocioException(HttpStatusCode httpStatusCode, string codigoErro, string mensagemErro, Exception? originalException = null) 
    : Exception(mensagemErro, originalException)
{
    public HttpStatusCode StatusCode { get; set; } = httpStatusCode;
    public ResponseModel ResponseModel { get; set; } = new()
    {
        Codigo = codigoErro,
        Messagem = mensagemErro
    };

}
