using System.Net.Http.Headers;
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

	public async Task<Usuario> Adicionar(Usuario usuario)
	{
		usuario.Validar();

		if (!usuario.ValidationResult.IsValid)
			return usuario;

		await _usuarioRepositorio.Adicionar(usuario);
		return usuario;
	}

	public async Task<Usuario> Atualizar(Usuario usuario)
	{
		usuario.Validar();
		if (!usuario.ValidationResult.IsValid)
			return usuario;

		await _usuarioRepositorio.Atualizar(usuario);
		return usuario;
	}

	public async Task<Usuario> ObterPorId(Guid id) =>
		await _usuarioRepositorio.ObterPorId(id);

	public Task<IEnumerable<Usuario>> ObterTodos() =>
		_usuarioRepositorio.ObterTodos();
}
