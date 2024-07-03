using application.Fretes.Data;
using MediatR;

namespace application.Fretes.Queries
{
    public class ListarFretesQuery : IRequest<IEnumerable<FreteResponse>>
    {
    }
}