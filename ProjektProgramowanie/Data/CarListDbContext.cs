using Microsoft.EntityFrameworkCore;

namespace ProjektProgramowanie
{
    public class CarListDbContext : DbContext
    {
        public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> CarList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasKey(x => x.CarId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
