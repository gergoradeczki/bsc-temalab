using Microsoft.Data.SqlClient;
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
            {
                int pos = toDelete.Position;
                db.Todos.Remove(toDelete);

                foreach(var todo in db.Todos)
                {
                    if (todo.Position > pos)
                        todo.Position -= 1;
                }
            }
            return db.SaveChanges() > 0;
        }

        public Todo? FindById(int id)
        {
            return db.Todos.Include(t => t.Column).FirstOrDefault(t => t.ID == id);
        }

        public ICollection<TodoDetailsDTO> GetAll()
        {
            return db.Todos
                .Include(t => t.Column)
                .Select(t => GetTodoDetailsDTO(t))
                .ToList();
        }

        public int Insert(Todo todo)
        {
            var p1Todo = db.Todos.FirstOrDefault(t => t.ID == todo.ID);
            var p2Todo = db.Todos.FirstOrDefault(t => t.Position == todo.Position && t.ColumnID == todo.ColumnID);
            var pColumn = db.Columns.FirstOrDefault(c => c.ID == todo.ColumnID);

            if (p1Todo == null && p2Todo == null && pColumn != null)
            {
                try
                {
                    db.Todos.Add(todo);
                    return db.SaveChanges();
                } catch (Exception ex)
                {
                    return 0;
                }
            }
            return 0;
        }

        public int Update(Todo todo)
        {
            var toUpdate = db.Todos.SingleOrDefault(t => t.ID == todo.ID);
            if(toUpdate != null)
            {
                toUpdate.ColumnID = todo.ColumnID;
                toUpdate.Position = todo.Position;
                toUpdate.Name = todo.Name;
                toUpdate.Deadline = todo.Deadline;
                toUpdate.Description = todo.Description;
                toUpdate.State = todo.State;
            }
            return db.SaveChanges();
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

        public static TodoDTO GetTodoDTO(Todo todo)
        {
            return new TodoDTO()
            {
                ID = todo.ID,
                Position = todo.Position,
                Name = todo.Name,
                Deadline = todo.Deadline,
                Description = todo.Description,
                State = todo.State
            };
        }

        public static TodoDetailsDTO GetTodoDetailsDTO(Todo todo)
        {
            return new TodoDetailsDTO()
            {
                ID = todo.ID,
                Position = todo.Position,
                Name = todo.Name,
                Deadline = todo.Deadline,
                Description = todo.Description,
                State = todo.State,
                Column = ColumnRepository.GetColumnDTO(todo.Column)
            };
        }
    }
}
