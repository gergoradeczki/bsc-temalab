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
        public IEnumerable<ColumnDetailsDTO> GetColumns()
        {
            return repo.GetAll();
        }
    }
}
