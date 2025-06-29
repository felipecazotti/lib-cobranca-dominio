using MongoDB.Bson.Serialization.Attributes;

namespace Cobranca.Lib.Dominio.Models;

public class CobrancaRecorrente : CobrancaBase
{
    [BsonElement("diaMesCobranca")]
    public int DiaMesCobranca { get; set; }
}
