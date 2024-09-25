using Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryEntity.Context;
using RepositoryEntity.Models;

namespace Services
{
    public class UsuarioService
    {
        private readonly string? _pepper;
        private readonly int _iteration = 3;
        private readonly IConfiguration _config;
        private readonly BancoContext _context;

        public UsuarioService(IConfiguration config, BancoContext context)
        {
            _config = config;
            _pepper = _config?["Hash:pepper"];
            _context = context;
        }

        public UsuarioResource Registro(RegistroResource resource)
        {
            var usuario = new Usuario
            {
                UserName = resource.Username,
                Email = resource.Email,
                SenhaSalt = SenhaHash.GenerateSalt()
            };

            usuario.SenhaHash = SenhaHash.ComputeHash(resource.Senha, usuario.SenhaSalt, _pepper, _iteration);

            _context.Add(usuario);
            _context.SaveChanges();

            return new UsuarioResource(usuario.Id, usuario.UserName, usuario.Email);
        }

        public UsuarioResource Login(LoginResource resource)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.UserName == resource.Username);

            if (usuario == null)
                throw new Exception("Username e senha não conferem.");

            var passwordHash = SenhaHash.ComputeHash(resource.Senha, usuario.SenhaSalt, _pepper, _iteration);

            if (usuario.SenhaHash != passwordHash)
                throw new Exception("Username e senha não conferem.");

            return new UsuarioResource(usuario.Id, usuario.UserName, usuario.Email);
        }
    }
}