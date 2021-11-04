namespace temalabor_2021_todo_backend.Model
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
        public int ColumnID { get; set; }
        public int Position { get; set; }
        public string? Name { get; set; }
        public DateTime Deadline { get; set; }
        public string? Description { get; set; }
        public TodoState State { get; set; }
        
        public Column? Column { get; set; }
    }
}
