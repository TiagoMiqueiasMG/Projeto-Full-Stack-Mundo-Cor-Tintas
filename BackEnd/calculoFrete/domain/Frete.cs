namespace model
{
    public class Frete
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
        public int Usuarioid { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public void AtualizarFrete(Frete frete)
        {
            NomeCliente = frete.NomeCliente;
            Cep = frete.Cep;
            Rua = frete.Rua;
            Numero = frete.Numero;
            DataEntrega = frete.DataEntrega;
            ValorKm = frete.ValorKm;
            ValorFrete = frete.ValorFrete;
            Distancia = frete.Distancia;
            ValorCompra = frete.ValorCompra;
        }
    }
}