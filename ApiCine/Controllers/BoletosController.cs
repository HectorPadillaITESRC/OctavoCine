using ApiCine.Models.DTOs;
using ApiCine.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticiasOctavoAPI.Repositories;

namespace ApiCine.Controllers
{
    [Authorize(Roles="Cliente")]
    [Route("api/[controller]")]
    [ApiController]
    public class BoletosController : ControllerBase
    {
        public class TicketsController : ControllerBase
        {
            private readonly IRepository<Tickets> _ticketRepository;
            private readonly IRepository<Asientos> _asientosRepository;

            public TicketsController(IRepository<Tickets> ticketRepository, IRepository<Asientos> asientosRepository)
            {
                _ticketRepository = ticketRepository;
                _asientosRepository = asientosRepository;
            }

            [HttpPost("Comprar")]
            public IActionResult ComprarBoletos(ComprarBoletosDTO dto)
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
                    return BadRequest(errors);
                }

                var clienteId = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (clienteId == null || !int.TryParse(clienteId.Value, out int id))
                {
                    return Unauthorized();
                }

                if (_asientosRepository.GetAll().Any(x=> dto.NumerosAsientos.Contains(x.NumAsiento??0)))
                {
                    return BadRequest("Uno o más asientos ya están ocupados.");
                }

                var nuevoTicket = new Tickets
                {
                    IdFuncion = dto.IdFuncion,
                    IdCliente = id,
                    Asientos = dto.NumerosAsientos.Select(numAsiento => new Asientos { NumAsiento = numAsiento }).ToList()
                };

                _ticketRepository.Insert(nuevoTicket);

                return Ok();
            }
        }
    }
}
