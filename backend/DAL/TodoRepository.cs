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
            throw new NotImplementedException();
        }

        public Todo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Todo> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Todo todo)
        {
            throw new NotImplementedException();
        }

        public void Update(Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}
