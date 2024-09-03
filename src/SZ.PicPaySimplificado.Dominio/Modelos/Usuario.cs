using FluentValidation.Results;
using SZ.PicPaySimplificado.CrossCutting.Enums;
using SZ.PicPaySimplificado.Dominio.Validator;

namespace SZ.PicPaySimplificado.Dominio.Modelos;

public class Usuario
{
	public Usuario(string nome,
		string documento,
		string email,
		TipoUsuario tipoUsuario,
		string senha,
		float saldo)
	{
		Nome = nome;
		Documento = documento;
		Email = email;
		TipoUsuario = tipoUsuario;
		Senha = senha;
		Saldo = saldo;
	}

	public Usuario(Guid id,
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

	public bool EhUsuarioComum() => TipoUsuario == TipoUsuario.Comum;

	public void Creditar(float valor) => Saldo += valor;
	public void Debitar(float valor) => Saldo -= valor;
}
