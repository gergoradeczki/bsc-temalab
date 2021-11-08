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
            // ID ellenőrzés: null kell hogy legyen
            // ColumnID: léteznie kell egy ilyen oszlopnak
            // Position: az adott oszlopon belül nem szabad hogy ilyen létezzen ÉS max() + 1 -re kell állítani
            // State: (int) [1,5] tartományban van

            if(
                (!db.Todos.Where(t => t.ID == todo.ID).Any()) &&
                todo.ColumnID != null && db.Columns.Where(c => c.ID == todo.ColumnID).Any() &&
                !db.Todos.Where(t => t.ColumnID == todo.ColumnID && t.Position == todo.Position).Any() &&
                (int)todo.State >= (int)TodoState.PendingState && (int)todo.State <= (int)TodoState.PostponedState
                )
            {
                if (db.Todos.Where(t => t.ColumnID == todo.ColumnID).Select(t => t.Position).Any())
                    todo.Position = 1 + db.Todos.Where(t => t.ColumnID == todo.ColumnID).Select(t => t.Position).Max();
                else
                    todo.Position = 0;
                db.Todos.Add(todo);
            }
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
            if(t1.ColumnID == t2.ColumnID)
            {
                int tempPos = t1.Position;
                t1.Position = t2.Position;
                t2.Position = tempPos;
            }

            return db.SaveChanges();
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
