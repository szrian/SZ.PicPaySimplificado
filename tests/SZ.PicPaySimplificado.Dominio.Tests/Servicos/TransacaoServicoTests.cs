using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using SZ.PicPaySimplificado.Clients.AutorizadorTransacoes.Interfaces;
using SZ.PicPaySimplificado.Clients.NotificacaoApi.Interfaces;
using SZ.PicPaySimplificado.Dados.Contexto;
using SZ.PicPaySimplificado.Dados.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;
using SZ.PicPaySimplificado.Dominio.Servicos;
using SZ.PicPaySimplificado.Dominio.Tests.Fakers;

namespace SZ.PicPaySimplificado.Dominio.Tests.Servicos;

public class TransacaoServicoTests
{
    private readonly ITransacaoServico _transacaoServico;
    private readonly IUsuarioServico _mockUsuarioServico;
    private readonly IAutorizacaoTransacaoServico _mockAutorizacaoTransacaoServico;
    private readonly ITransacaoRepositorio _mockTransacaoRepositorio;
    private readonly INotificadorServico _mockNotificadorServico;
    private readonly UsuarioRepositorio _usuarioRepositorio;
    private readonly UsuarioServico _usuarioServico;
    private readonly PicPaySimplificadoContexto _context;
    public TransacaoServicoTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<PicPaySimplificadoContexto>()
        .UseSqlite(connection)
        .Options;
        _context = new PicPaySimplificadoContexto(options);
        _context.Database.EnsureCreated();

        _usuarioRepositorio = new UsuarioRepositorio(_context);
        _usuarioServico = new UsuarioServico(_usuarioRepositorio);
        _mockAutorizacaoTransacaoServico = Substitute.For<IAutorizacaoTransacaoServico>();
        _mockUsuarioServico = Substitute.For<IUsuarioServico>();
        _mockTransacaoRepositorio = Substitute.For<ITransacaoRepositorio>();
        _mockNotificadorServico = Substitute.For<INotificadorServico>();

        _transacaoServico = new TransacaoServico(_mockTransacaoRepositorio, _mockUsuarioServico, _mockAutorizacaoTransacaoServico, _mockNotificadorServico);
    }

    [Fact]
    public async Task Adicionar_DadaTransacaoInvalida_DeveRetornarValidationResultFalseAsync()
    {
        //Arrange
        var transacao = TransacaoFaker.GerarTransacaoInvalida();

        //Act
        var transacaoResult = await _transacaoServico.Adicionar(transacao);

        //Assert
        transacaoResult.ValidationResult.IsValid.Should().BeFalse();
        transacao.ValidationResult.Errors.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task Adicionar_DadaTransacaoDeUsuarioLojista_DeveRetornarTransacaoComErroAsync()
    {
        var transacao = TransacaoFaker.GerarTransacaoValida();
        _mockUsuarioServico.ObterPorId(Arg.Any<Guid>())
            .Returns(Task.FromResult(UsuarioFaker.GerarUsuarioLojista()));

        var transacaoResult = await _transacaoServico.Adicionar(transacao);

        transacaoResult.ValidationResult.Errors.Should().HaveCountGreaterThan(0);
        transacaoResult.ValidationResult.Errors.Should()
            .ContainSingle(p => p.ErrorMessage == "Usuários lojistas não podem realizar transações.");
    }

    [Fact]
    public async Task Adicionar_DadaTransacaoDeUsuarioComSaldoInsuficiente_DeveRetornarTransacaoComErroAsync()
    {
        var transacao = TransacaoFaker.GerarTransacaoValida();
        _mockUsuarioServico.ObterPorId(Arg.Any<Guid>())
            .Returns(Task.FromResult(UsuarioFaker.GerarUsuarioComumSemSaldo()));

        var transacaoResult = await _transacaoServico.Adicionar(transacao);

        transacaoResult.ValidationResult.Errors.Should().HaveCountGreaterThan(0);
        transacaoResult.ValidationResult.Errors.Should()
            .ContainSingle(p => p.ErrorMessage == "Você não possui saldo suficiente para realizar essa transação.");
    }

    [Fact]
    public async Task Adicionar_DadaTransacaoNaoAutorizada_DeveRetornarTransacaoComErroAsync()
    {
        var transacao = TransacaoFaker.GerarTransacaoValida();

        _mockUsuarioServico.ObterPorId(Arg.Any<Guid>())
            .Returns(Task.FromResult(UsuarioFaker.GerarUsuarioComum()));

        _mockAutorizacaoTransacaoServico.AutorizarTransacao()
            .Returns(Task.FromResult(false));

        var transacaoResult = await _transacaoServico.Adicionar(transacao);

        transacaoResult.ValidationResult.Errors.Should().HaveCountGreaterThan(0);
        transacaoResult.ValidationResult.Errors.Should()
            .ContainSingle(p => p.ErrorMessage == "Transação não autorizada.");
    }

    [Fact]
    public async Task Adicionar_DadaTransacaoValida_DeveRegistrarTransacaoDebitarECreditarDosUsuarios()
    {
        //Arrange
        var usuarioPagador = UsuarioFaker.GerarUsuarioComum();
        var usuarioRecebedor = UsuarioFaker.GerarUsuarioLojista();
        var transacao = TransacaoFaker.GerarTransacaoValidaComUsuarios(usuarioPagador.Id, usuarioRecebedor.Id);

        await _usuarioRepositorio.Adicionar(usuarioPagador);
        await _usuarioRepositorio.Adicionar(usuarioRecebedor);

        var saldoPagadorEsperado = usuarioPagador.Saldo - transacao.Valor;
        var saldoRecebedorEsperado = usuarioRecebedor.Saldo + transacao.Valor;

        _mockAutorizacaoTransacaoServico.AutorizarTransacao()
            .Returns(Task.FromResult(true));

        _mockNotificadorServico.NotificarTransacao()
            .Returns(Task.FromResult(true));

        var transacaoServico = new TransacaoServico(_mockTransacaoRepositorio, _usuarioServico, _mockAutorizacaoTransacaoServico, _mockNotificadorServico);

        //Act
        var transacaoResult = await transacaoServico.Adicionar(transacao);

        //Arrange
        var usuarioPagadorRetorno = await _usuarioServico.ObterPorId(transacao.PagadorId);
        var usuarioRecebedorRetorno = await _usuarioServico.ObterPorId(transacao.RecebedorId);

        transacaoResult.Should().NotBeNull();
        usuarioPagadorRetorno.Saldo.Should().Be(saldoPagadorEsperado);
        usuarioRecebedorRetorno.Saldo.Should().Be(saldoRecebedorEsperado);
    }
}
