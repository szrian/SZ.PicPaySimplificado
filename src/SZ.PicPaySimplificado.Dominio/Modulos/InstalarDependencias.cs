using Microsoft.Extensions.DependencyInjection;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;
using SZ.PicPaySimplificado.Dominio.Servicos;

namespace SZ.PicPaySimplificado.Dominio.Modulos;

public static class InstalarDependencias
{
	public static IServiceCollection RegistrarServicosDeDominio(this IServiceCollection servicos)
	{
		servicos.AddScoped<IUsuarioServico, UsuarioServico>();

		return servicos;
	}
}
