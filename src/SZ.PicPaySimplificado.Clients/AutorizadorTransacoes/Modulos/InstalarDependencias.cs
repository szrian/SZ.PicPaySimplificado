using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Configuracao;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Interfaces;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Servicos;

namespace SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Modulos;

public static class InstalarDependencias
{
	public static IServiceCollection RegistrarAutorizacaoTransacoesServico(this IServiceCollection servicos, IConfiguration configuration)
	{
		var autorizadorTransacoesConfig = new AutorizadorTransacoesConfig();
		configuration.Bind("AutorizadorTransacoesConfig", autorizadorTransacoesConfig);

		servicos.AddSingleton(autorizadorTransacoesConfig);
		servicos.AddScoped<IAutorizacaoTransacaoServico, AutorizacaoTransacaoServico>();

		servicos.AddHttpClient("AutorizadorTransacoes", httpClient =>
		{
			httpClient.BaseAddress = new Uri(autorizadorTransacoesConfig.Uri);
		});

		return servicos;
	}
}
