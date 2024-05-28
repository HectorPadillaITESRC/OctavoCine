using ApiCine.Models.DTOs.ApiCine.DTOs;
using ApiCine.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticiasOctavoAPI.Repositories;

namespace ApiCine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IRepository<Clientes> _repository;

        public ClientesController(IRepository<Clientes> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<ClienteDTO> GetAll()
        {
            var clientes = _repository.GetAll();
            return clientes.Select(c => MapToDto(c));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cliente = _repository.Get(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(cliente));
        }

        [HttpPost]
        public IActionResult Post(ClienteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(errors);
            }

            var cliente = MapToEntity(dto);
            _repository.Insert(cliente);
            return Ok();
        }

        public IActionResult Put(ClienteDTO dto)
        {
        
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(errors);
            }

            var cliente = _repository.Get(dto.Id);
            if (cliente == null)
            {
                return NotFound();
            }

            MapToEntity(dto, cliente);
            _repository.Update(cliente);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _repository.Get(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _repository.Delete(cliente);
            return Ok();
        }

        private ClienteDTO MapToDto(Clientes cliente)
        {
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Usuario = cliente.Usuario,
                Contraseña = cliente.Contraseña
            };
        }

        private Clientes MapToEntity(ClienteDTO dto, Clientes? original = null)
        {
            if (original == null)
            {
                original = new Clientes();
            }

            return new Clientes
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Usuario = dto.Usuario,
                Contraseña = dto.Contraseña
            };
        }
    }
}
