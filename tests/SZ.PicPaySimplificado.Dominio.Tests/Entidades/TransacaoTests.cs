using Bogus;
using FluentAssertions;
using SZ.PicPaySimplificado.Dominio.Modelos;
using SZ.PicPaySimplificado.Dominio.Tests.Fakers;

namespace SZ.PicPaySimplificado.Dominio.Tests.Entidades;

public class TransacaoTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Construtor_DadoOsParametros_DeveSetarAsPropriedadesCorretamente()
    {
        //Arrange
        var valorEsperado = _faker.Random.Float(1,100);
        var pagadorIdEsperado = Guid.NewGuid();
        var recebedorIdEsperado = Guid.NewGuid();

        //Act
        var transacao = new Transacao(valorEsperado,
            pagadorIdEsperado,
            recebedorIdEsperado);

        //Assert
        transacao.Valor.Should().Be(valorEsperado);
        transacao.PagadorId.Should().Be(pagadorIdEsperado);
        transacao.RecebedorId.Should().Be(recebedorIdEsperado);
    }

    [Fact]
    public void Validar_DadaTransacaoValida_DeveRetornarValidationResultTrue()
    {
        //Arrange
        var transacao = TransacaoFaker.GerarTransacaoValida();

        //Act
        transacao.Validar();

        //Assert
        transacao.ValidationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_DadaTransacaoInvalida_DeveRetornarValidationResultFalse()
    {
        //Arrange
        var transacao = TransacaoFaker.GerarTransacaoInvalida();

        //Act
        transacao.Validar();

        //Assert
        transacao.ValidationResult.IsValid.Should().BeFalse();
    }
}
