using Microsoft.AspNetCore.Mvc;
using SZ.PicPaySimplificado.Aplicacao.DTOs.Transacao;
using SZ.PicPaySimplificado.Aplicacao.Interfaces;

namespace SZ.PicPaySimplificado.Site.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransacaoController : ControllerBase
{
	private readonly ITransacaoAppService _transacaoAppService;
	public TransacaoController(ITransacaoAppService transacaoAppService)
	{
		_transacaoAppService = transacaoAppService;
	}

	[HttpPost]
	public async Task<ActionResult<bool>> Transferir(TransacaoDto transacaoDto)
	{
		if (transacaoDto == null)
			return BadRequest(false);

		await _transacaoAppService.Adicionar(transacaoDto);

		return Ok(true);
	}
}
