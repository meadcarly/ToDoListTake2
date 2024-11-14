namespace ToDoListTake2;

public class Prompts
{
    public static void WelcomeMenu()
    {
        Console.WriteLine("Welcome to ToDo, where we help you organize your priorities! Tell me where you would like to start...");
        Console.WriteLine();
        Console.WriteLine("1. View your ToDo List");
        Console.WriteLine("2. Add a new task to your ToDo List");
        Console.WriteLine("3. Update a task: add a day you plan to tackle the task! Hopefully it's a fun one!");
        Console.WriteLine("4. Update a task status on your ToDo List");
        Console.WriteLine("5. Delete a task from your ToDo List");
        Console.WriteLine("6. Exit");
    }
}