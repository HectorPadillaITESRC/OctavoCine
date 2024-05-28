namespace ApiCine.Models.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class FuncionesDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la película es obligatorio.")]
        public string NombrePelicula { get; set; } = "";

        [Required(ErrorMessage = "El horario es obligatorio.")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "El formato del horario debe ser HH:MM.")]
        public string Horario { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "El número de sala debe ser mayor que cero.")]
        public int NumSala { get; set; }

        public bool EsNuevo { get; set; }
    }

}
