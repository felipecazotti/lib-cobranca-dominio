using Cobranca.Lib.Dominio.Models;

namespace Cobranca.Lib.Dominio.Processadores;

public static class GeradorPayloadQrCode
{
    public static string GerarPayloadPixEstatico(string chavePix,
        string nomeRecebedor,
        string cidade,
        decimal? valor = null,
        string identificadorCobranca = "***",
        string? descricao = null)
    {
        // Formatação dos campos conforme padrão Pix
        string payloadFormatIndicator = "000201";
        string merchantAccountInfo = $"26{(14 + chavePix.Length):00}0014BR.GOV.BCB.PIX01{chavePix.Length:00}{chavePix}";
        string merchantCategoryCode = "52040000";
        string transactionCurrency = "5303986";
        string transactionAmount = valor.HasValue ? $"54{valor.Value.ToString("0.00").Replace(",", ".").Length:00}{valor.Value.ToString("0.00").Replace(",", ".")}" : "";
        string countryCode = "5802BR";
        string merchantName = $"59{nomeRecebedor.Length:00}{nomeRecebedor}";
        string merchantCity = $"60{cidade.Length:00}{cidade}";

        // Adicional: campo 62 (txid e descrição)
        string txidField = $"05{identificadorCobranca.Length:00}{identificadorCobranca}";
        string additionalDataField = $"62{(txidField.Length + (descricao != null ? 4 + descricao.Length : 0)):00}{txidField}";
        if (!string.IsNullOrEmpty(descricao))
        {
            string descricaoField = $"50{descricao.Length:00}{descricao}";
            additionalDataField += descricaoField;
        }

        // Monta o payload sem o CRC
        string payloadSemCRC = $"{payloadFormatIndicator}{merchantAccountInfo}{merchantCategoryCode}{transactionCurrency}{transactionAmount}{countryCode}{merchantName}{merchantCity}{additionalDataField}6304";

        // Calcula o CRC16 do payload
        string crc16 = CalcularCRC16(payloadSemCRC).ToUpper();

        return payloadSemCRC + crc16;
    }

    // Função para calcular o CRC16-CCITT (0x1021)
    private static string CalcularCRC16(string input)
    {
        ushort polinomio = 0x1021;
        ushort resultado = 0xFFFF;

        foreach (char c in input)
        {
            resultado ^= (ushort)(c << 8);
            for (int i = 0; i < 8; i++)
            {
                if ((resultado & 0x8000) != 0)
                    resultado = (ushort)((resultado << 1) ^ polinomio);
                else
                    resultado <<= 1;
            }
        }
        return resultado.ToString("X4");
    }
}
