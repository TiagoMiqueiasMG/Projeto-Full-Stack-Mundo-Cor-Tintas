using MediatR;
using model;

namespace application.Usuarios.Commands
{
    public class ObterUsuarioCommand : IRequest<Usuario>
    {
    }
}