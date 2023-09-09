using Microsoft.EntityFrameworkCore;
using NotiApp.Models;
using NotiApp.Servicios.Contrato;

namespace NotiApp.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DbContextSingleton _dbContextSingleton;
        private readonly NotiDbContext _dbContext;

        public UsuarioService(DbContextSingleton dbContextSingleton)
        {
            _dbContextSingleton = dbContextSingleton;
            _dbContext = _dbContextSingleton.GetDbContext();
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuarioEncontrado = await _dbContext.Usuarios.Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefaultAsync();

            return usuarioEncontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();

            return modelo;
        }
    }
}
