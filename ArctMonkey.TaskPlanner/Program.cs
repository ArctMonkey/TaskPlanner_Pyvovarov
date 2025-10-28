using ClassLibrary1.Domain.Models;
using ClassLibrary1.Domain.Models.Enums;
namespace TaskPlanner.Domain.Logic;

internal static class Program
{
    public static object SimpleTaskPlanner { get; private set; }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Введіть кількість задач: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int amount))
        {
            WorkItem[] workItems = new WorkItem[amount];
            for (int i = 0; i < amount; i++)
            {
                WorkItem workItem = new WorkItem();
                /* Console.WriteLine("Введіть дані товару:\n"); */
                Console.WriteLine("Введіть назву:\n");
                workItem.Title = Console.ReadLine();
                Console.WriteLine("Введіть дату виконання (формат: dd.mm.yyyy):\n");
                string dataTime = Console.ReadLine();
                workItem.DueDate = DateTime.ParseExact(
                    dataTime,
                    "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture
                ); Console.WriteLine("Введіть пріоритет об'єкта (1 – Немає, 2 – Низький, 3 – Середній, 4 – Високий, 5 – Терміново):\t");
                int k = int.Parse(Console.ReadLine()) - 1;
                workItem.Priority = (Priority)k;
                workItems[i] = workItem;
            }
            SimpleTaskPlanner simpleTaskPlanner = new SimpleTaskPlanner();
            WorkItem[] sortedItems = simpleTaskPlanner.CreatePlan(workItems);
            foreach (WorkItem item in sortedItems)
            {
                Console.WriteLine(item.ToString());
            }
        }
        else
        {
            Console.WriteLine("Помилка: потрібно ввести число!");
        }
    }
}