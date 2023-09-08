using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotiApp.Models;
using System.Security.Claims;

namespace NotiApp.Controllers
{
    public class NotaController : Controller
    {
        private readonly NotiDbContext _contexto;

        public NotaController(NotiDbContext contexto)
        {
            _contexto = contexto;
        }

        // Mostrar notas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Nota.ToListAsync());
        }

   
        [HttpGet]
        public IActionResult CrearNota()
        {
            return View();
        }

        // Crear notas
        [HttpPost]
        public async Task<IActionResult> CrearNota(Nota nota)
        {

            if (nota != null)
            {
                _contexto.Nota.Add(nota);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult EditarNota(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var nota = _contexto.Nota.Find(Id);

            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // Editar notas
        [HttpPost]
        public async Task<IActionResult> EditarNota(Nota nota)
        {
            if (nota != null)
            {
                // Recogemos el valor de IdNota del formulario
                int idNota = nota.IdNota;

                // Buscamos la nota correspondiente en la base de datos
                var notaExistente = await _contexto.Nota.FindAsync(idNota);

                if (notaExistente != null)
                {
                    // Actualizamos los campos de la nota existente con los valores nuevos
                    notaExistente.TituloNota = nota.TituloNota;
                    notaExistente.Descripcion = nota.Descripcion;

                    // Guardamos los cambios
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(nota);
            }
        }

        // Eliminar nota
        [HttpPost]
        public async Task<IActionResult> EliminarNota(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var nota = await _contexto.Nota.FindAsync(Id);

            if (nota == null)
            {
                return NotFound();
            }

            _contexto.Nota.Remove(nota);
            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        // Cerrar sesión
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("IniciarSesion", "Inicio");
        }
    }
}
