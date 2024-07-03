using application.Fretes.Commands;
using MediatR;
using model;
using repository;

namespace services.Fretes
{
    public class AtualizarFreteService : IRequestHandler<AtualizarFreteCommand, Frete>
    {
        private readonly DataContext _dataContext;

        public AtualizarFreteService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<Frete> Handle(
            AtualizarFreteCommand request,
            CancellationToken cancellationToken)
        {
            Frete frete = _dataContext
                .Fretes
                .FirstOrDefault(x => x.Id == request.Id)!;

            Frete freteMapeado = MapearFrete(request);

            frete.AtualizarFrete(freteMapeado);

            _dataContext.Update(frete);

            var linhasAlteradas = _dataContext.SaveChanges();

            return Task.FromResult(frete);
        }

        private static Frete MapearFrete(AtualizarFreteCommand request)
        {
            return new()
            {
                NomeCliente = request.NomeCliente,
                Cep = request.Cep,
                Rua = request.Rua,
                Numero = request.Numero,
                DataEntrega = request.DataEntrega,
                ValorKm = request.ValorKm,
                Distancia = request.Distancia,
                ValorFrete = CalcularValorFrete(request),
                ValorCompra = request.ValorCompra
            };
        }

        public static decimal CalcularValorFrete(
            AtualizarFreteCommand request)
        {
            decimal valorFrete = request.Distancia * request.ValorKm;

            return request.ValorCompra >= 300 && request.Cidade == "Ribeirão das Neves" ? 0 : valorFrete;
        }
    }
}