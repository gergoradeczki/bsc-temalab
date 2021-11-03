using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temalabor_2021_todo_backend.Models
{
    public class Column
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public ICollection<Todo>? Todos { get; set; }
    }
}
