using SZ.PicPaySimplificado.Aplicacao.Modulos;
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

			return services;
		}
	}
}
