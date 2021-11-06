using Microsoft.AspNetCore.Mvc;
using temalabor_2021_todo_backend.DAL;
using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.Controllers
{
    [Route("api/columns")]
    public class ColumnController : Controller
    {
        private readonly IColumnRepository repo;

        public ColumnController(IColumnRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IEnumerable<ColumnDetailsDTO> GetAll()
        {
            return repo.GetAll();
        }

        [HttpGet]
        [Route("{id}")] // Path: api/columns/{id}
        public IActionResult GetOne(int id)
        {
            var res = repo.FindById(id);
            if (res == null)
                return NotFound();
            return Ok(ColumnRepository.GetColumnDetailsDTO(res));
        }

        [HttpDelete]
        [Route("{id}")] // Path: api/columns/{id}
        public IActionResult Delete(int id)
        {
            return repo.Delete(id) ? NoContent() : NotFound();
        }
    }
}
