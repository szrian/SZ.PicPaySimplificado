using FluentValidation.Results;
using SZ.PicPaySimplificado.Aplicacao.Enums;
using SZ.PicPaySimplificado.Dominio.Validator;

namespace SZ.PicPaySimplificado.Dominio.Modelos;

public class Usuario
{
    public Usuario()
    { }

	public Usuario(string nome, string documento, string email, TipoUsuario tipoUsuario, string senha)
	{
		Nome = nome;
		Documento = documento;
		Email = email;
		TipoUsuario = tipoUsuario;
		Senha = senha;
	}

	public Guid Id { get;  private set; }
    public string Nome { get; private set; }
    public string Documento { get; private set; }
    public string Email { get; private set; }
    public TipoUsuario TipoUsuario { get; private set; }
    public string Senha { get; private set; }
    public float Saldo { get; private set; }

	public ValidationResult ValidationResult { get; private set; }

	public virtual ICollection<Transacao> Transacoes { get; set; }

	public void Validar() =>
		ValidationResult = new UsuarioValidator().Validate(this);
}
