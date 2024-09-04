using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Servicos;

public class UsuarioServico : IUsuarioServico
{
	private readonly IUsuarioRepositorio _usuarioRepositorio;

	public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
	{
		_usuarioRepositorio = usuarioRepositorio;
	}

	public async Task Adicionar(Usuario usuario)
	{
		//Validar regras de entidade

		await _usuarioRepositorio.Adicionar(usuario);
	}

	public async Task Atualizar(Usuario usuario)
	{
		//Validar regras de entidade

		await _usuarioRepositorio.Atualizar(usuario);
	}

	public async Task<Usuario> ObterPorId(Guid id) =>
		await _usuarioRepositorio.ObterPorId(id);

	public Task<IEnumerable<Usuario>> ObterTodos() =>
		_usuarioRepositorio.ObterTodos();
}
