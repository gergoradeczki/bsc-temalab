using Microsoft.EntityFrameworkCore;
using temalabor_2021.Models;

namespace temalabor_2021.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Column> Columns => Set<Column>();
        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Column>().ToTable("Column");
            modelBuilder.Entity<Todo>().ToTable("Todo");

            modelBuilder.Entity<Column>().HasMany(column => column.Todos).WithOne();
            modelBuilder.Entity<Todo>().HasOne(todo => todo.Column).WithMany(column => column.Todos);
        }
    }
}
