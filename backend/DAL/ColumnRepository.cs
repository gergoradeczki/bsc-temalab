using Microsoft.EntityFrameworkCore;
using temalabor2021.Data;
using temalabor2021.Models;

namespace temalabor2021.DAL
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
            return db.Columns.Include(c => c.Todos).SingleOrDefault(c => c.ID == id);
        }

        public ICollection<ColumnDetailsDTO?> GetAll()
        {

            return db.Columns
                .Include(c => c.Todos)
                .Select(c => GetColumnDetailsDTO(c))
                .ToList();
        }

        public int Insert(Column column)
        {
            db.Columns.Add(column);
            return db.SaveChanges();
        }

        public static ColumnDTO? GetColumnDTO(Column column)
        {
            if (column == null) return null;
            return new ColumnDTO()
            {
                ID = column.ID,
                Name = column.Name
            };
        }

        public static ColumnDetailsDTO? GetColumnDetailsDTO(Column column)
        {
            if(column == null) return null;
            if(column.Todos == null) return null;
            return new ColumnDetailsDTO()
            {
                ID = column.ID,
                Name = column.Name,
                Todos = column.Todos.Select(t => TodoRepository.GetTodoDTO(t))
            };
        }
    }
}
