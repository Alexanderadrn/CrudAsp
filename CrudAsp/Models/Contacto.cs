using System;
using System.Collections.Generic;

namespace CrudAsp.Models;

public partial class Contacto
{
    
    public int IdContacto { get; set; }
    
    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }
}
