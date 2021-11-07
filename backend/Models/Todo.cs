using System.ComponentModel.DataAnnotations.Schema;

namespace temalabor_2021.Models
{
    public enum TodoState
    {
        PendingState,
        ProgressState,
        DoneState,
        PostponedState
    }
    public class Todo
    {
        public int ID { get; set; }
        public int? ColumnID { get; set; }
        public int Position { get; set; }
        public string? Name { get; set; }
        public DateTime Deadline { get; set; }
        public string? Description { get; set; }
        public TodoState State { get; set; }

        public virtual Column Column { get; set; } = null!;
    }

    public class TodoDTO
    {
        public int ID { get; set; }
        public int Position { get; set; }
        public string? Name { get; set; }
        public DateTime Deadline { get; set; }
        public string? Description { get; set; }
        public TodoState State { get; set; }
    }

    public class TodoDetailsDTO
    {
        public int ID { get; set; }
        public int Position { get; set; }
        public string? Name { get; set; }
        public DateTime Deadline { get; set; }
        public string? Description { get; set; }
        public TodoState State { get; set; }

        public ColumnDTO Column { get; set; } = null!;
    }
}
