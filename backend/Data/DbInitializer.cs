using System;
using System.Linq;
using temalabor2021.Models;

namespace temalabor2021.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context == null)
                return;

            context.Database.EnsureCreated();

            if (context.Columns.Any())
                return;

            var columns = new Column[]
            {
                new Column {Name = "Egy oszlop"},
                new Column {Name = "Másik oszlop"}
            };

            context.Columns.AddRange(columns);
            context.SaveChanges();

            var todos = new Todo[]
            {
                new Todo
                {
                    ColumnID = columns[0].ID,
                    Position = 0,
                    Name = "Függőben",
                    Description = "Valamilyen leírás",
                    Deadline = new DateTime(2021, 12, 10, 23, 59, 59),
                    State = TodoState.PendingState
                },
                new Todo
                {
                    ColumnID = columns[0].ID,
                    Position = 1,
                    Name = "Folyamatban",
                    Description = "Valamilyen leírás",
                    Deadline = new DateTime(2021, 12, 10, 23, 59, 59),
                    State = TodoState.ProgressState
                },
                new Todo
                {
                    ColumnID = columns[0].ID,
                    Position = 2,
                    Name = "Kész",
                    Description = "Valamilyen leírás",
                    Deadline = new DateTime(2021, 12, 10, 23, 59, 59),
                    State = TodoState.DoneState
                },
                new Todo
                {
                    ColumnID = columns[0].ID,
                    Position = 3,
                    Name = "Elhalasztva",
                    Description = "Valamilyen leírás",
                    Deadline = new DateTime(2021, 12, 10, 23, 59, 59),
                    State = TodoState.PostponedState
                }
            };

            context.Todos.AddRange(todos);
            context.SaveChanges();
        }
    }
}

