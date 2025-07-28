using Bogus;
using Bogus.Extensions.Brazil;
using SZ.PicPaySimplificado.CrossCutting.Enums;
using SZ.PicPaySimplificado.Dominio.Modelos;

namespace SZ.PicPaySimplificado.Dominio.Tests.Fakers;

internal static class UsuarioFaker
{
    public static Usuario GerarUsuario()
    {
        var faker = new Faker("pt_BR");

        return new Usuario(faker.Person.FirstName,
            faker.Person.Cpf(false),
            faker.Person.Email,
            faker.PickRandom<TipoUsuario>(),
            faker.Internet.Password(),
            faker.Random.Float());
    }

    public static Usuario GerarUsuarioInvalido()
    {
        var faker = new Faker("pt_BR");

        return new Usuario(string.Empty,
            string.Empty,
            faker.Random.Word(),
            faker.PickRandom<TipoUsuario>(),
            faker.Internet.Password(),
            faker.Random.Float());
    }

    public static Usuario GerarUsuarioComum()
    {
        var faker = new Faker("pt_BR");

        return new Usuario(faker.Person.FirstName,
            faker.Person.Cpf(false),
            faker.Person.Email,
            TipoUsuario.Comum,
            faker.Internet.Password(),
            faker.Random.Float());
    }

    public static Usuario GerarUsuarioLojista()
    {
        var faker = new Faker("pt_BR");

        return new Usuario(faker.Company.CompanyName(),
            faker.Company.Cnpj(false),
            faker.Person.Email,
            TipoUsuario.Lojista,
            faker.Internet.Password(),
            faker.Random.Float());
    }
}
