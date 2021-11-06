using Microsoft.AspNetCore.Mvc;
using temalabor_2021_todo_backend.DAL;
using temalabor_2021_todo_backend.Data;
using temalabor_2021_todo_backend.Models;

namespace temalabor_2021_todo_backend.Controllers
{

    [Route("api/columns")]
    public class ColumnController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ColumnRepository _repo;

        public ColumnController(AppDbContext context)
        {
            _context = context;
            _repo = new ColumnRepository(_context);
        }

        [HttpGet]
        public IEnumerable<ColumnDetailsDTO> GetColumns()
        {
            return _repo.GetAll();
        }
    }
}
