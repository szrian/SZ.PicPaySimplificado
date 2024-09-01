using SZ.PicPaySimplificado.Aplicacao.DTOs.Usuario;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Aplicacao.Conversores;

public interface IUsuarioConversor
{
	Usuario ConverterParaEntidade(AdicionarUsuarioDto usuarioDto);
	AdicionarUsuarioDto ConverterParaDtoAdicionar(Usuario usuario);
	ObterUsuarioDto ConverterParaDtoObter(Usuario usuario);
}

public class UsuarioConversor : IUsuarioConversor
{
	public AdicionarUsuarioDto ConverterParaDtoAdicionar(Usuario usuario) =>
		new(usuario.Nome,
			usuario.Documento,
			usuario.Email,
			usuario.TipoUsuario,
			usuario.Senha,
			usuario.Saldo,
			usuario.ValidationResult);

	public ObterUsuarioDto ConverterParaDtoObter(Usuario usuario) =>
		new(usuario.Id,
			usuario.Nome,
			usuario.Documento,
			usuario.Email,
			usuario.TipoUsuario,
			usuario.Senha,
			usuario.Saldo);

	public Usuario ConverterParaEntidade(AdicionarUsuarioDto usuarioDto) =>
		new(usuarioDto.Nome,
			usuarioDto.Documento,
			usuarioDto.Email,
			usuarioDto.TipoUsuario,
			usuarioDto.Senha,
			usuarioDto.Saldo);
}
