using Microsoft.EntityFrameworkCore;

namespace Lesson35.WebAPI.Data
{
    public class PersonsDbContext:DbContext
    {
        public PersonsDbContext(DbContextOptions<PersonsDbContext> options):base(options)
        {
                
        }
        public DbSet<Persons> Persons { get; set; }
        public PersonsDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=PersonsDB;Trusted_Connection=True;");
        }


    }
}
