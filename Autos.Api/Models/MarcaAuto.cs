using System.ComponentModel.DataAnnotations;

namespace Autos.Api.Models;

public class MarcaAuto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string PaisOrigen { get; set; } = string.Empty;
}
