using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.Configuracao;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.Interfaces;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.Servicos;

namespace SZ.PicPaySimplificado.Clients.NotificacaoApi.Modulos;

public static class InstalarDependencias
{
	public static IServiceCollection RegistrarNotificadorServico(this IServiceCollection servicos, IConfiguration configuration)
	{
		var notificadorApiConfig = new NotificadorApiConfig();
		configuration.Bind("NotificadorApiConfig", notificadorApiConfig);

		servicos.AddSingleton(notificadorApiConfig);
		servicos.AddScoped<INotificadorServico, NotificadorServico>();

		servicos.AddHttpClient("Notificador", httpClient =>
		{
			httpClient.BaseAddress = new Uri(notificadorApiConfig.Uri);
		});

		return servicos;
	}
}
