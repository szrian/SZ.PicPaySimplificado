using SZ.PicPaySimplificado.Aplicacao.DTOs.Usuario;

namespace SZ.PicPaySimplificado.Aplicacao.Interfaces;

public interface IUsuarioAppService
{
	Task Adicionar(AdicionarUsuarioDto usuarioDto);
	Task Atualizar(AdicionarUsuarioDto usuarioDto);
	Task<ObterUsuarioDto> ObterPorId(Guid id);
}
