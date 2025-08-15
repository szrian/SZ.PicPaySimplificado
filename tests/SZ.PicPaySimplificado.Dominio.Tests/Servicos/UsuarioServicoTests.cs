using Bogus;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SZ.PicPaySimplificado.Dados.Contexto;
using SZ.PicPaySimplificado.Dados.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Repositorios;
using SZ.PicPaySimplificado.Dominio.Interfaces.Servicos;
using SZ.PicPaySimplificado.Dominio.Servicos;
using SZ.PicPaySimplificado.Dominio.Tests.Fakers;

namespace SZ.PicPaySimplificado.Dominio.Tests.Servicos;

public class UsuarioServicoTests
{
    private readonly IUsuarioServico _usuarioServico;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly PicPaySimplificadoContexto _context;
    private readonly Faker _faker;
    public UsuarioServicoTests()
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
        _faker = new Faker("pt_BR");
    }

    [Fact]
    public async Task Adicionar_DadoUsuarioInvalido_DeveRetornarValidationResultComErros()
    {
        var usuarioInvalido = UsuarioFaker.GerarUsuarioInvalido();

        var usuarioRetorno = await _usuarioServico.Adicionar(usuarioInvalido);

        usuarioRetorno.ValidationResult.IsValid.Should().BeFalse();
        usuarioRetorno.ValidationResult.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Adicionar_DadoUsuarioValido_DeveAdicionarUsuarioNoBanco()
    {
        var usuarioValido = UsuarioFaker.GerarUsuario();

        var usuarioRetorno = await _usuarioServico.Adicionar(usuarioValido);

        var usuarioDoBanco = await _usuarioRepositorio.ObterPorId(usuarioRetorno.Id);

        usuarioRetorno.ValidationResult.IsValid.Should().BeTrue();
        usuarioRetorno.ValidationResult.Errors.Should().BeEmpty();
        usuarioDoBanco.Should().BeEquivalentTo(usuarioRetorno);
    }

    [Fact]
    public async Task Atualizar_DadoUsuarioInvalido_DeveRetornarValidationResultComErros()
    {
        var usuarioInvalido = UsuarioFaker.GerarUsuarioInvalido();

        var usuarioRetorno = await _usuarioServico.Atualizar(usuarioInvalido);

        usuarioRetorno.ValidationResult.IsValid.Should().BeFalse();
        usuarioRetorno.ValidationResult.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Atualizar_DadoUsuarioValido_DeveAtualizarUsuarioNoBanco()
    {
        var usuarioValido = UsuarioFaker.GerarUsuario();
        await _usuarioServico.Adicionar(usuarioValido);
        usuarioValido.Creditar(_faker.Random.Float(1, 100));

        await _usuarioServico.Atualizar(usuarioValido);
        var usuarioDoBanco = await _usuarioRepositorio.ObterPorId(usuarioValido.Id);

        usuarioDoBanco.Should().NotBeNull();
        usuarioDoBanco.Should().BeEquivalentTo(usuarioValido);
    }

    [Fact]
    public async Task ObterPorId_DadoIdGuid_DeveObterUmUsuario()
    {
        var usuarioValido = UsuarioFaker.GerarUsuario();
        await _usuarioServico.Adicionar(usuarioValido);

        var usuarioDoBanco = await _usuarioServico.ObterPorId(usuarioValido.Id);

        usuarioDoBanco.Should().NotBeNull();
        usuarioDoBanco.Should().BeEquivalentTo(usuarioValido);
    }

    [Fact]
    public async Task ObterTodos_DeveObterTodosUsuariosRegistrados()
    {
        var usuarioLojistaValido = UsuarioFaker.GerarUsuarioLojista();
        var usuarioComumValido = UsuarioFaker.GerarUsuarioComum();
        await _usuarioServico.Adicionar(usuarioLojistaValido);
        await _usuarioServico.Adicionar(usuarioComumValido);

        var usuariosDoBanco = await _usuarioServico.ObterTodos();

        usuariosDoBanco.Should().NotBeNull();
        usuariosDoBanco.Should().HaveCountGreaterThan(0);
    }
}
