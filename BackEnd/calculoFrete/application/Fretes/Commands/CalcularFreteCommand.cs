using application.Data;
using MediatR;

namespace application.Fretes.Commands
{
    public class CalcularFreteCommand : IRequest<Response>
    {
        public string NomeCliente { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal ValorKm { get; set; }
        public decimal Distancia { get; set; }
        public decimal ValorCompra { get; set; }
    }
}