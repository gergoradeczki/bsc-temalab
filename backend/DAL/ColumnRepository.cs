using Microsoft.EntityFrameworkCore;
using temalabor_2021_todo_backend.Data;
using temalabor_2021_todo_backend.Model;

namespace temalabor_2021_todo_backend.DAL
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly AppDbContext db;

        public ColumnRepository(AppDbContext db)
        {
            this.db = db;
        }

        public bool Delete(int id)
        {
            var toDelete = db.Columns.Where(c => c.ID == id).SingleOrDefault();
            if(toDelete != null)
                db.Columns.Remove(toDelete);
            return db.SaveChanges() > 0;
        }

        public Column? FindById(int id)
        {
            return db.Columns.SingleOrDefault(c => c.ID == id);
        }

        public ICollection<ColumnDetailsDTO> GetAll()
        {
            return db.Columns
                .Include(c => c.Todos)
                .Select(c => new ColumnDetailsDTO()
                {
                    ID = c.ID,
                    Name = c.Name,
                    Todos = c.Todos.Select(t => new TodoDTO() 
                    {
                        ID = t.ID,
                        Position = t.Position,
                        Name = t.Name,
                        Deadline = t.Deadline,
                        Description = t.Description,
                        State = t.State
                    })
                })
                .ToList();
        }

        public int Insert(Column column)
        {
            db.Columns.Add(column);
            return db.SaveChanges();
        }
    }
}
