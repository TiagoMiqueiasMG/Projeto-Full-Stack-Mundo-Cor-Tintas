using MediatR;
using model;

namespace application.Fretes.Commands
{
    public class DeletarFreteCommand : IRequest<Frete>
    {
        public int Id { get; set; }
    }
}
