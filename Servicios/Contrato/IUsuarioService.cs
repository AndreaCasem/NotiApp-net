using Microsoft.EntityFrameworkCore;
using NotiApp.Models;

namespace NotiApp.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave); // Obtener correo y clave
        Task<Usuario> SaveUsuario(Usuario modelo); // Guardar usuario

    }
}
