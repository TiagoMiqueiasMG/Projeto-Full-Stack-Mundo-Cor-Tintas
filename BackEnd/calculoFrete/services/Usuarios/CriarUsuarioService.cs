using application.Data;
using application.Usuarios.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using model;
using repository;
using System.Security.Claims;

namespace services.Usuarios
{
    public class CriarUsuarioService : IRequestHandler<CriarUsuarioCommand, Response>
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CriarUsuarioService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Response> Handle(
            CriarUsuarioCommand request, 
            CancellationToken cancellationToken)
        {
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            string uId = user.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            Usuario usuario = new()
            {
                Nome = request.Nome,
                Email = request.Email,
                UId = uId
            };

            _dataContext.Add(usuario);

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
    }
}