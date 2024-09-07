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
	public async Task<ActionResult> Transferir(TransacaoDto transacaoDto)
	{
		if (transacaoDto == null)
			return BadRequest("Informe os dados para transação.");

		var retorno = await _transacaoAppService.Adicionar(transacaoDto);
		if (retorno.ValidationResult.Errors.Any())
			return BadRequest(retorno.ValidationResult.Errors.Select(p => p.ErrorMessage));

		return Ok("Transação realizada com sucesso!");
	}
}
