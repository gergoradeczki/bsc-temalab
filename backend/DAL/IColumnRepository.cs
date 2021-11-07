using temalabor_2021.Models;

namespace temalabor_2021.DAL
{
    public interface IColumnRepository
    {
        Column? FindById(int id);
        ICollection<ColumnDetailsDTO> GetAll();
        int Insert(Column column);
        bool Delete(int id);

    }
}
