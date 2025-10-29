using ClassLibrary1.Domain.Models;
using ClassLibrary1.Domain.Models.Enums;
using System;
using System.Globalization;
using TaskPlanner.Domain.Logic;

namespace TaskPlanner.App
{
    internal static class Launcher
    {
        public static object TaskManager { get; private set; }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "★ Task Planner Console Application ★";
            Console.ForegroundColor = ConsoleColor.Cyan;

            PrintHeader();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("→ Enter the number of tasks to create: ");
            string userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out int totalTasks) || totalTasks <= 0)
            {
                ShowError("Invalid number! Please enter a positive integer.");
                return;
            }

            WorkItem[] tasks = new WorkItem[totalTasks];
            Console.Clear();
            Console.WriteLine();
            PrintSubHeader($"Task Creation Mode ({totalTasks} tasks)");

            for (int i = 0; i < totalTasks; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n╔══════════════════════════════════════╗");
                Console.WriteLine($"║ Creating task #{i + 1} of {totalTasks,-20}║");
                Console.WriteLine($"╚══════════════════════════════════════╝");
                Console.ResetColor();

                WorkItem task = new WorkItem();

                Console.Write("📝 Title: ");
                task.Title = Console.ReadLine();

                Console.Write("📅 Due date (dd.MM.yyyy): ");
                string dateInput = Console.ReadLine();

                try
                {
                    task.DueDate = DateTime.ParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ShowError("Invalid date format! Using today’s date instead.");
                    task.DueDate = DateTime.Today;
                }

                Console.WriteLine("🔥 Priority (1 – None, 2 – Low, 3 – Medium, 4 – High, 5 – Urgent): ");
                Console.Write("→ ");
                if (int.TryParse(Console.ReadLine(), out int p) && p >= 1 && p <= 5)
                {
                    task.Priority = (Priority)(p - 1);
                }
                else
                {
                    ShowError("Invalid priority! Default set to 'None'.");
                    task.Priority = Priority.None;
                }

                tasks[i] = task;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✔ Task added successfully!");
                Console.ResetColor();
            }

            Console.Clear();
            PrintSubHeader("📋 Sorted Task List");

            SimpleTaskPlanner planner = new SimpleTaskPlanner();
            WorkItem[] sorted = planner.CreatePlan(tasks);

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var item in sorted)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine($"• Title: {item.Title}");
                Console.WriteLine($"• Due Date: {item.DueDate:dd.MM.yyyy}");
                Console.WriteLine($"• Priority: {item.Priority}");
            }

            Console.ResetColor();
            Console.WriteLine("\n===========================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   Thank you for using Task Planner!");
            Console.WriteLine("===========================================\n");
            Console.ResetColor();
        }

        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===========================================");
            Console.WriteLine("         🧭 SIMPLE TASK PLANNER v2.0       ");
            Console.WriteLine("===========================================\n");
            Console.ResetColor();
        }

        private static void PrintSubHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("===========================================");
            Console.WriteLine($"   {title}");
            Console.WriteLine("===========================================\n");
            Console.ResetColor();
        }

        private static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"⚠ {message}");
            Console.ResetColor();
        }
    }
}
