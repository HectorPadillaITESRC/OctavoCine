namespace ApiCine.Models.DTOs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    namespace ApiCine.DTOs
    {
        public class ClienteDTO
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "El nombre es requerido.")]
            public string Nombre { get; set; } = "";

            [Required(ErrorMessage = "El nombre de usuario es requerido.")]
            public string Usuario { get; set; } = "";

            [Required(ErrorMessage = "La contraseña es requerida.")]
            public string Contraseña { get; set; } = "";
        }
    }

}
