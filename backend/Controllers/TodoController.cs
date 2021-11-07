using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using temalabor_2021_todo_backend.DAL;
using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.Controllers
{
    [EnableCors("policy")]
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
        public IActionResult Create([FromBody] Todo todo)
        {
            return repo.Insert(todo) > 0 ? Created("/api/todos/" + todo.ID, todo) : Forbid();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Todo todo)
        {
            return repo.Update(todo) > 0 ? Ok() : NoContent();
        }

        [HttpGet("{id}")] // Path: api/todos/{id}
        public IActionResult GetOne(int id)
        {
            var res = repo.FindById(id);
            if (res == null)
                return NotFound();
            return Ok(TodoRepository.GetTodoDetailsDTO(res));
        }

        [HttpDelete("{id}")] // Path: api/todos/{id}
        public IActionResult Delete(int id)
        {
            return repo.Delete(id) ? NoContent() : NotFound();
        }

        [HttpPut("swap")] // Path: api/todos/swap
        public IActionResult SwapTodos([FromBody] SwapClass sc)
        {
            var t1 = repo.FindById(sc.a);
            var t2 = repo.FindById(sc.b);

            if(t1 != null && t2 != null)
            {
                return repo.SwapPosition(t1, t2) > 0 ? Ok() : NoContent();
            }
            return NoContent();
        }

        public class SwapClass
        {
            public int a { get; set; }
            public int b { get; set; }
        }
    }
}
