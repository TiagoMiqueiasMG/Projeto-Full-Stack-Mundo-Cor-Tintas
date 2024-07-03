using application.Fretes.Data;
using application.Fretes.Queries;
using application.Usuarios.Commands;
using Mapster;
using MediatR;
using model;

namespace repository.Fretes
{
    internal class ListarFretesRepository : IRequestHandler<ListarFretesQuery, IEnumerable<FreteResponse>>
    {
        private readonly DataContext _dataContext;
        private readonly IMediator _mediator;

        public ListarFretesRepository(
            DataContext dataContext,
            IMediator mediator)
        {
            _dataContext = dataContext;
            _mediator = mediator;
        }

        public Task<IEnumerable<FreteResponse>> Handle(
            ListarFretesQuery request,
            CancellationToken cancellationToken)
        {
            Task<Usuario> usuario = ObterUsuarioAsync(cancellationToken);

            IEnumerable<Frete> fretes = _dataContext.Fretes.Where(x => x.Usuarioid == usuario.Result.Id).ToList();

            IEnumerable<FreteResponse> fretesResponse = fretes.Adapt<IEnumerable<FreteResponse>>();

            return Task.FromResult(fretesResponse);
        }

        private async Task<Usuario> ObterUsuarioAsync(
            CancellationToken cancellationToken)
        {
            ObterUsuarioCommand command = new();

            return await _mediator.Send(command, cancellationToken);
        }
    }
}