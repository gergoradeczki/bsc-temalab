using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using temalabor2021.DAL;
using temalabor2021.Models;

namespace temalabor2021.Controllers
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
        public IEnumerable<TodoDetailsDTO?> GetAll()
        {
            return repo.GetAll();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Todo todo)
        {
            if (todo == null) return BadRequest();
            return repo.Insert(todo) > 0 ? Created(new Uri(Request.Host + "/api/todos/" + todo.ID), todo) : BadRequest();
        }

        [HttpPut("{id}")] // Path: api/todos/{id}
        public IActionResult Update(int id, [FromBody] Todo todo)
        {
            var t = repo.FindById(id);
            if(t == null) return NotFound();
            return repo.Update(todo) > 0 ? Ok() : BadRequest();
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
        public IActionResult SwapTodos([FromBody] SwapDTO sc)
        {
            if(sc == null) return BadRequest();
            var t1 = repo.FindById(sc.A);
            var t2 = repo.FindById(sc.B);

            if(t1 != null && t2 != null)
            {
                return repo.SwapPosition(t1, t2) > 0 ? Ok() : BadRequest();
            }
            return BadRequest();
        }

        
    }
}
