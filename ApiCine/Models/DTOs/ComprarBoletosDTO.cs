using System.ComponentModel.DataAnnotations;

namespace ApiCine.Models.DTOs
{
    public class ComprarBoletosDTO
    {
            [Required(ErrorMessage = "Seleccione una función es obligatorio.")]
            public int IdFuncion { get; set; }

            [Required(ErrorMessage = "Debe especificar al menos un número de asiento.")]
            public List<int> NumerosAsientos { get; set; } = new List<int>();
        
    }
}
