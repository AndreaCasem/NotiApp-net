using NotiApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NotiApp
{
    // Clase sellada (sealed) para evitar que se pueda heredar y solo pueda haber una instancia.
    public sealed class DbContextSingleton
    {
        // _instance almacena la única instancia de la clase DbContextSingleton y se inica como nula al principio
        private static DbContextSingleton _instance; 
        private static readonly object _lock = new object();
        // Este servicio se pasa al constructor para obtener instancias de NotiDbContext
        private readonly IServiceProvider _serviceProvider;
        private NotiDbContext _dbContext;

        // Con este constructor privado no se puede crear una instancia desde fuera de esta clase
        private DbContextSingleton(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _dbContext = null; // Inicialmente no se ha creado la instancia del contexto.
        }

        public static DbContextSingleton Instance(IServiceProvider serviceProvider)
        {
            // Garantizar que solo se cree una instancia si _instance esta nula.
            // Si _instance ya tiene algún valor devuelve la instancia que existe
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DbContextSingleton(serviceProvider);
                }

                return _instance;
            }
        }

        // Método para obtener una instancia de NotiDbContext
        public NotiDbContext GetDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = _serviceProvider.GetRequiredService<NotiDbContext>();
            }

            return _dbContext;
        }
    }
}
