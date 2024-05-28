using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiCine.Helpers;
using ApiCine.Models.DTOs;
using ApiCine.Models.Entities;
using NoticiasOctavoAPI.Repositories;
using System.Security.Claims;
using Microsoft.JSInterop.Infrastructure;

namespace ApiCine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IRepository<Clientes> repository;
        private readonly JwtHelper jwtHelper;

        public LoginController(IRepository<Clientes> repository, JwtHelper jwtHelper)
        {
            this.repository = repository;
            this.jwtHelper = jwtHelper;
        }

        [HttpPost]
        public IActionResult Authenticate(LoginDTO dto)
        {
            if(dto.Usuario=="Admin" && dto.Contraseña == "admin")
            {
                var token2 = jwtHelper.GetToken("Admin", "Admin",
                new List<Claim> { new Claim("Id", "0") }
                );
                return Ok(token2);
            }

            var usuario = repository.GetAll().FirstOrDefault(x => x.Usuario == dto.Usuario && x.Contraseña == dto.Contraseña);


            if (usuario == null)
                return Unauthorized();

            var token = jwtHelper.GetToken(usuario.Nombre,"Cliente",
                new List<Claim> { new Claim("Id", usuario.Id.ToString())}
                );

            return Ok(token);
        }


    }
}
