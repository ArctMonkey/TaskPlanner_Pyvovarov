using ClassLibrary1.Domain.Models.Enums;


namespace ClassLibrary1.Domain.Models
{

    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplited { get; set; }
        public override string ToString()
        {
            return $"{Title} {DueDate:dd.MM.yyyy} {Priority.ToString().ToLower()}\n";
        }

    }
}