using Microsoft.EntityFrameworkCore;
using temalabor_2021_todo_backend.Model;

namespace temalabor_2021_todo_backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Column> Columns { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Column>().ToTable("Column");
            modelBuilder.Entity<Todo>().ToTable("Todo");
        }
    }
}
