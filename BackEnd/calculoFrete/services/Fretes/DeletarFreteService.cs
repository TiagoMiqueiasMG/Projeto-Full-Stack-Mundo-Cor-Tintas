using application.Fretes.Commands;
using MediatR;
using model;
using repository;

namespace services.Fretes
{
    public class DeletarFreteService : IRequestHandler<DeletarFreteCommand, Frete>
    {
        private readonly DataContext _dataContext;

        public DeletarFreteService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<Frete> Handle(
            DeletarFreteCommand request,
            CancellationToken cancellationToken)
        {
            Frete frete = _dataContext
                .Fretes
                .FirstOrDefault(x => x.Id == request.Id)!;

            _dataContext.Remove(frete);

            var linhasAlteradas = _dataContext.SaveChanges();

            return Task.FromResult(frete);
        }
    }
}