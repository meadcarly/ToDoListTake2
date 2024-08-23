using System.Data;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ToDoListTake2;
//This is my tester branch
//Comment for testing
//Test 2
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
                        Console.WriteLine("Would you like to add another task? yes/no");
                        userAnswer = UserInputs.UserResponseYesNo();
                    }

                    if (userAnswer == "no")
                    {
                        UserInputs.IfUserAnswerNoEnter();
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
                        UserInputs.IfUserAnswerNoEnter();
                    }
                    break;
                //_____________________________________________________________________________________________
                case 3:
                    do
                    {
                        taskId = itemsInToDoList.GetTaskId();

                        while (taskId == 0)
                        {
                            itemsInToDoList.ListAllToDoItems();
                            
                            taskId = itemsInToDoList.GetTaskId();
                            
                        }

                        var updatedDay = itemsInToDoList.DayToAccomplishTask();
                        itemsInToDoList.UpdateTaskScheduledFor(taskId, updatedDay);
                        Console.WriteLine("Your status has been updated!");
                        Console.WriteLine("Would you like to update another item? yes/no");
                        userAnswer = UserInputs.UserResponseYesNo();
                        
                    } while (userAnswer == "yes");
                            
                    UserInputs.IfUserAnswerNoUpdates();
                    break;
                case 4:
                    do
                    {
                        taskId = itemsInToDoList.GetTaskId();
                    
                        while (taskId == 0)
                        {
                            itemsInToDoList.ListAllToDoItems();

                            taskId = itemsInToDoList.GetTaskId();
                        }

                        var updateStatus = itemsInToDoList.GetStatus();
                        itemsInToDoList.UpdateTaskStatus(taskId, updateStatus);
                        Console.WriteLine("Your status has been updated!");
                        Console.WriteLine("Would you like to update another item? yes/no");
                        userAnswer = UserInputs.UserResponseYesNo();
                    } while (userAnswer == "yes");
                    
                    UserInputs.IfUserAnswerNoUpdates();
                    break;

                case 5:
                    itemsInToDoList.ListAllToDoItems();

                    if(itemsInToDoList.IsListEmpty())
                    {
                        Console.WriteLine("Your ToDo List is currently empty!");
                        Console.WriteLine("Would you like to add a task? yes/no");
                        userAnswer = UserInputs.UserResponseYesNo();
                        while(userAnswer == "yes")
                        {
                            var taskDetails = itemsInToDoList.GetTaskDetails();
                            itemsInToDoList.AddItem(taskDetails.task, taskDetails.status,
                                taskDetails.scheduled_for);
                            Console.WriteLine("Would you like to add another task? yes/no");
                            userAnswer = UserInputs.UserResponseYesNo();
                        }

                        UserInputs.IfUserAnswerNoEnter();
                        break;
                    }
                        
                    do
                    {
                        var id = itemsInToDoList.GetTaskIdToDelete();
                        itemsInToDoList.DeleteItem(id);

                        Console.WriteLine("Your task was deleted. Would you like to delete another item? yes/no");
                        userAnswer = UserInputs.UserResponseYesNo();
                    }while(userAnswer == "yes");

                    UserInputs.IfUserAnswerNoDelete();
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