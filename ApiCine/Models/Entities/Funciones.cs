using System;
using System.Collections.Generic;

namespace ApiCine.Models.Entities;

public partial class Funciones
{
    public int Id { get; set; }

    public string NombrePelicula { get; set; } = null!;

    public string Horario { get; set; } = null!;

    public int NumSala { get; set; }

    public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();
}
