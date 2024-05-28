using System;
using System.Collections.Generic;

namespace ApiCine.Models.Entities;

public partial class Clientes
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();
}
