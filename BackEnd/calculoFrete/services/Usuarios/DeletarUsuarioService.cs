using application.Data;
using application.Usuarios.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using model;
using repository;
using System.Security.Claims;

namespace services.Usuarios
{
    public class DeletarUsuarioService : IRequestHandler<DeletarUsuarioCommand, Response>
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeletarUsuarioService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Response> Handle(
            DeletarUsuarioCommand request, 
            CancellationToken cancellationToken)
        {
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            string uId = user.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            Usuario usuario = _dataContext
                .Usuarios
                .FirstOrDefault(x => x.UId == uId)!;

            _dataContext.Remove(usuario);

            int linhasAlteradas = _dataContext.SaveChanges();

            if (linhasAlteradas > 0)
            {
                return Task.FromResult(new Response()
                {
                    Mensagem = "Deletado com sucesso."
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
    }
}