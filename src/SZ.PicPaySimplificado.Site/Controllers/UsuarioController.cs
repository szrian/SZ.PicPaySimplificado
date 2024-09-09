using Microsoft.AspNetCore.Mvc;
using SZ.PicPaySimplificado.Aplicacao.DTOs.Usuario;
using SZ.PicPaySimplificado.Aplicacao.Interfaces;

namespace SZ.PicPaySimplificado.Site.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly IUsuarioAppService _usuarioAppService;
		public UsuarioController(IUsuarioAppService usuarioAppService)
		{
			_usuarioAppService = usuarioAppService;
		}

		[Route("novo-usuario")]
		[HttpPost]
		public async Task<ActionResult> Adicionar(AdicionarUsuarioDto usuarioDto)
		{
			if (usuarioDto == null)
				return BadRequest();

			var retorno	= await _usuarioAppService.Adicionar(usuarioDto);

			if (!retorno.ValidationResult.IsValid)
				return BadRequest(retorno.ValidationResult.Errors.Select(p => p.ErrorMessage));

			return Ok("Usuário cadastrado com sucesso!");
		}

		[Route("atualizar-usuario")]
		[HttpPut]
		public async Task<ActionResult> Atualizar(AdicionarUsuarioDto usuarioDto)
		{
			if (usuarioDto == null)
				return BadRequest();

			var retorno = await _usuarioAppService.Atualizar(usuarioDto);

			if (!retorno.ValidationResult.IsValid)
				return BadRequest(retorno.ValidationResult.Errors.Select(p => p.ErrorMessage));

			return Ok("Usuário atualizado com sucesso!");
		}

		[Route("obter-por-id/{id:guid}")]
		[HttpGet]
		public async Task<ActionResult<ObterUsuarioDto>> ObterPorId(Guid id)
		{
			if (id == Guid.Empty)
				return BadRequest();

			return await _usuarioAppService.ObterPorId(id);
		}

		[Route("obter-todos")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ObterUsuarioDto>>> ObterTodos()
		{
			return Ok(await _usuarioAppService.ObterTodos());
		}
	}
}
