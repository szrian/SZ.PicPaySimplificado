using SZ.PicPaySimplificado.CrossCutting.Enums;

namespace SZ.PicPaySimplificado.Aplicacao.DTOs.Usuario;

public class ObterUsuarioDto
{
	public ObterUsuarioDto(Guid id,
		string nome,
		string documento,
		string email,
		TipoUsuario tipoUsuario,
		string senha,
		float saldo)
	{
		Id = id;
		Nome = nome;
		Documento = documento;
		Email = email;
		TipoUsuario = tipoUsuario;
		Senha = senha;
		Saldo = saldo;
	}

	public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Email { get; set; }
    public TipoUsuario TipoUsuario { get; set; }
    public string Senha { get; set; }
    public float Saldo { get; set; }
}
