using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Servicos;

public class TransacaoServico : ITransacaoServico
{
    private readonly ITransacaoRepositorio _transacaoRepositorio;
	private readonly IUsuarioServico _usuarioServico;
	public TransacaoServico(ITransacaoRepositorio transacaoRepositorio)
	{
		_transacaoRepositorio = transacaoRepositorio;
	}
	public async Task Adicionar(Transacao transacao)
	{
		var pagador = await _usuarioServico.ObterPorId(transacao.PagadorId);
		var recebedor = await _usuarioServico.ObterPorId(transacao.RecebedorId);

		ValidarTransacao(pagador, transacao.Valor);

		recebedor.Creditar(transacao.Valor);
		pagador.Debitar(transacao.Valor);

		await _usuarioServico.Atualizar(recebedor);
		await _usuarioServico.Atualizar(pagador);

		await _transacaoRepositorio.Adicionar(transacao);
	}

	private void ValidarTransacao(Usuario pagador, float valorTransacao)
	{
		if (!pagador.EhUsuarioComum())
			throw new Exception("Usuários lojistas não podem realizar transações.");

		if (pagador.Saldo < valorTransacao)
			throw new Exception("Você não possui saldo suficiente para realizar essa transação.");
	}
}
