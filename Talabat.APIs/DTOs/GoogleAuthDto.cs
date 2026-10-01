using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs;

public class GoogleAuthDto
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
}
