using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SZ.PicPaySimplificado.Dados.Contexto;
using SZ.PicPaySimplificado.Dados.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;

namespace SZ.PicPaySimplificado.Dados.Modulos
{
	public static class InstalarDependencias
	{
		public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection servicos, IConfiguration configuracao) =>
			servicos.AddDbContext<PicPaySimplificadoContexto>(opcoes => opcoes.UseSqlServer(configuracao.GetConnectionString("DefaultConnection")));

		public static IServiceCollection RegistrarRepositorios(this IServiceCollection servicos)
		{
			servicos.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
			servicos.AddScoped<ITransacaoRepositorio, TransacaoRepositorio>();

			return servicos;
		}
	}
}
