using FluentValidation.Results;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Configuracao;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Interfaces;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.Interfaces;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Servicos;

public class TransacaoServico : ITransacaoServico
{
    private readonly ITransacaoRepositorio _transacaoRepositorio;
	private readonly IUsuarioServico _usuarioServico;
	private readonly IAutorizacaoTransacaoServico _autorizacaoTransacaoServico;
	private readonly INotificadorServico _notificadorServico;
	public TransacaoServico(ITransacaoRepositorio transacaoRepositorio,
		IUsuarioServico usuarioServico,
		IAutorizacaoTransacaoServico autorizacaoTransacaoServico,
		INotificadorServico notificadorServico)
	{
		_transacaoRepositorio = transacaoRepositorio;
		_usuarioServico = usuarioServico;
		_autorizacaoTransacaoServico = autorizacaoTransacaoServico;
		_notificadorServico = notificadorServico;
	}
	public async Task<Transacao> Adicionar(Transacao transacao)
	{
		transacao.Validar();
		if (!transacao.ValidationResult.IsValid)
			return transacao;

		var pagador = await _usuarioServico.ObterPorId(transacao.PagadorId);
		var recebedor = await _usuarioServico.ObterPorId(transacao.RecebedorId);

		var erro = await ValidarTransacao(pagador, transacao.Valor);
		if (erro != null)
		{
			transacao.ValidationResult.Errors.Add(erro);
			return transacao;
		}
			
		await RealizarTransacao(pagador, recebedor, transacao.Valor);
		await _transacaoRepositorio.Adicionar(transacao);
		var resultado = await _notificadorServico.NotificarTransacao();
		if (!resultado)
			transacao.ValidationResult.Errors.Add(new ValidationFailure(string.Empty,
												  "Não foi possível enviar a notificação da transação."));

		return transacao;
	}

	private async Task<ValidationFailure> ValidarTransacao(Usuario pagador, float valorTransacao)
	{
		if (!pagador.EhUsuarioComum())
			return new ValidationFailure(string.Empty, "Usuários lojistas não podem realizar transações.");

		if (pagador.Saldo < valorTransacao)
			return new ValidationFailure(string.Empty, "Você não possui saldo suficiente para realizar essa transação.");

		if (!await _autorizacaoTransacaoServico.AutorizarTransacao())
			return new ValidationFailure(string.Empty, "Transação não autorizada.");

		return null;
	}

	private async Task RealizarTransacao(Usuario pagador, Usuario recebedor, float valor)
	{
		pagador.Debitar(valor);
		recebedor.Creditar(valor);
		await _usuarioServico.Atualizar(recebedor);
		await _usuarioServico.Atualizar(pagador);
	}
}
