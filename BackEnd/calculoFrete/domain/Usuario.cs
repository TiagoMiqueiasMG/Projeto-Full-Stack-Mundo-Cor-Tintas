namespace model;

public partial class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string UId { get; set; }
    public virtual List<Frete> Fretes { get; set; }

    public void AtualizarUsuario(Usuario usuario)
    {
        Nome = usuario.Nome;
        Email = usuario.Email;
    }
}