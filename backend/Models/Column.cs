using System.Collections.Generic;

namespace temalabor2021.Models
{
    public class Column
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Todo>? Todos { get; }
    }

    public class ColumnDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }

    public class ColumnDetailsDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public IEnumerable<TodoDTO?>? Todos { get; set; }
    }
}
