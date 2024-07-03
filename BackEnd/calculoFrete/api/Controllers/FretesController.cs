using application.Data;
using application.Fretes.Commands;
using application.Fretes.Data;
using application.Fretes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using model;

namespace api.Controllers
{
    [ApiController]
    [Route("fretes")]
    [Authorize]
    public class FretesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FretesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("calcular")]
        public async Task<Response> CalcularFrete(
            [FromBody]CalcularFreteCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("listar")]
        public async Task<IEnumerable<FreteResponse>> ListarFretes()
        {
            ListarFretesQuery query = new();

            return await _mediator.Send(query);
        }

        [HttpPut("atualizar")]
        public async Task<Frete> AtualizarFrete(
            [FromBody] AtualizarFreteCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("deletar")]
        public async Task<Frete> DeletarFrete(
            [FromBody] DeletarFreteCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
