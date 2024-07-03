using MediatR;
using model;

namespace application.Usuarios.Commands
{
    public class AtualizarUsuarioCommand : IRequest<Usuario>
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
