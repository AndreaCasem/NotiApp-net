using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotiApp.Models;

public partial class Nota
{
    public int IdNota { get; set; }

    public string? TituloNota { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }



    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
