using temalabor_2021.Models;

namespace temalabor_2021.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Columns.Any())
            {
                return;
            }

            var columns = new Column[]
            {
                new Column {Name = "Üres oszlop"},
                new Column {Name = "Egy elemes oszlop"},
                new Column {Name = "Sok elemes oszlop"}
            };

            context.Columns.AddRange(columns);
            context.SaveChanges();

            var todos = new Todo[]
            {
                new Todo {ColumnID = 2, Position = 0, Name = "Demo 1", Deadline = DateTime.Parse("2021-12-06"), Description = "Column 2", State = TodoState.PendingState },
                new Todo {ColumnID = 3, Position = 0, Name = "Demo 2", Deadline = DateTime.Parse("2021-12-07"), Description = "Column 3", State = TodoState.PendingState },
                new Todo {ColumnID = 3, Position = 1, Name = "Demo 3", Deadline = DateTime.Parse("2021-12-08"), Description = "Column 3", State = TodoState.ProgressState },
                new Todo {ColumnID = 3, Position = 2, Name = "Demo 4", Deadline = DateTime.Parse("2021-12-09"), Description = "Column 3", State = TodoState.DoneState },
                new Todo {ColumnID = 3, Position = 3, Name = "Demo 5", Deadline = DateTime.Parse("2021-12-10"), Description = "Column 3", State = TodoState.PostponedState }
            };

            context.Todos.AddRange(todos);
            context.SaveChanges();
        }
    }
}

