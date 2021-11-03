using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.Repository
{
    internal interface ITodoRepository
    {
        Todo FindById(int id);
        ICollection<Todo> GetAll();
        void Update(Todo todo);
        int Insert(Todo todo);
        bool Delete(int id);
    }
}
