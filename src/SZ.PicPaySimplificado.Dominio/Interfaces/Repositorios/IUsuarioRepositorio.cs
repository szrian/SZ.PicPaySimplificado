using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;

public interface IUsuarioRepositorio
{
	Task Adicionar(Usuario usuario);
	Task Atualizar(Usuario usuario);
	Task<Usuario> ObterPorId(Guid id);
	Task<int> Commit();
}
