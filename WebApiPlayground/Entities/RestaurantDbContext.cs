using Microsoft.EntityFrameworkCore;

namespace WebApiPlayground.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private readonly string _connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Restaurants;Integrated Security=true";

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Restaurant>()
                .HasOne(x => x.Address)
                .WithOne(x => x.Restaurant)
                .HasForeignKey<Address>(x => x.RestaurantId);

            modelBuilder.Entity<Dish>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(x => x.City)
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(x => x.Street)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
               .Property(x => x.Name)
               .IsRequired();
        }
    }
}