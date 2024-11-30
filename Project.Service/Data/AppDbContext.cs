using Project.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleModel>()
                .HasOne(m => m.VehicleMake)
                .WithMany(v => v.VehicleModels)
                .HasForeignKey(m => m.MakeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VehicleMake>().HasData(
                new VehicleMake { Id = 1, Name = "BMW", Abrv = "BMW" },
                new VehicleMake { Id = 2, Name = "Ford", Abrv = "FORD" },
                new VehicleMake { Id = 3, Name = "Volkswagen", Abrv = "VW" },
                new VehicleMake { Id = 4, Name = "Škoda", Abrv = "SKODA" },
                new VehicleMake { Id = 5, Name = "Audi", Abrv = "AUDI" },
                new VehicleMake { Id = 6, Name = "Renault", Abrv = "RENAULT" },
                new VehicleMake { Id = 7, Name = "Opel", Abrv = "OPEL" }
            );

            modelBuilder.Entity<VehicleModel>().HasData(
                new VehicleModel { Id = 1, Name = "BMW 128", Abrv = "128", MakeId = 1 },
                new VehicleModel { Id = 2, Name = "BMW 325", Abrv = "325", MakeId = 1 },
                new VehicleModel { Id = 3, Name = "Ford Focus", Abrv = "Focus", MakeId = 2 },
                new VehicleModel { Id = 4, Name = "VW Tiguan", Abrv = "Tiguan", MakeId = 3 },
                new VehicleModel { Id = 5, Name = "Škoda Octavia", Abrv = "Octavia", MakeId = 4 },
                new VehicleModel { Id = 6, Name = "Renault Clio", Abrv = "Clio", MakeId = 6 }
            );
        }
    }
}
