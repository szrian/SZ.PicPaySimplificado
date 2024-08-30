using Microsoft.EntityFrameworkCore;
using SZ.PicPaySimplificado.Dados.Contexto;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dados.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
	private readonly PicPaySimplificadoContexto _contexto;

	public UsuarioRepositorio(PicPaySimplificadoContexto contexto)
	{
		_contexto = contexto;
		_contexto.Database.GetDbConnection();
	}

	public async Task Adicionar(Usuario usuario)
	{
		await _contexto.Set<Usuario>().AddAsync(usuario);
		await Commit();
	}

	public async Task Atualizar(Usuario usuario)
	{
		_contexto.Set<Usuario>().Update(usuario);
		await Commit();
	}

	public async Task<int> Commit() =>
		await _contexto.SaveChangesAsync();

	public async Task<Usuario> ObterPorId(Guid id) =>
		await _contexto.Set<Usuario>().FindAsync(id);
}
