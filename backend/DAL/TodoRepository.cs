using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using temalabor2021.Data;
using temalabor2021.Models;

namespace temalabor2021.DAL
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

        public ICollection<TodoDetailsDTO?> GetAll()
        {
            return db.Todos
                .Include(t => t.Column)
                .Select(t => GetTodoDetailsDTO(t))
                .ToList();
        }

        public int Insert(Todo todo)
        {
            if(todo == null) return 0;
            // ID ellenőrzés: null kell hogy legyen
            // ColumnID: léteznie kell egy ilyen oszlopnak
            // Position: az adott oszlopon belül nem szabad hogy ilyen létezzen ÉS max() + 1 -re kell állítani
            // State: (int) [1,5] tartományban van

            if(
                (!db.Todos.Where(t => t.ID == todo.ID).Any()) &&
                db.Columns.Where(c => c.ID == todo.ColumnID).Any() &&
                !db.Todos.Where(t => t.ColumnID == todo.ColumnID && t.Position == todo.Position).Any()
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
            if( todo == null) return 0;
            var toUpdate = db.Todos.SingleOrDefault(t => t.ID == todo.ID);
            if(toUpdate != null && 
                db.Columns.Where(c => c.ID == todo.ColumnID).Any())
            {
                toUpdate.ColumnID = todo.ColumnID;
                toUpdate.Position = todo.Position;
                toUpdate.Name = todo.Name;
                toUpdate.Deadline = todo.Deadline;
                toUpdate.Description = todo.Description;
                toUpdate.State = todo.State;


                return db.SaveChanges();
            }
            return 0;
        }

        public int SwapPosition(Todo t1, Todo t2)
        {
            if(t1 == null || t2 == null) return 0;
            if(t1.ColumnID == t2.ColumnID)
            {
                int tempPos = t1.Position;
                t1.Position = t2.Position;
                t2.Position = tempPos;
            }

            return db.SaveChanges();
        }

        public static TodoDTO? GetTodoDTO(Todo todo)
        {
            if(todo == null) return null;
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

        public static TodoDetailsDTO? GetTodoDetailsDTO(Todo todo)
        {
            if(todo == null) return null;
            var columnDTO = ColumnRepository.GetColumnDTO(todo.Column);
            return new TodoDetailsDTO()
            {
                ID = todo.ID,
                Position = todo.Position,
                Name = todo.Name,
                Deadline = todo.Deadline,
                Description = todo.Description,
                State = todo.State,
                Column = columnDTO ?? new ColumnDTO()
            };
        }
    }
}
