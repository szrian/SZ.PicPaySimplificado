using SZ.PicPaySimplificado.Aplicacao.Modulos;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Modulos;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.Modulos;
using SZ.PicPaySimplificado.Dados.Modulos;
using SZ.PicPaySimplificado.Dominio.Modulos;

namespace SZ.PicPaySimplificado.Site.Configuracao
{
	public static class InjecaoDeDependenciaConfig
	{
		public static IServiceCollection ResolverDependencias(this IServiceCollection services, IConfiguration configuration)
		{
			services.AdicionarBancoDeDados(configuration);
			services.RegistrarRepositorios();
			services.RegistrarServicosDeDominio();
			services.RegistrarServicosAppService();
			services.RegistrarAutorizacaoTransacoesServico(configuration);
			services.RegistrarNotificadorServico(configuration);

			return services;
		}
	}
}
