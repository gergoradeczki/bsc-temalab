using Microsoft.EntityFrameworkCore;
using System.Linq;
using temalabor_2021.Controllers;
using temalabor_2021.DAL;
using temalabor_2021.Data;
using Xunit;

namespace temalabor_2021.Tests
{
    public class SqliteTodoControllerTest : ControllerTest
    {
        public SqliteTodoControllerTest() : base(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Filename=Test.db")
            .Options)
        {
        }

        [Fact]
        public void CanGetAllTodoItems()
        {
            using (var context = new AppDbContext(ContextOptions))
            {
                var repo = new TodoRepository(context);
                var controller = new TodoController(repo);

                var items = controller.GetAll();

                Assert.Equal(5, items.ToList().Count);
            }
        }
    }
}
