using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using temalabor_2021.DAL;
using temalabor_2021.Models;

namespace temalabor_2021.Controllers
{
    [EnableCors("policy")]
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

        [HttpPost]
        public IActionResult Create([FromBody] Column column)
        {
            return repo.Insert(column) > 0 ? Created("/api/columns/" + column.ID, column) : Forbid();
        }

        [HttpGet("{id}")] // Path: api/columns/{id}
        public IActionResult GetOne(int id)
        {
            var res = repo.FindById(id);
            if (res == null)
                return NotFound();
            return Ok(ColumnRepository.GetColumnDetailsDTO(res));
        }

        [HttpDelete("{id}")] // Path: api/columns/{id}
        public IActionResult Delete(int id)
        {
            return repo.Delete(id) ? NoContent() : NotFound();
        }
    }
}
