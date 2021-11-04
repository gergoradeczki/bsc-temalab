using Microsoft.EntityFrameworkCore;
using temalabor_2021_todo_backend.Model;

namespace temalabor_2021_todo_backend.DAL
{
    internal class ColumnRepository : IColumnRepository
    {
        private readonly string connectionString;

        public ColumnRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private AppDbContext createDbContext()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            contextOptionsBuilder.UseSqlServer(connectionString);
            return new AppDbContext(contextOptionsBuilder.Options);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Column FindById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Column> GetAll()
        {
            using(var context = createDbContext())
            {
                return context.Columns.Include(c => c.Todos).ToList();
            }
        }

        public int Insert(Column column)
        {
            throw new NotImplementedException();
        }
    }
}
