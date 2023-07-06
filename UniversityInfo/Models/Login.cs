using System.ComponentModel.DataAnnotations;

namespace UniversityInfo.Models
{
    public class Login
    {
        [Required]
        public string? Username {get; set;}
        [Required]
        public string? Password {get; set;}
    }
}