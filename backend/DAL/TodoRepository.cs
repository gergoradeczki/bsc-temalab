using Microsoft.EntityFrameworkCore;
using temalabor_2021.Data;
using temalabor_2021.Models;

namespace temalabor_2021.DAL
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
            return db.Todos.Include(t => t.Column).SingleOrDefault(t => t.ID == id);
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
            if(db.Todos.Where(t => t.ColumnID == todo.ColumnID).Select(t => t.Position).Any())
            {
                int maxPos = db.Todos.Where(t => t.ColumnID == todo.ColumnID).Select(t => t.Position).Max();
                todo.Position = maxPos + 1;
            } else
            {
                todo.Position = 0;
            }
            

            db.Todos.Add(todo);
            return db.SaveChanges();

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

        public int SwapPosition(Todo t1, Todo t2)
        {
            int tempPos = t1.Position;
            t1.Position = t2.Position;
            t2.Position = tempPos;

            return db.SaveChanges();
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
