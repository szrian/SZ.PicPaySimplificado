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

	public async Task<AdicionarUsuarioDto> Adicionar(AdicionarUsuarioDto usuarioDto)
	{
		var usuario = _conversor.ConverterParaEntidade(usuarioDto);
		return _conversor.ConverterParaDtoAdicionar(await _usuarioServico.Adicionar(usuario));
	}

	public async Task<AdicionarUsuarioDto> Atualizar(AdicionarUsuarioDto usuarioDto)
	{
		var usuario = _conversor.ConverterParaEntidade(usuarioDto);
		return _conversor.ConverterParaDtoAdicionar(await _usuarioServico.Atualizar(usuario));
	}

	public async Task<ObterUsuarioDto> ObterPorId(Guid id)
	{
		var usuario = await _usuarioServico.ObterPorId(id);
		var usuarioDto = _conversor.ConverterParaDtoObter(usuario);

		return usuarioDto;
	}

	public async Task<IEnumerable<ObterUsuarioDto>> ObterTodos()
	{
		var usuarios = await _usuarioServico.ObterTodos();
		var usuariosDto = new List<ObterUsuarioDto>();

		foreach (var usuario in usuarios)
			usuariosDto.Add(_conversor.ConverterParaDtoObter(usuario));

		return usuariosDto;
	}
}
