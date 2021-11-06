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

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            return repo.Insert(todo) > 0 ? Created("/api/todos/", todo) : Forbid();
        }

        [HttpPut]
        public IActionResult Update(Todo todo)
        {
            return repo.Update(todo) > 0 ? Created("/api/todos/", todo) : Forbid();
        }

        [HttpGet]
        [Route("{id}")] // Path: api/todos/{id}
        public IActionResult GetOne(int id)
        {
            var res = repo.FindById(id);
            if (res == null)
                return NotFound();
            return Ok(TodoRepository.GetTodoDetailsDTO(res));
        }

        [HttpDelete]
        [Route("{id}")] // Path: api/todos/{id}
        public IActionResult Delete(int id)
        {
            return repo.Delete(id) ? NoContent() : NotFound();
        }
    }
}
