using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.DAL
{
    public interface ITodoRepository
    {
        Todo? FindById(int id);
        ICollection<TodoDetailsDTO> GetAll();
        int Update(Todo todo);
        int Insert(Todo todo);
        bool Delete(int id);
        int SwapPosition(Todo t1, Todo t2);
        void MoveUp(Todo todo);
        void MoveDown(Todo todo);
    }
}
