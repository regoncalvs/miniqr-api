namespace MiniQr.Application.Queries.ObterCobrancasPagas
{
    /// <summary>
    /// Representa a resposta da consulta cobranças pagas.
    /// </summary>
    public record ObterCobrancasResponse(
        string Id,
        string Descricao,
        decimal Valor,
        char Status
    );
}
