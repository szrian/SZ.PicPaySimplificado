using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;

public interface IUsuarioServico
{
	Task Adicionar(Usuario usuario);
	Task Atualizar(Usuario usuario);
	Task<Usuario> ObterPorId(Guid id);
	Task<IEnumerable<Usuario>> ObterTodos();
}
