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
        private readonly DbContextSingleton _dbContextSingleton;
        private readonly NotiDbContext _dbContext; // Campo privado para almacenar el contexto de la base de datos

        public NotaController(DbContextSingleton dbContextSingleton)
        {
            _dbContextSingleton = dbContextSingleton;
            _dbContext = _dbContextSingleton.GetDbContext(); // Inicializa el contexto en el constructor
        }

        // Mostrar notas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Nota.ToListAsync());
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
                _dbContext.Nota.Add(nota);
                await _dbContext.SaveChangesAsync();
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

            var nota = _dbContext.Nota.Find(Id);

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
                var notaExistente = await _dbContext.Nota.FindAsync(idNota);

                if (notaExistente != null)
                {
                    // Actualizamos los campos de la nota existente con los valores nuevos
                    notaExistente.TituloNota = nota.TituloNota;
                    notaExistente.Descripcion = nota.Descripcion;

                    // Guardamos los cambios
                    await _dbContext.SaveChangesAsync();
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

            var nota = await _dbContext.Nota.FindAsync(Id);

            if (nota == null)
            {
                return NotFound();
            }

            _dbContext.Nota.Remove(nota);
            await _dbContext.SaveChangesAsync();

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