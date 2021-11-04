using temalabor_2021_todo_backend.Model;

namespace temalabor_2021_todo_backend
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Columns.Any())
            {
                return;   // DB has been seeded
            }

            var columns = new Column[]
            {
                new Column {Name = "Első oszlop"},
                new Column {Name = "Második oszlop"},
                new Column {Name = "Harmadik oszlop"}
            };

            foreach(Column column in columns)
            {
                context.Columns.Add(column);
            }
            context.SaveChanges();

            var todos = new Todo[]
            {
                new Todo {ColumnID = 1, Position = 0, Name = "Demo 1", Deadline = DateTime.Parse("2020-11-01"), Description = "Demo desc", State = TodoState.PendingState },
                new Todo {ColumnID = 2, Position = 0, Name = "Demo 2", Deadline = DateTime.Parse("2020-11-10"), Description = "Demo desc", State = TodoState.PendingState },
                new Todo {ColumnID = 3, Position = 0, Name = "Demo 3", Deadline = DateTime.Parse("2020-11-20"), Description = "Demo desc", State = TodoState.PendingState }
            };

            foreach(Todo todo in todos)
            {
                context.Todos.Add(todo);
            }

            context.SaveChanges();
        }
    }
}

