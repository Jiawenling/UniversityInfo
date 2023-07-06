using Microsoft.EntityFrameworkCore;
using UniversityInfo.Models;

namespace UniversityInfo.Helpers{

    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){

        }

        public DbSet<User> User {get; set;}
        public DbSet<University> Universities {get; set;}
    }
}
