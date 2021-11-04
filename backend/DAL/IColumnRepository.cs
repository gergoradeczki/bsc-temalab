using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using temalabor_2021_todo_backend.Model;

namespace temalabor_2021_todo_backend.DAL
{
    public interface IColumnRepository
    {
        Column FindById(int id);
        ICollection<Column> GetAll();
        int Insert(Column column);
        bool Delete(int id);

    }
}
