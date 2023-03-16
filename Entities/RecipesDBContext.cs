using Microsoft.EntityFrameworkCore;

namespace RecipeFinderAPI.Entities
{
    public class RecipesDBContext : DbContext
    {
        private string _connectionString =
                 "Server=(localdb)\\localRecipeFinderDB;Database=RecipesDB;Trusted_Connection=True;";
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingridient> Ingridients { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<RecipeIngridient> RecipeIngridients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Ingridient>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Unit>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<RecipeIngridient>()
                .Property(a => a.Amount)
                .IsRequired()
                .HasPrecision(5, 2);

            modelBuilder.Entity<RecipeIngridient>()
                .Property(a => a.Description)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
