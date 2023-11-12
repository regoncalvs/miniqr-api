namespace MiniQr.Application.Queries.ObterStatusCobrancas
{
    /// <summary>
    /// Representa a resposta da consulta de status de cobranças.
    /// </summary>
    public record ObterStatusCobrancasResponse(
        string Id,
        string Descricao,
        char Status
    );
}
