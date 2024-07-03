using application.Usuarios.Commands;
using MediatR;
using model;
using repository;

namespace services.Usuarios
{
    public class AtualizarUsuarioService : IRequestHandler<AtualizarUsuarioCommand, Usuario>
    {
        private readonly IMediator _mediator;
        private readonly DataContext _dataContext;

        public AtualizarUsuarioService(DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
            _mediator = mediator;
        }

        public Task<Usuario> Handle(
            AtualizarUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            Task<Usuario> usuario = ObterUsuarioAsync(cancellationToken);

            Usuario usuarioMapeado = MapearUsuario(request);

            usuario.Result.AtualizarUsuario(usuarioMapeado);

            _dataContext.Update(usuario.Result);

            var linhasAlteradas = _dataContext.SaveChanges();

            return Task.FromResult(usuario.Result);
        }

        private async Task<Usuario> ObterUsuarioAsync(CancellationToken cancellationToken)
        {
            ObterUsuarioCommand command = new();

            return await _mediator.Send(command, cancellationToken);
        }

        private static Usuario MapearUsuario(AtualizarUsuarioCommand request)
        {
            return new()
            {
                Nome = request.Nome,
                Email = request.Email,
            };
        }
    }
}
