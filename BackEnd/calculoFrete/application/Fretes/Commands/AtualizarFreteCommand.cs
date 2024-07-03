using MediatR;
using model;

namespace application.Fretes.Commands
{
    public class AtualizarFreteCommand : IRequest<Frete>
    {
        public int Id { get; set; }
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