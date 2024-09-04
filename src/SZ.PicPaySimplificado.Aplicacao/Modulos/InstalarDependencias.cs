using Microsoft.Extensions.DependencyInjection;
using SZ.PicPaySimplificado.Aplicacao.Conversores;
using SZ.PicPaySimplificado.Aplicacao.Interfaces;
using SZ.PicPaySimplificado.Aplicacao.Servicos;

namespace SZ.PicPaySimplificado.Aplicacao.Modulos;

public static class InstalarDependencias
{
	public static IServiceCollection RegistrarServicosAppService(this IServiceCollection servicos)
	{
		servicos.AddScoped<IUsuarioAppService, UsuarioAppService>();
		servicos.AddScoped<ITransacaoAppService, TransacaoAppService>();

		servicos.AddScoped<IUsuarioConversor, UsuarioConversor>();
		servicos.AddScoped<ITransacaoConversor, TransacaoConversor>();

		return servicos;
	}
}
