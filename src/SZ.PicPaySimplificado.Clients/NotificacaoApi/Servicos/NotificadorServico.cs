using Newtonsoft.Json;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.DTOs;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.Interfaces;

namespace SZ.PicPaySimplificado.Clients.NotificacaoApi.Servicos;

public class NotificadorServico : INotificadorServico
{
    private readonly IHttpClientFactory _httpClientFactory;
	public NotificadorServico(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}
	public async Task<bool> NotificarTransacao()
	{
		var httpClient = _httpClientFactory.CreateClient("Notificador");

		var response = await httpClient.GetAsync($"api/v1/notify");

		if (!response.IsSuccessStatusCode)
			return false;

		var notificacaoDto = JsonConvert.DeserializeObject<NotificacaoDto>(await response.Content.ReadAsStringAsync());

		if (notificacaoDto.Status == "fail")
			return false;

		return true;
	}
}
