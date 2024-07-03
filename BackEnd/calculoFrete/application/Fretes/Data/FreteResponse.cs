namespace application.Fretes.Data
{
    public class FreteResponse
    {
        public int Id { get; set; }
        public required string NomeCliente { get; set; }
        public required string Cep { get; set; }
        public required string Rua { get; set; }
        public int Numero { get; set; }
        public DateTime? DataEntrega { get; set; }
        public decimal ValorKm { get; set; }
        public decimal Distancia { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorCompra { get; set; }
    }
}