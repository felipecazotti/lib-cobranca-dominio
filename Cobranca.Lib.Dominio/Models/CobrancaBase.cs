using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cobranca.Lib.Dominio.Models;

public abstract class CobrancaBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("nomeCobranca")]
    public required string NomeCobranca { get; set; }

    [BsonElement("descricaoCobranca")]
    public string? DescricaoCobranca { get; set; }

    [BsonElement("valorCobranca")]
    public required decimal ValorCobranca { get; set; }

    [BsonElement("nomeDevedor")]
    public required string NomeDevedor { get; set; }

    [BsonElement("emailDevedor")]
    public required string EmailDevedor { get; set; }

    [BsonElement("nomeRecebedor")]
    public required string NomeRecebedor { get; set; }

    [BsonElement("chavePix")]
    public required string ChavePix { get; set; }

    [BsonElement("qrCode")]
    public required string QrCode { get; set; }

    [BsonElement("dataHoraRegistroCobranca")]
    public required DateTime DataHoraRegistroCobranca { get; set; }
}
