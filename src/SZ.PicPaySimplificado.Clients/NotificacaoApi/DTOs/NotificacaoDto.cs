using Newtonsoft.Json;

namespace SZ.PicPaySimplificado.Clients.NotificacaoApi.DTOs;

public class NotificacaoDto
{
	[JsonProperty("status")]
	public string Status {  get; set; }

	[JsonProperty("data")]
	public Dados Dados { get; set; }
}

public class Dados
{
	[JsonProperty("message")]
	public string Mensagem { get; set; }
}
