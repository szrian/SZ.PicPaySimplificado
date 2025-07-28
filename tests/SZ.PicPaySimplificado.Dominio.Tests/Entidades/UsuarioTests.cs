using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using SZ.PicPaySimplificado.CrossCutting.Enums;
using SZ.PicPaySimplificado.Dominio.Modelos;
using SZ.PicPaySimplificado.Dominio.Tests.Fakers;

namespace SZ.PicPaySimplificado.Dominio.Tests.Entidades;

public class UsuarioTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Construtor_DadoOsParametros_DeveSetarAsPropriedadesCorretamenteSemId()
    {
        //Arrange
        var nomeUsuarioEsperado = _faker.Person.FirstName;
        var documentoUsuarioEsperado = _faker.Person.Cpf(false);
        var emailUsuarioEsperado = _faker.Person.Email;
        var tipoUsuarioEsperado = _faker.PickRandom<TipoUsuario>();
        var senhaUsuarioEsperado = _faker.Random.Word();
        var saldoUsuarioEsperado = _faker.Random.Float();


        //Act
        var usuario = new Usuario(nomeUsuarioEsperado,
            documentoUsuarioEsperado,
            emailUsuarioEsperado,
            tipoUsuarioEsperado,
            senhaUsuarioEsperado,
            saldoUsuarioEsperado);


        //Assert
        usuario.Id.Should().BeEmpty();
        usuario.Nome.Should().Be(nomeUsuarioEsperado);
        usuario.Documento.Should().Be(documentoUsuarioEsperado);
        usuario.Email.Should().Be(emailUsuarioEsperado);
        usuario.TipoUsuario.Should().Be(tipoUsuarioEsperado);
        usuario.Senha.Should().Be(senhaUsuarioEsperado);
        usuario.Saldo.Should().Be(saldoUsuarioEsperado);
    }

    [Fact]
    public void Construtor_DadoOsParametros_DeveSetarAsPropriedadesCorretamenteComId()
    {
        //Arrange
        var idUsuarioEsperado = Guid.NewGuid();
        var nomeUsuarioEsperado = _faker.Person.FirstName;
        var documentoUsuarioEsperado = _faker.Person.Cpf(false);
        var emailUsuarioEsperado = _faker.Person.Email;
        var tipoUsuarioEsperado = _faker.PickRandom<TipoUsuario>();
        var senhaUsuarioEsperado = _faker.Random.Word();
        var saldoUsuarioEsperado = _faker.Random.Float();


        //Act
        var usuario = new Usuario(idUsuarioEsperado,
            nomeUsuarioEsperado,
            documentoUsuarioEsperado,
            emailUsuarioEsperado,
            tipoUsuarioEsperado,
            senhaUsuarioEsperado,
            saldoUsuarioEsperado);


        //Assert
        usuario.Id.Should().Be(idUsuarioEsperado);
        usuario.Nome.Should().Be(nomeUsuarioEsperado);
        usuario.Documento.Should().Be(documentoUsuarioEsperado);
        usuario.Email.Should().Be(emailUsuarioEsperado);
        usuario.TipoUsuario.Should().Be(tipoUsuarioEsperado);
        usuario.Senha.Should().Be(senhaUsuarioEsperado);
        usuario.Saldo.Should().Be(saldoUsuarioEsperado);
    }

    [Fact]
    public void EhUsuarioComum_DadoUsuarioComum_DeveRetornarTrue()
    {
        //Arrange
        var usuario = UsuarioFaker.GerarUsuarioComum();

        //Assert
        usuario.EhUsuarioComum().Should().BeTrue();
    }

    [Fact]
    public void EhUsuarioComum_DadoUsuarioLojista_DeveRetornarFalse()
    {
        //Arrange
        var usuario = UsuarioFaker.GerarUsuarioLojista();

        //Assert
        usuario.EhUsuarioComum().Should().BeFalse();
    }

    [Fact]
    public void Creditar_DadoValorFloat_DeveCreditarNoSaldoDoUsuario()
    {
        //Arrange
        var usuario = UsuarioFaker.GerarUsuario();
        var valorParaCreditar = _faker.Random.Float();
        var valorEsperado = usuario.Saldo + valorParaCreditar;

        //Act
        usuario.Creditar(valorParaCreditar);

        //Assert
        usuario.Saldo.Should().Be(valorEsperado);
    }

    [Fact]
    public void Debitar_DadoValorFloat_DeveDebitarDoSaldoDoUsuario()
    {
        //Arrange
        var usuario = UsuarioFaker.GerarUsuario();
        var valorParaDebitar = _faker.Random.Float();
        var valorEsperado = usuario.Saldo - valorParaDebitar;

        //Act
        usuario.Debitar(valorParaDebitar);

        //Assert
        usuario.Saldo.Should().Be(valorEsperado);
    }

    [Fact]
    public void Validar_DadoUsuarioValido_DeveRetornarValidationResultTrue()
    {
        //Arrange
        var usuario = UsuarioFaker.GerarUsuario();

        //Act
        usuario.Validar();

        //Assert
        usuario.ValidationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_DadoUsuarioInvalido_DeveRetornarValidationResultFalse()
    {
        //Arrange
        var usuario = UsuarioFaker.GerarUsuarioInvalido();

        //Act
        usuario.Validar();

        //Assert
        usuario.ValidationResult.IsValid.Should().BeFalse();
    }
}
