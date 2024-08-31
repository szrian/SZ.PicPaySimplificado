using SZ.PicPaySimplificado.Dados.Modulos;
using SZ.PicPaySimplificado.Dominio.Modulos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarBancoDeDados(builder.Configuration);
builder.Services.RegistrarRepositorios();
builder.Services.RegistrarServicosDeDominio();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
