using Microsoft.AspNetCore.Mvc;
using temalabor_2021_todo_backend.DAL;
using temalabor_2021_todo_backend.Data;
using temalabor_2021_todo_backend.Model;

namespace temalabor_2021_todo_backend.Controllers
{
    [Route("api/todos")]
    public class TodoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly TodoRepository _repo;

        public TodoController(AppDbContext context)
        {
            _context = context;
            _repo = new TodoRepository(_context);
        }

        [HttpGet]
        public IEnumerable<TodoDetailsDTO> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
