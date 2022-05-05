using DotaApi.Dtos;
using DotaApi.Enums;
using DotaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotaApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PersonagemController : ControllerBase
    {
        public readonly IPersonagemService _personagemService;

        public PersonagemController(IPersonagemService personagemService)
        {
            _personagemService = personagemService;
        }

        [HttpPost(Name = "PostPersonagem")]
        public IActionResult Post([FromBody] EntradaDto? dadosEntrada)
        {
            var retorno = _personagemService.InserirPersonagem(dadosEntrada);

            if (retorno.Status.Equals(SistemaEnum.Retorno.BadRequest)) return BadRequest(retorno.Mensagem);

            return Created(retorno.Mensagem, retorno.Retorno);
        }


        [HttpGet(Name = "GetPersonagem")]
        public IActionResult Get(Guid? id, [FromQuery] EntradaDto? dadosEntrada)
        {
            var retorno = _personagemService.PegarPersonagem(id, dadosEntrada);

            if (retorno.Status.Equals(SistemaEnum.Retorno.NotFound)) return NotFound(retorno.Mensagem);

            if (retorno.Status.Equals(SistemaEnum.Retorno.BadRequest)) return BadRequest(retorno.Mensagem);

            return Ok(retorno.Retorno);
        }

        [HttpPut(Name = "PutPersonagem")]
        public IActionResult Put(Guid? id, [FromBody] EntradaDto? dadosEntrada)
        {
            var retorno = _personagemService.MudarPersonagem(id, dadosEntrada);

            if (retorno.Status.Equals(SistemaEnum.Retorno.NotFound)) return NotFound(retorno.Mensagem);

            if (retorno.Status.Equals(SistemaEnum.Retorno.BadRequest)) return BadRequest(retorno.Mensagem);

            return Ok(retorno.Mensagem);
        }


        [HttpPatch(Name = "PatchPersonagem")]
        public IActionResult Patch(Guid? id, [FromBody] EntradaDto? dadosEntrada)
        {
            var retorno = _personagemService.AtualizarPersonagem(id, dadosEntrada);

            if (retorno.Status.Equals(SistemaEnum.Retorno.NotFound)) return NotFound(retorno.Mensagem);

            if (retorno.Status.Equals(SistemaEnum.Retorno.BadRequest)) return BadRequest(retorno.Mensagem);

            return Ok(retorno.Mensagem);
        }

        [HttpDelete(Name = "DeletePersonagem")]
        public IActionResult Delete(Guid? id)
        {
            var retorno = _personagemService.DeletarPersonagem(id);

            if (retorno.Status.Equals(SistemaEnum.Retorno.NotFound)) return NotFound(retorno.Mensagem);

            if (retorno.Status.Equals(SistemaEnum.Retorno.BadRequest)) return BadRequest(retorno.Mensagem);

            return Ok(retorno.Mensagem);
        }
    }
}