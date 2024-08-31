namespace SZ.PicPaySimplificado.Dominio.Modelos;

public class Transacao
{
    public Transacao()
    { }

    public Guid Id { get; private set; }
    public float Valor {  get; private set; }
    public Guid PagadorId { get; private set; }
    public Guid RecebedorId { get; private set; }
    public DateTime DataTransacao { get; private set; }

    public virtual Usuario Pagador { get; set; }
    public virtual Usuario Recebedor { get; set; }
}
