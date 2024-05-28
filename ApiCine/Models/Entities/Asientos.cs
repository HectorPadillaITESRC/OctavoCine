using System;
using System.Collections.Generic;

namespace ApiCine.Models.Entities;

public partial class Asientos
{
    public int Id { get; set; }

    public int? NumAsiento { get; set; }

    public int? IdTicket { get; set; }

    public virtual Tickets? IdTicketNavigation { get; set; }
}
