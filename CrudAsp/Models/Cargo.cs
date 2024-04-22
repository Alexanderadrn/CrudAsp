using System;
using System.Collections.Generic;

namespace CrudAsp.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string? NombreCargo { get; set; }

    public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();
}
