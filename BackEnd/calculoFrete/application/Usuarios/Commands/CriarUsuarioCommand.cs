using application.Data;
using MediatR;

namespace application.Usuarios.Commands
{
    public class CriarUsuarioCommand : IRequest<Response>
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}