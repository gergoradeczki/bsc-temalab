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
            using(var db = createDbContext())
            {
                var toDelete = db.Columns.Where(c => c.ID == id).SingleOrDefault();
                if(toDelete != null)
                    db.Columns.Remove(toDelete);
                return db.SaveChanges() > 0;
            }
        }

        public Column? FindById(int id)
        {
            using (var db = createDbContext())
            {
                return db.Columns.SingleOrDefault(c => c.ID == id);
            }
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
            using (var db = createDbContext())
            {
                db.Columns.Add(column);
                return db.SaveChanges();
            }
        }
    }
}
