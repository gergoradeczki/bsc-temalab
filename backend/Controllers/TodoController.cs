using Microsoft.AspNetCore.Mvc;
using temalabor_2021_todo_backend.DAL;
using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.Controllers
{
    [Route("api/todos")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository repo;

        public TodoController(ITodoRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IEnumerable<TodoDetailsDTO> GetAll()
        {
            return repo.GetAll();
        }
    }
}
