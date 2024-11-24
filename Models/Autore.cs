using System;
using System.Collections.Generic;

namespace CRUDXimenaCastanedaEF.Models;

public partial class Autore
{
    public int IdAutor { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<LibrosAutore> LibrosAutores { get; set; } = new List<LibrosAutore>();
}
