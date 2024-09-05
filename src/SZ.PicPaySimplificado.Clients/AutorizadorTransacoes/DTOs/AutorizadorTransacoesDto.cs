using Newtonsoft.Json;

namespace SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.DTOs;

public class AutorizadorTransacoesDto
{
	[JsonProperty("status")]
	public string Status { get; set; }
	[JsonProperty("data")]
	public Dados Dados { get; set; }
}

public class Dados
{
	[JsonProperty("authorization")]
	public bool Autorizacao { get; set; }
}
