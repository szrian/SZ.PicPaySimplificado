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

		[HttpPost]
		public async Task<ActionResult<bool>> Adicionar(AdicionarUsuarioDto usuarioDto)
		{
			if (usuarioDto == null)
				return BadRequest();

			await _usuarioAppService.Adicionar(usuarioDto);

			return Ok(true);
		}

		[HttpPut]
		public async Task<ActionResult> Atualizar(AdicionarUsuarioDto usuarioDto)
		{
			if (usuarioDto == null)
				return BadRequest();

			await _usuarioAppService.Atualizar(usuarioDto);

			return Ok(true);
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<ObterUsuarioDto>> ObterPorId(Guid id)
		{
			if (id == Guid.Empty)
				return BadRequest();

			return await _usuarioAppService.ObterPorId(id);
		}
	}
}
