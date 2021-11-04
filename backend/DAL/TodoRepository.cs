using Microsoft.EntityFrameworkCore;
using temalabor_2021_todo_backend.Model;

namespace temalabor_2021_todo_backend.DAL
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly string connectionString;

        public TodoRepository(string connectionString)
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
            using (var db = createDbContext())
            {
                var toDelete = db.Todos.Where(t => t.ID == id).SingleOrDefault();
                if(toDelete != null)
                    db.Todos.Remove(toDelete);
                return db.SaveChanges() > 0;
            }
        }

        public Todo? FindById(int id)
        {
            using (var db = createDbContext())
            {
                return db.Todos.FirstOrDefault(t => t.ID == id);
            }
        }

        public ICollection<Todo> GetAll()
        {
            using (var db = createDbContext())
            {
                return db.Todos.Include(t => t.Column).ToList();
            }
        }

        public int Insert(Todo todo)
        {
            using (var db = createDbContext())
            {
                db.Todos.Add(todo);
                return db.SaveChanges();
            }
        }

        public void Update(Todo todo)
        {
            throw new NotImplementedException();
        }

        public void SwapPosition(Todo t1, Todo t2)
        {
            using (var db = createDbContext())
            {
                int tempPos = t1.Position;
                t1.Position = t2.Position;
                t2.Position = tempPos;

                db.SaveChanges();
            }
        }

        public void MoveUp(Todo todo)
        {
            throw new NotImplementedException();
        }

        public void MoveDown(Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}
