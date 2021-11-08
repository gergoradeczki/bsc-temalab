using Microsoft.EntityFrameworkCore;
using System.Linq;
using temalabor2021.Controllers;
using temalabor2021.DAL;
using temalabor2021.Data;
using Xunit;

namespace temalabor2021.Tests
{
    public class SqliteTodoControllerTest : ControllerTest
    {
        public SqliteTodoControllerTest() : base(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Filename=Test.db")
            .Options)
        {
        }

        [Fact]
        public void TestTodoGetAll()
        {
            using var context = new AppDbContext(ContextOptions);

            var repo = new TodoRepository(context);

            var items = repo.GetAll();

            Assert.Equal(5, items.ToList().Count);
        }

        [Fact]
        public void TestTodoFindById()
        {
            using var context = new AppDbContext(ContextOptions);
            
            var repo = new TodoRepository(context);

            var item1 = repo.FindById(1);
            var item2 = repo.FindById(99);

            Assert.NotNull(item1);
            Assert.Equal(1, item1?.ID);
            Assert.Null(item2);
        }

        [Fact]
        public void TestTodoDelete()
        {
            using var context = new AppDbContext(ContextOptions);

            var repo = new TodoRepository(context);

            Assert.NotNull(repo.FindById(1));
            Assert.True(repo.Delete(1));

            Assert.Null(repo.FindById(99));
            Assert.False(repo.Delete(99));
        }

        [Fact]
        public void TestTodoInsert()
        {
            using var context = new AppDbContext(ContextOptions);

            var repo = new TodoRepository(context);

            var todo = new Models.Todo()
            {
                ColumnID = 1,
                Position = 0,
                Name = "Test Insert",
                Description = "This is a test item",
                Deadline = new System.DateTime(),
                State = Models.TodoState.PendingState
            };

            Assert.Equal(1, repo.Insert(todo)); // everything OK
            Assert.Equal(6, context.Todos.Count());

            todo = new Models.Todo()
            {
                ID = 1,
                ColumnID = 1,
                Position = 0,
                Name = "Test Insert",
                Description = "This is a test item",
                Deadline = new System.DateTime(),
                State = Models.TodoState.PendingState
            };

            Assert.Equal(0, repo.Insert(todo)); // already existing ID
            Assert.Equal(6, context.Todos.Count());

            todo = new Models.Todo()
            {
                ID = 99,
                ColumnID = 1,
                Position = 1,
                Name = "Test Insert",
                Description = "This is a test item",
                Deadline = new System.DateTime(),
                State = Models.TodoState.PendingState
            };

            Assert.Equal(1, repo.Insert(todo)); // not existing ID
            Assert.Equal(7, context.Todos.Count());

            todo = new Models.Todo()
            {
                Position = 2,
                Name = "Test Insert",
                Description = "This is a test item",
                Deadline = new System.DateTime(),
                State = Models.TodoState.PendingState
            };

            Assert.Equal(0, repo.Insert(todo)); // no ColumnID
            Assert.Equal(7, context.Todos.Count());

            todo = new Models.Todo()
            {
                ColumnID = 99,
                Position = 2,
                Name = "Test Insert",
                Description = "This is a test item",
                Deadline = new System.DateTime(),
                State = Models.TodoState.PendingState
            };

            Assert.Equal(0, repo.Insert(todo)); // Bad ColumnID
            Assert.Equal(7, context.Todos.Count());

            todo = new Models.Todo()
            {
                ColumnID = 1,
                Position = 2,
                Name = "Test Insert",
                Description = "This is a test item",
                Deadline = new System.DateTime()
            };

            Assert.Equal(1, repo.Insert(todo)); // no State
            Assert.Equal(Models.TodoState.PendingState, todo.State); // defaults to PendingState
            Assert.Equal(8, context.Todos.Count());

        }

        [Fact]
        public void TestTodoUpdate()
        {
            using var context = new AppDbContext(ContextOptions);

            var repo = new TodoRepository(context);

            var t = repo.FindById(1);
            Assert.NotNull(t);
            if (t == null)
                return;

            var todo = new Models.Todo()
            {
                ID = 1,
                ColumnID = 1,
                Position = 0,
                Name = "Test Update",
                Description = "This is a test update",
                Deadline = t.Deadline,
                State = Models.TodoState.DoneState
            };

            Assert.Equal(1, repo.Update(todo));
            Assert.Equal("Test Update", t.Name);
            Assert.Equal("This is a test update", t.Description);
            Assert.Equal(Models.TodoState.DoneState, t.State);
        }

        [Fact]
        public void TestTodoSwap()
        {
            using var context = new AppDbContext(ContextOptions);

            var repo = new TodoRepository(context);

            var t1 = repo.FindById(1);
            if (t1 == null)
                return;
            int pos1 = t1.Position;

            var t2 = repo.FindById(2);
            if (t2 == null)
                return;
            int pos2 = t2.Position;

            Assert.Equal(2, repo.SwapPosition(t1, t2));
            Assert.Equal(pos1, t2.Position);
            Assert.Equal(pos2, t1.Position);
            Assert.Equal(0, repo.SwapPosition(t1, t1));
        }

        [Fact]
        public void CanGetAllColumnItems()
        {
            using var context = new AppDbContext(ContextOptions);
            
            var repo = new ColumnRepository(context);

            var items = repo.GetAll();

            Assert.Equal(3, items.ToList().Count);
        }
    }
}
