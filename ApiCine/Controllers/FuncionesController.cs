using ApiCine.Models.DTOs;
using ApiCine.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticiasOctavoAPI.Repositories;

namespace ApiCine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class FuncionesController : ControllerBase
    {

        private readonly IRepository<Funciones> _repository;

        public FuncionesController(IRepository<Funciones> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<FuncionesDTO> GetAll()
        {
            var funciones = _repository.GetAll();
            return funciones.Select(f => MapToDto(f));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var funcion = _repository.Get(id);
            if (funcion == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(funcion));
        }

        [HttpPost]
        public IActionResult Post(FuncionesDTO dto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(errors);
            }

            var funcion = MapToEntity(dto);
            
            _repository.Insert(funcion);
            return Ok();
        }

        [HttpPost]
        public IActionResult Put(FuncionesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(errors);
            }

            var funcion = _repository.Get(dto.Id);
            if (funcion == null)
            {
                return NotFound();
            }

            MapToEntity(dto, funcion);

            _repository.Update(funcion);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var funcion = _repository.Get(id);
            if (funcion == null)
            {
                return NotFound();
            }

            _repository.Delete(funcion);
            return Ok();
        }

        private FuncionesDTO MapToDto(Funciones funcion)
        {
            return new FuncionesDTO
            {
                Id = funcion.Id,
                NombrePelicula = funcion.NombrePelicula,
                Horario = funcion.Horario,
                NumSala = funcion.NumSala
            };
        }

        private Funciones MapToEntity(FuncionesDTO dto, Funciones? original = null)
        {
            if (original == null)
            {
                original = new();
            }

            return new Funciones
            {
                Id = dto.Id,
                NombrePelicula = dto.NombrePelicula,
                Horario = dto.Horario,
                NumSala = dto.NumSala
            };
        }
    }
}
