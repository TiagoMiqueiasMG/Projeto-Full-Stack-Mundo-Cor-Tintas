using application.Data;
using application.Usuarios.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using model;

namespace api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("obter")]
        public async Task<Usuario> ObterUsuario()
        {
            ObterUsuarioCommand command = new();

            return await _mediator.Send(command);
        }

        [HttpPost("criar")]
        public async Task<Response> CriarUsuario(
            [FromBody] CriarUsuarioCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("atualizar")]
        public async Task<Usuario> AtualizarUsuario(
            [FromBody] AtualizarUsuarioCommand comand)
        {
            return await _mediator.Send(comand);
        }

        [HttpDelete("deletar")]
        public async Task<Response> DeletarUsuario()
        {
            DeletarUsuarioCommand command = new();

            return await _mediator.Send(command);
        }
    }
}