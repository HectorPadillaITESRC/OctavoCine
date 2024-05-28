using System;
using System.Collections.Generic;

namespace ApiCine.Models.Entities;

public partial class Tickets
{
    public int Id { get; set; }

    public int IdFuncion { get; set; }

    public int IdCliente { get; set; }

    public virtual ICollection<Asientos> Asientos { get; set; } = new List<Asientos>();

    public virtual Clientes IdClienteNavigation { get; set; } = null!;

    public virtual Funciones IdFuncionNavigation { get; set; } = null!;
}
