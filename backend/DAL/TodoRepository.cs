using Microsoft.EntityFrameworkCore;
using temalabor_2021_todo_backend.Data;
using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.DAL
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext db;

        public TodoRepository(AppDbContext db)
        {
            this.db = db;
        }

        public bool Delete(int id)
        {
            var toDelete = db.Todos.Where(t => t.ID == id).SingleOrDefault();
            if(toDelete != null)
                db.Todos.Remove(toDelete);
            return db.SaveChanges() > 0;
        }

        public Todo? FindById(int id)
        {
            return db.Todos.FirstOrDefault(t => t.ID == id);
        }

        public ICollection<TodoDetailsDTO> GetAll()
        {
            return db.Todos
                .Include(t => t.Column)
                .Select(t => new TodoDetailsDTO()
                {
                    ID = t.ID,
                    Position = t.Position,
                    Name = t.Name,
                    Deadline = t.Deadline,
                    Description = t.Description,
                    State = t.State,
                    Column = new ColumnDTO()
                    {
                        ID = t.Column.ID,
                        Name = t.Column.Name
                    }
                })
                .ToList();
        }

        public int Insert(Todo todo)
        {
            db.Todos.Add(todo);
            return db.SaveChanges();
        }

        public void Update(Todo todo)
        {
            var toUpdate = db.Todos.SingleOrDefault(t => t.ID == todo.ID);
            if(toUpdate != null)
            {
                toUpdate.Position = todo.Position;
                toUpdate.Name = todo.Name;
                toUpdate.Deadline = todo.Deadline;
                toUpdate.Description = todo.Description;
                toUpdate.State = todo.State;
            }
        }

        public void SwapPosition(Todo t1, Todo t2)
        {
            int tempPos = t1.Position;
            t1.Position = t2.Position;
            t2.Position = tempPos;

            db.SaveChanges();
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
