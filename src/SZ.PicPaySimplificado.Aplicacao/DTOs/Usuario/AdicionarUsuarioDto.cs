using FluentValidation.Results;
using SZ.PicPaySimplificado.CrossCutting.Enums;

namespace SZ.PicPaySimplificado.Aplicacao.DTOs.Usuario;

public class AdicionarUsuarioDto
{
	public AdicionarUsuarioDto(string nome,
		string documento,
		string email,
		TipoUsuario tipoUsuario,
		string senha,
		float saldo,
		ValidationResult validationResult)
	{
		Nome = nome;
		Documento = documento;
		Email = email;
		TipoUsuario = tipoUsuario;
		Senha = senha;
		Saldo = saldo;
		ValidationResult = validationResult;
	}

	public string Nome { get; set; }
	public string Documento { get; set; }
	public string Email { get; set; }
	public TipoUsuario TipoUsuario { get; set; }
	public string Senha { get; set; }
	public float Saldo { get; set; }

	public ValidationResult ValidationResult { get; set; }
}
