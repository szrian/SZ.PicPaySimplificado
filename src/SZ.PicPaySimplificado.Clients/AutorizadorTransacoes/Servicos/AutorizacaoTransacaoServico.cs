using Newtonsoft.Json;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.DTOs;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Interfaces;

namespace SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Servicos;

public class AutorizacaoTransacaoServico : IAutorizacaoTransacaoServico
{
	private readonly IHttpClientFactory _httpClientFactory;
	public AutorizacaoTransacaoServico(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<bool> AutorizarTransacao()
	{
		var httpClient = _httpClientFactory.CreateClient("AutorizadorTransacoes");

		var response = await httpClient.GetAsync($"api/v2/authorize");

		if (!response.IsSuccessStatusCode)
			return false;

		var autorizadorTransacoesDto = JsonConvert.DeserializeObject<AutorizadorTransacoesDto>(await response.Content.ReadAsStringAsync());

		if (autorizadorTransacoesDto.Status != "success" && !autorizadorTransacoesDto.Dados.Autorizacao)
			return false;

		return true;
	}
}
