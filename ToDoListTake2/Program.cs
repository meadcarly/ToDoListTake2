using System.Data;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ToDoListTake2;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connString = config.GetConnectionString("DefaultConnection");

        IDbConnection conn = new MySqlConnection(connString);

        var itemsInToDoList = new DapperToDoListRepository(conn);
        bool running = true;
        int userInput;
        var userAnswer = "";
        string yesOrNo = "";

        while (running)
        {
            Console.WriteLine("Welcome to ToDo, where we help you organize your priorities! Tell me where you would like to start...");
            Console.WriteLine();
            Console.WriteLine("1. View your ToDo List");
            Console.WriteLine("2. Update a task status on your ToDo List");
            Console.WriteLine("3. Update a task: add a day you plan to tackle the task!");
            Console.WriteLine("4. Add a new task to your ToDo List");
            Console.WriteLine("5. Delete a task from your ToDo List");
            Console.WriteLine("6. Exit");

            int.TryParse(Console.ReadLine(), out userInput);
            while (userInput == null || userInput < 1 || userInput > 6)
            {
                Console.WriteLine("I'm sorry, that was not one of our options. Please type the number of the option you would like.");
                int.TryParse(Console.ReadLine(), out userInput);
            }

            switch (userInput)
            {
                case 1:
                    var items = itemsInToDoList.ListAllToDoItems();
                    foreach (var item in items)
                    {
                        Console.WriteLine($"id: {item.id} | task: {item.task} | status: {item.status} | scheduled_for: {item.scheduled_for}");
                    }
                    while (itemsInToDoList.ListAllToDoItems().Count() == 0)
                    {
                        Console.WriteLine("Your ToDo List is currently empty!");
                        Console.WriteLine("Would you like to add a task? Yes/No");
                        yesOrNo = Console.ReadLine().ToLower();
                        while (yesOrNo != "yes" && yesOrNo != "no")
                        {
                            Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                            yesOrNo = Console.ReadLine().ToLower();
                        }

                        while (yesOrNo == "yes")
                        {
                            Console.WriteLine("Please enter the task you want to add to your ToDo List");
                            userAnswer = Console.ReadLine();
                            Console.WriteLine("The status of your new task will start off as 'pending'");
                            Console.WriteLine("For the scheduled_for section, you may write a day you plan to complete the task, or you may write 'n/a'");
                            var day = Console.ReadLine();
                            itemsInToDoList.AddItem(userAnswer, "pending", day);
                            Console.WriteLine("Your task was added to your ToDo List!");
                            Console.WriteLine("Would you like to add another task? Yes/No");
                            yesOrNo = Console.ReadLine().ToLower();
                        }

                        if (yesOrNo == "no")
                        {
                            break;
                        }

                    }

                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                case 2:
                    var taskId = 0;
                    Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id or type '0' to reference the list'");
                    while (!int.TryParse(Console.ReadLine(), out taskId))
                    {
                        Console.WriteLine("Oops, you must enter a numeric value...");
                        int.TryParse(Console.ReadLine(), out taskId);
                    }

                    while (taskId == 0)
                    {
                        items = itemsInToDoList.ListAllToDoItems();
                        foreach (var item in items)
                        {
                            Console.WriteLine($"id: {item.id} | task: {item.task} | status: {item.status} | scheduled_for: {item.scheduled_for}");
                        }
                        Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id.");
                        while (!int.TryParse(Console.ReadLine(), out taskId))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out taskId);
                        }
                    }

                    Console.WriteLine("Ok, please enter an updated status for your task.");
                    var updatedStatus = Console.ReadLine();
                    itemsInToDoList.UpdateTaskStatus(taskId, updatedStatus);
                    Console.WriteLine("Your status has been updated!");
                    Console.WriteLine("Would you like to update another item? Yes/No");
                    yesOrNo = Console.ReadLine().ToLower();
                    while (yesOrNo != "yes" && yesOrNo != "no")
                    {
                        Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                        yesOrNo = Console.ReadLine().ToLower();
                    }
                    if (yesOrNo == "yes")
                    {
                        Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id or type '0' to reference the list'");
                        while (!int.TryParse(Console.ReadLine(), out userInput))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out userInput);
                        }
                        while (userInput == 0)
                        {
                            itemsInToDoList.ListAllToDoItems();
                            Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id.");
                            while (!int.TryParse(Console.ReadLine(), out userInput))
                            {
                                Console.WriteLine("Oops, you must enter a numeric value...");
                                int.TryParse(Console.ReadLine(), out userInput);
                            }
                        }

                        Console.WriteLine("Ok, please enter an updated status for your task.");
                        userAnswer = Console.ReadLine();
                        itemsInToDoList.UpdateTaskStatus(userInput, userAnswer);
                        Console.WriteLine("Your status has been updated!");
                        Console.WriteLine("Would you like to update another item? Yes/No");
                        yesOrNo = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        break;
                    }

                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id or type '0' to reference the list'");
                    while (!int.TryParse(Console.ReadLine(), out taskId))
                    {
                        Console.WriteLine("Oops, you must enter a numeric value...");
                        int.TryParse(Console.ReadLine(), out taskId);
                    }

                    while (taskId == 0)
                    {
                        items = itemsInToDoList.ListAllToDoItems();
                        foreach (var item in items)
                        {
                            Console.WriteLine($"id: {item.id} | task: {item.task} | status: {item.status} | scheduled_for: {item.scheduled_for}");
                        }
                        Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id.");
                        while (!int.TryParse(Console.ReadLine(), out taskId))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out taskId);
                        }
                    }

                    Console.WriteLine("Ok, please enter the day you plan to accomplish your task. (Example: Monday)");
                    var updatedDayOfWeek = Console.ReadLine();
                    itemsInToDoList.UpdateTaskScheduledFor(taskId, updatedDayOfWeek);
                    Console.WriteLine("Your status has been updated!");
                    Console.WriteLine("Would you like to update another item? Yes/No");
                    yesOrNo = Console.ReadLine().ToLower();
                    while (yesOrNo != "yes" && yesOrNo != "no")
                    {
                        Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                        yesOrNo = Console.ReadLine().ToLower();
                    }
                    if (yesOrNo == "yes")
                    {
                        Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id or type '0' to reference the list'");
                        while (!int.TryParse(Console.ReadLine(), out userInput))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out userInput);
                        }
                        while (userInput == 0)
                        {
                            itemsInToDoList.ListAllToDoItems();
                            Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id.");
                            while (!int.TryParse(Console.ReadLine(), out userInput))
                            {
                                Console.WriteLine("Oops, you must enter a numeric value...");
                                int.TryParse(Console.ReadLine(), out userInput);
                            }
                        }

                        Console.WriteLine("Ok, please enter the day you plan to accomplish your task. (Example: Monday)");
                        updatedDayOfWeek = Console.ReadLine();
                        itemsInToDoList.UpdateTaskScheduledFor(userInput, updatedDayOfWeek);
                        Console.WriteLine("Your status has been updated!");
                        Console.WriteLine("Would you like to update another item? Yes/No");
                        yesOrNo = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        break;
                    }

                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                case 4:
                    do
                    {
                        Console.WriteLine("Please enter the task you want to add to your ToDo List");
                        userAnswer = Console.ReadLine();
                        Console.WriteLine("The status of your new task will start off as 'pending'");
                        Console.WriteLine("If you choose, you may write a day you plan to complete the task, OR you may write 'n/a'");
                        var day = Console.ReadLine();
                        itemsInToDoList.AddItem(userAnswer, "pending", day);
                        Console.WriteLine("Your task was added to your ToDo List!");
                        Console.WriteLine("Would you like to add another task? Yes/No");
                        yesOrNo = Console.ReadLine().ToLower();
                        while (yesOrNo != "yes" && yesOrNo != "no")
                        {
                            Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                            yesOrNo = Console.ReadLine().ToLower();
                        }
                    } while (yesOrNo == "yes");
                    break;
                case 5:
                    items = itemsInToDoList.ListAllToDoItems();
                    foreach (var item in items)
                    {
                        Console.WriteLine($"id: {item.id} | task: {item.task} | status: {item.status} | scheduled_for: {item.scheduled_for}");
                    }
                    while (items.Count() == 0)
                    {
                        Console.WriteLine("Your ToDo List is currently empty!");
                        Console.WriteLine("Would you like to add a task? Yes/No");
                        yesOrNo = Console.ReadLine().ToLower();
                        while (yesOrNo != "yes" && yesOrNo != "no")
                        {
                            Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                            yesOrNo = Console.ReadLine().ToLower();
                        }

                        while (yesOrNo == "yes")
                        {
                            Console.WriteLine("Please enter the task you want to add to your ToDo List");
                            userAnswer = Console.ReadLine();
                            Console.WriteLine("The status of your new task will start off as 'pending'");
                            Console.WriteLine(
                                "For the scheduled_for section, you may write a day you plan to complete the task, or you may write 'n/a'");
                            var day = Console.ReadLine();
                            itemsInToDoList.AddItem(userAnswer, "pending", day);
                            Console.WriteLine("Your task was added to your ToDo List!");
                            Console.WriteLine("Would you like to add another task? Yes/No");
                            yesOrNo = Console.ReadLine().ToLower();
                        }

                        if (yesOrNo == "no")
                        {
                            //how to take me back to main menu
                        }
                    }
                    Console.WriteLine("What task id number would you like to delete in your ToDo List? (Hint: enter the number of the id.");
                    while (!int.TryParse(Console.ReadLine(), out userInput))
                    {
                        Console.WriteLine("Oops, you must enter a numeric value...");
                        int.TryParse(Console.ReadLine(), out userInput);
                    }

                    while (userInput == 0)
                    {
                        Console.WriteLine("I'm sorry, '0' is not an option. Please select the id of the task you would like to delete.");
                        int.TryParse(Console.ReadLine(), out userInput);
                        while (!int.TryParse(Console.ReadLine(), out userInput))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out userInput);
                        }
                    }

                    do
                    {
                        itemsInToDoList.DeleteItem(userInput);
                        Console.WriteLine("The item has been deleted.\nWould you like to delete another item? Yes/No");
                        yesOrNo = Console.ReadLine().ToLower();
                        while (yesOrNo != "yes" && yesOrNo != "no")
                        {
                            Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                            yesOrNo = Console.ReadLine().ToLower();
                        }
                    } while (yesOrNo == "yes");
                    break;
                case 6:
                    Console.WriteLine("Thank you for using ToDo! See you again soon...");
                    running = false;
                    break;
                default:
                    break;
            }
        }
    }
}