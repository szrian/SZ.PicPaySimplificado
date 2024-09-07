using SZ.PicPaySimplificado.Aplicacao.DTOs.Usuario;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Aplicacao.Interfaces;

public interface IUsuarioAppService
{
	Task<AdicionarUsuarioDto> Adicionar(AdicionarUsuarioDto usuarioDto);
	Task<AdicionarUsuarioDto> Atualizar(AdicionarUsuarioDto usuarioDto);
	Task<ObterUsuarioDto> ObterPorId(Guid id);
	Task<IEnumerable<ObterUsuarioDto>> ObterTodos();
}
