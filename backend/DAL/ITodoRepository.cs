using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.DAL
{
    public interface ITodoRepository
    {
        Todo? FindById(int id);
        ICollection<TodoDetailsDTO> GetAll();
        void Update(Todo todo);
        int Insert(Todo todo);
        bool Delete(int id);
        void SwapPosition(Todo t1, Todo t2);
        void MoveUp(Todo todo);
        void MoveDown(Todo todo);
    }
}
