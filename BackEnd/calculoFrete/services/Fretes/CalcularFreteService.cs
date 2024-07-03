using application.Data;
using application.Fretes.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using model;
using repository;
using System.Security.Claims;

namespace services.Fretes
{
    public class CalcularFreteService : IRequestHandler<CalcularFreteCommand, Response>
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CalcularFreteService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Response> Handle(
            CalcularFreteCommand request,
            CancellationToken cancellationToken)
        {
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            string uId = user.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            Usuario? usuario = _dataContext.Usuarios.FirstOrDefault(x => x.UId == uId);

            if (usuario is null)
            {
                return Task.FromResult(new Response()
                {
                    Mensagem = "Usuário não encontrado."
                });
            }

            Frete frete = new()
            {
                NomeCliente = request.NomeCliente,
                Cep = request.Cep,
                Rua = request.Rua,
                Numero = request.Numero,
                DataEntrega = request.DataEntrega,
                ValorKm = request.ValorKm,
                Distancia = request.Distancia,
                ValorFrete = CalcularValorFrete(request),
                ValorCompra = request.ValorCompra,
                Usuario = usuario,
            };

            _dataContext.Add(frete);

            var linhasAlteradas = _dataContext.SaveChanges();

            if (linhasAlteradas > 0)
            {
                return Task.FromResult(new Response()
                {
                    Mensagem = "Salvo com sucesso."
                });
            }
            else
            {
                return Task.FromResult(new Response()
                {
                    Mensagem = "Execução falhou"
                });
            }
        }

        public static decimal CalcularValorFrete(
            CalcularFreteCommand request)
        {
            decimal valorFrete = request.Distancia * request.ValorKm;

            return request.ValorCompra >= 300 && request.Cidade == "Ribeirão das Neves" ? 0 : valorFrete;
        }
    }
}