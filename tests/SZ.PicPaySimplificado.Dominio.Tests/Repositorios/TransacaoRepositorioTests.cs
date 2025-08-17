using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SZ.PicPaySimplificado.Dados.Contexto;
using SZ.PicPaySimplificado.Dados.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Modelos;
using SZ.PicPaySimplificado.Dominio.Tests.Fakers;

namespace SZ.PicPaySimplificado.Dominio.Tests.Repositorios;

public class TransacaoRepositorioTests
{
    private readonly ITransacaoRepositorio _transacaoRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly PicPaySimplificadoContexto _context;

    public TransacaoRepositorioTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<PicPaySimplificadoContexto>()
        .UseSqlite(connection)
        .Options;

        _context = new PicPaySimplificadoContexto(options);
        _context.Database.EnsureCreated();

        _transacaoRepositorio = new TransacaoRepositorio(_context);
        _usuarioRepositorio = new UsuarioRepositorio(_context);
    }

    [Fact]
    public async Task Adicionar_DadaEntidadeTransacao_DeveAdicionarNoBancoDeDados()
    {
        var usuarioPagador = UsuarioFaker.GerarUsuarioComum();
        var usuarioRecebedor = UsuarioFaker.GerarUsuarioLojista();

        await _usuarioRepositorio.Adicionar(usuarioPagador);
        await _usuarioRepositorio.Adicionar(usuarioRecebedor);

        var transacaoValida = TransacaoFaker.GerarTransacaoValidaComUsuarios(usuarioPagador.Id, usuarioRecebedor.Id);

        await _transacaoRepositorio.Adicionar(transacaoValida);

        var transacaoDoBanco = _context.Set<Transacao>().FirstOrDefault();

        transacaoDoBanco.Should().NotBeNull();
        transacaoDoBanco.Should().BeEquivalentTo(transacaoValida);
    }

    [Fact]
    public async Task ObterTransacoesPorUsuarioId_DadaTransacaoAdicionada_DeveObterPorUsuarioIdNoBancoDeDados()
    {
        var usuarioPagador = UsuarioFaker.GerarUsuarioComum();
        var usuarioRecebedor = UsuarioFaker.GerarUsuarioLojista();

        await _usuarioRepositorio.Adicionar(usuarioPagador);
        await _usuarioRepositorio.Adicionar(usuarioRecebedor);

        var transacaoValida = TransacaoFaker.GerarTransacaoValidaComUsuarios(usuarioPagador.Id, usuarioRecebedor.Id);
        await _transacaoRepositorio.Adicionar(transacaoValida);

        var transacaoDoBanco = await _transacaoRepositorio.ObterTransacoesPorUsuarioId(usuarioPagador.Id);

        transacaoDoBanco.Should().NotBeEmpty();
        transacaoDoBanco.First().Id.Should().NotBeEmpty();
        transacaoDoBanco.First().Valor.Should().Be(transacaoValida.Valor);
        transacaoDoBanco.First().DataTransacao.Should().Be(transacaoValida.DataTransacao);
        transacaoDoBanco.First().PagadorId.Should().Be(transacaoValida.PagadorId);
        transacaoDoBanco.First().RecebedorId.Should().Be(transacaoValida.RecebedorId);
    }
}
