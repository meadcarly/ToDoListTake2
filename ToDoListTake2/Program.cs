using System.Data;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ToDoListTake2;
//This is my tester branch
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
        var userAnswer = "";
        var items = "";
        var taskId = 0;
        

        while (running)
        {
            Prompts.WelcomeMenu();
           

            int userInput = UserInputs.MainMenuUserResponse();
            switch (userInput)
            {
                case 1:
                    itemsInToDoList.ListAllToDoItems();
                    
                    Console.WriteLine("Would you like to add a task? yes/no");
                    userAnswer = UserInputs.UserResponseYesNo();
                    while (userAnswer == "yes")
                    {
                        var taskDetails = itemsInToDoList.GetTaskDetails();
                        itemsInToDoList.AddItem(taskDetails.task, taskDetails.status, taskDetails.scheduled_for);
                        userAnswer = UserInputs.UserResponseYesNo();
                    }

                    if (userAnswer == "no")
                    {
                        UserInputs.IfUserAnswerNo();
                    }

                    break;
                case 2:
                    do
                    {
                        var taskDetails = itemsInToDoList.GetTaskDetails();
                        itemsInToDoList.AddItem(taskDetails.task, taskDetails.status, taskDetails.scheduled_for);
                        Console.WriteLine("Would you like to add another task? yes/no");
                        userAnswer = UserInputs.UserResponseYesNo();
                    } while (userAnswer == "yes");
                    
                    if (userAnswer == "no")
                    {
                        UserInputs.IfUserAnswerNo();
                    }
                    break;
                //_____________________________________________________________________________________________
                case 3:
                    /*Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id or type '0' to view all to do items.'");
                    while(!int.TryParse(Console.ReadLine(), out taskId))
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
                        Console.WriteLine("What task id would you like to update in your ToDo List? (Hint: enter the id number.");
                        while(!int.TryParse(Console.ReadLine(), out taskId))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out taskId);
                        }
                    }

                    /Console.WriteLine("Ok, please enter the day of the week you plan to accomplish your task. (Example: Monday)");
                    var updatedDayOfWeek = Console.ReadLine();
                    itemsInToDoList.UpdateTaskScheduledFor(taskId, updatedDayOfWeek);
                    Console.WriteLine("Your status has been updated!");
                    Console.WriteLine("Would you like to update another item? yes/no");
                    yesOrNo = Console.ReadLine().ToLower();
                    while (yesOrNo != "yes" && yesOrNo != "no")
                    {
                        Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                        yesOrNo = Console.ReadLine().ToLower();
                    }
                    if (yesOrNo == "yes")
                    {
                        Console.WriteLine("What task id would you like to update in your ToDo List? (Hint: enter the id number or type '0' to view all to do items.'");
                        while(!int.TryParse(Console.ReadLine(), out userInput))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out userInput);
                        }
                        while (userInput == 0)
                        {
                            itemsInToDoList.ListAllToDoItems();
                            Console.WriteLine("What task id would you like to update in your ToDo List? (Hint: enter the id number.");
                            while(!int.TryParse(Console.ReadLine(), out userInput))
                            {
                                Console.WriteLine("Oops, you must enter a numeric value...");
                                int.TryParse(Console.ReadLine(), out userInput);
                            }
                        }

                        Console.WriteLine("Ok, please enter the day of the week you plan to accomplish your task. (Example: Monday)");
                        updatedDayOfWeek = Console.ReadLine();
                        itemsInToDoList.UpdateTaskScheduledFor(userInput, updatedDayOfWeek);
                        Console.WriteLine("Your status has been updated!");
                        Console.WriteLine("Would you like to update another item? yes/no");
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
                    Console.WriteLine("What task id would you like to update in your ToDo List? (Hint: enter the id number or type '0' to view all to do items.'");
                    while(!int.TryParse(Console.ReadLine(), out taskId))
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
                        Console.WriteLine("What task id would you like to update in your ToDo List? (Hint: enter the id number.");
                        while(!int.TryParse(Console.ReadLine(), out taskId))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out taskId);
                        }
                    }

                    Console.WriteLine("Ok, please enter an updated status for your task.");
                    var updatedStatus = Console.ReadLine();
                    itemsInToDoList.UpdateTaskStatus(taskId, updatedStatus);
                    Console.WriteLine("Your status has been updated!");
                    Console.WriteLine("Would you like to update another item? yes/no");
                    yesOrNo = Console.ReadLine().ToLower();
                    while (yesOrNo != "yes" && yesOrNo != "no")
                    {
                        Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                        yesOrNo = Console.ReadLine().ToLower();
                    }
                    if (yesOrNo == "yes")
                    {
                        Console.WriteLine("What task id would you like to update in your ToDo List? (Hint: enter the id number or type '0' to view all to do items.'");
                        while(!int.TryParse(Console.ReadLine(), out userInput))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out userInput);
                        }
                        while (userInput == 0)
                        {
                            itemsInToDoList.ListAllToDoItems();
                            Console.WriteLine("What task id would you like to update in your ToDo List? (Hint: enter the id number.");
                            while(!int.TryParse(Console.ReadLine(), out userInput))
                            {
                                Console.WriteLine("Oops, you must enter a numeric value...");
                                int.TryParse(Console.ReadLine(), out userInput);
                            }
                        }

                        Console.WriteLine("Ok, please enter an updated status for your task.");
                        userAnswer = Console.ReadLine();
                        itemsInToDoList.UpdateTaskStatus(userInput, userAnswer);
                        Console.WriteLine("Your status has been updated!");
                        Console.WriteLine("Would you like to update another item? yes/no");
                        yesOrNo = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        break;
                    }

                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
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
                        Console.WriteLine("Would you like to add a task? yes/no");
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
                            Console.WriteLine("Would you like to add another task? yes/no");
                            yesOrNo = Console.ReadLine().ToLower();
                        }

                        if (yesOrNo == "no")
                        {
                            //how to take me back to main menu
                        }
                    }
                    Console.WriteLine("What task id would you like to delete in your ToDo List? (Hint: enter the id number.)");
                    while(!int.TryParse(Console.ReadLine(), out userInput))
                    {
                        Console.WriteLine("Oops, you must enter a numeric value...");
                        int.TryParse(Console.ReadLine(), out userInput);
                    }

                    while (userInput == 0)
                    {
                        Console.WriteLine("I'm sorry, '0' is not an option. Please select the id of the task you would like to delete.");
                        int.TryParse(Console.ReadLine(), out userInput);
                        while(!int.TryParse(Console.ReadLine(), out userInput))
                        {
                            Console.WriteLine("Oops, you must enter a numeric value...");
                            int.TryParse(Console.ReadLine(), out userInput);
                        }
                    }

                    do
                    {
                        itemsInToDoList.DeleteItem(userInput);
                        Console.WriteLine("The item has been deleted.\nWould you like to delete another item? yes/no");
                        yesOrNo = Console.ReadLine().ToLower();
                        while (yesOrNo != "yes" && yesOrNo != "no")
                        {
                            Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                            yesOrNo = Console.ReadLine().ToLower();
                        }

                        do
                        {
                            Console.WriteLine(
                                "What task id would you like to delete in your ToDo List? (Hint: enter the id number.");
                            while (!int.TryParse(Console.ReadLine(), out userInput))
                            {
                                Console.WriteLine("Oops, you must enter a numeric value...");
                                int.TryParse(Console.ReadLine(), out userInput);
                            }

                            while (userInput == 0)
                            {
                                Console.WriteLine(
                                    "I'm sorry, '0' is not an option. Please select the id of the task you would like to delete.");
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
                                Console.WriteLine(
                                    "The item has been deleted.\nWould you like to delete another item? yes/no");
                                yesOrNo = Console.ReadLine().ToLower();
                                while (yesOrNo != "yes" && yesOrNo != "no")
                                {
                                    Console.WriteLine(
                                        "I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
                                    yesOrNo = Console.ReadLine().ToLower();
                                }
                            } while (yesOrNo == "yes");

                        } while (yesOrNo == "yes");
                    } while (yesOrNo == "yes");

                    break;*/
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