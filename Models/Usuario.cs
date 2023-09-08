using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotiApp.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
