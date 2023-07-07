using System.ComponentModel.DataAnnotations;
namespace UniversityInfo.Models
{
    public class University
    {
        public Guid Id {get; set;}
        public bool IsBookMarked {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string Country {get; set;}
        [Required]
        public string Webpages {get; set;}
        public DateTime Created {get; set;}
        public DateTime LastModified {get; set;}
        public DateTime DeletedAt {get; set;}
        [Required]
        public bool IsActive {get; set;}
    }
}