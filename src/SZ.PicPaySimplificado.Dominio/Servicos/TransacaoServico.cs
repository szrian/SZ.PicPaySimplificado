using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Interfaces;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Servicos;

public class TransacaoServico : ITransacaoServico
{
    private readonly ITransacaoRepositorio _transacaoRepositorio;
	private readonly IUsuarioServico _usuarioServico;
	private readonly IAutorizacaoTransacaoServico _autorizacaoTransacaoServico;
	public TransacaoServico(ITransacaoRepositorio transacaoRepositorio,
		IUsuarioServico usuarioServico,
		IAutorizacaoTransacaoServico autorizacaoTransacaoServico)
	{
		_transacaoRepositorio = transacaoRepositorio;
		_usuarioServico = usuarioServico;
		_autorizacaoTransacaoServico = autorizacaoTransacaoServico;
	}
	public async Task Adicionar(Transacao transacao)
	{
		var pagador = await _usuarioServico.ObterPorId(transacao.PagadorId);
		var recebedor = await _usuarioServico.ObterPorId(transacao.RecebedorId);

		await ValidarTransacao(pagador, transacao.Valor);

		recebedor.Creditar(transacao.Valor);
		pagador.Debitar(transacao.Valor);

		await _usuarioServico.Atualizar(recebedor);
		await _usuarioServico.Atualizar(pagador);

		await _transacaoRepositorio.Adicionar(transacao);
	}

	private async Task ValidarTransacao(Usuario pagador, float valorTransacao)
	{
		if (!pagador.EhUsuarioComum())
			throw new Exception("Usuários lojistas não podem realizar transações.");

		if (pagador.Saldo < valorTransacao)
			throw new Exception("Você não possui saldo suficiente para realizar essa transação.");

		if (!await _autorizacaoTransacaoServico.AutorizarTransacao())
			throw new Exception("Transação não autorizada.");
	}
}
