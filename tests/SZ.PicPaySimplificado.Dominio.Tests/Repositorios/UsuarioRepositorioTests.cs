using Bogus;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SZ.PicPaySimplificado.Dados.Contexto;
using SZ.PicPaySimplificado.Dados.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Modelos;
using SZ.PicPaySimplificado.Dominio.Tests.Fakers;

namespace SZ.PicPaySimplificado.Dominio.Tests.Repositorios;

public class UsuarioRepositorioTests
{
    private readonly IUsuarioRepositorio _repositorio;
    private readonly PicPaySimplificadoContexto _context;
    private readonly Faker _faker;
    public UsuarioRepositorioTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<PicPaySimplificadoContexto>()
        .UseSqlite(connection)
        .Options;

        _context = new PicPaySimplificadoContexto(options);
        _context.Database.EnsureCreated();

        _repositorio = new UsuarioRepositorio(_context);
        _faker = new Faker("pt_BR");
    }

    [Fact]
    public async Task Adicionar_DadaEntidadeUsuario_DeveAdicionarNoBancoDeDados()
    {
        var usuarioValido = UsuarioFaker.GerarUsuario();

        await _repositorio.Adicionar(usuarioValido);

        var usuariosDoBanco = await _context.Set<Usuario>().AsNoTracking().ToListAsync();

        usuariosDoBanco.Should().NotBeEmpty();
        usuariosDoBanco[0].Should().BeEquivalentTo(usuarioValido);
    }

    [Fact]
    public async Task Atualizar_DadaEntidadeUsuario_DeveAtualizarNoBancoDeDados()
    {
        var usuarioValido = UsuarioFaker.GerarUsuario();
        await _repositorio.Adicionar(usuarioValido);
        usuarioValido.Creditar(_faker.Random.Float(1, 100));

        await _repositorio.Atualizar(usuarioValido);

        var usuarioDoBanco = await _context.Set<Usuario>().FindAsync(usuarioValido.Id);

        usuarioDoBanco.Should().NotBeNull();
        usuarioDoBanco.Should().BeEquivalentTo(usuarioValido);
    }

    [Fact]
    public async Task ObterPorId_DadaEntidadeUsuarioAdicionada_DeveObterPorIdNoBancoDeDados()
    {
        var usuarioValido = UsuarioFaker.GerarUsuario();
        await _repositorio.Adicionar(usuarioValido);

        var usuarioDoBanco = await _repositorio.ObterPorId(usuarioValido.Id);

        usuarioDoBanco.Should().NotBeNull();
        usuarioDoBanco.Should().BeEquivalentTo(usuarioValido);
    }

    [Fact]
    public async Task ObterTodos_DadaEntidadesUsuarioAdicionadas_DeveObterTodosDoBancoDeDados()
    {
        var usuarioComumValido = UsuarioFaker.GerarUsuarioComum();
        var usuarioLojistaValido = UsuarioFaker.GerarUsuarioLojista();
        await _repositorio.Adicionar(usuarioComumValido);
        await _repositorio.Adicionar(usuarioLojistaValido);

        var usuariosDoBanco = await _repositorio.ObterTodos();

        usuariosDoBanco.Should().NotBeEmpty();
        usuariosDoBanco.Should().HaveCount(2);
    }
}
