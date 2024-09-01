using SZ.PicPaySimplificado.Aplicacao.Conversores;
using SZ.PicPaySimplificado.Aplicacao.DTOs.Usuario;
using SZ.PicPaySimplificado.Aplicacao.Interfaces;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;


namespace SZ.PicPaySimplificado.Aplicacao.Servicos;

public class UsuarioAppService : IUsuarioAppService
{
	private readonly IUsuarioServico _usuarioServico;
	private readonly IUsuarioConversor _conversor;

	public UsuarioAppService(IUsuarioServico usuarioServico, IUsuarioConversor conversor)
	{
		_usuarioServico = usuarioServico;
		_conversor = conversor;
	}

	public async Task Adicionar(AdicionarUsuarioDto usuarioDto)
	{
		var usuario = _conversor.ConverterParaEntidade(usuarioDto);
		await _usuarioServico.Adicionar(usuario);
	}

	public async Task Atualizar(AdicionarUsuarioDto usuarioDto)
	{
		var usuario = _conversor.ConverterParaEntidade(usuarioDto);
		await _usuarioServico.Atualizar(usuario);
	}

	public async Task<ObterUsuarioDto> ObterPorId(Guid id)
	{
		var usuario = await _usuarioServico.ObterPorId(id);
		var usuarioDto = _conversor.ConverterParaDtoObter(usuario);

		return usuarioDto;
	}
}
