namespace UniversityInfo.Models
{
    public class University
    {
        public Guid Id {get; set;}
        public bool IsBookMarked {get; set;}
        public string Name {get; set;}
        public string Country {get; set;}
        public string Webpages {get; set;}
        public DateTime Created {get; set;}
        public DateTime LastModified {get; set;}
        public DateTime DeletedAt {get; set;}
        public bool IsActive {get; set;}
    }
}