namespace SZ.PicPaySimplificado.Clients.NotificacaoApi.Interfaces;

public interface INotificadorServico
{
	Task<bool> NotificarTransacao();
}
