using application.Usuarios.Commands;
using MediatR;
using model;
using repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace services.Usuarios
{
    public class ObterUsuarioService : IRequestHandler<ObterUsuarioCommand, Usuario>
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ObterUsuarioService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Usuario> Handle(
            ObterUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            string uId = user.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            Usuario? usuario = _dataContext.Usuarios.FirstOrDefault(x => x.UId == uId);

            return Task.FromResult(usuario)!;
        }
    }
}