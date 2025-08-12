using Bogus;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Tests.Fakers;

public static class TransacaoFaker
{
    public static Transacao GerarTransacaoValida()
    {
        var faker = new Faker("pt_BR");
        return new Transacao(faker.Random.Float(1, 100),
            Guid.NewGuid(),
            Guid.NewGuid());
    }

    public static Transacao GerarTransacaoValidaComUsuarios(Guid pagadorId, Guid recebedorId)
    {
        var faker = new Faker("pt_BR");
        return new Transacao(faker.Random.Float(1, 100),
            pagadorId,
            recebedorId);
    }

    public static Transacao GerarTransacaoInvalida()
    {
        var faker = new Faker("pt_BR");
        return new Transacao(0,
            Guid.Empty,
            Guid.Empty);
    }
}
