using System.Collections.Generic;
using System.Reflection.Emit;
using TasteAndRateAPI.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TasteAndRateAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
        //Add models here
       
        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<GastroEntity> Gastro { get; set; }
        public DbSet<ValoracionEntity> Valoracion { get; set; }
        public DbSet<CriterioEntity> Criterio { get; set; }
        public DbSet<ValoracionCriterioEntity> ValoracionCriterio { get; set; }

        public DbSet<EtiquetaEntity> Etiqueta { get; set; }

    }
}