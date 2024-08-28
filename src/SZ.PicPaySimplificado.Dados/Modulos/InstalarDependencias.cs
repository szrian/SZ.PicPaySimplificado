using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SZ.PicPaySimplificado.Dados.Contexto;

namespace SZ.PicPaySimplificado.Dados.Modulos
{
	public static class InstalarDependencias
	{
		public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection servicos, IConfiguration configuracao) =>
			servicos.AddDbContext<PicPaySimplificadoContexto>(opcoes => opcoes.UseSqlServer(configuracao.GetConnectionString("DefaultConnection")));
	}
}
