using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Helpers;
using ProjektProgramowanie.Model;

namespace ProjektProgramowanie
{
    public class CarListDbContext : DbContext
    {
        public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Car { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<CarPart> CarPart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasKey(x => x.Id);
            modelBuilder.Entity<Permission>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<CarPart>().HasKey(x => x.Id);
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = PermissionEnum.Admin });
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 2, Name = PermissionEnum.User });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, BirthDate = new System.DateTime(2000, 5, 4), Email = "kw@poczta.pl", Name = "Krzysztof", NIP = 123456879, PermissionsId = 1, Surename = "Wróblewski", Password = "haslo123" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
