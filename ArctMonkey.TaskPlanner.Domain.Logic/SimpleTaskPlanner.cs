using ClassLibrary1.Domain.Models;

namespace TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] workItems)
        {
            return workItems
                .OrderByDescending(w => w.Priority)
                .ThenBy(w => w.DueDate)
                .ThenBy(w => w.Title)
                .ToArray();
        }
    }
}