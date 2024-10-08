using System.Data;
using Dapper;

namespace ToDoListTake2;

public class DapperToDoListRepository : IToDoListRepository
{
    private readonly IDbConnection _connection;

    public DapperToDoListRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public void ListAllToDoItems()
    {
        var toDoItems = _connection.Query<ToDoList>("SELECT * FROM ToDo;");
        if (toDoItems.Any())
        {
            foreach (var item in toDoItems)
            {
                Console.WriteLine($"id: {item.id} | task: {item.task} | status: {item.status} | scheduled_for: {item.scheduled_for}");
            }
        }
        else
        {
            Console.WriteLine("Your ToDo List is currently empty!");
        }
    }

    public void AddItem(string task, string status, string scheduled_for)
    {
        _connection.Execute("INSERT INTO ToDo (task, status, scheduled_for) VALUES (@task, @status, @scheduled_for);",
          new { task = task, status = status, scheduled_for = scheduled_for });
        Console.WriteLine("Your task was added to your ToDo List!");
    }

    public void UpdateTaskStatus(int id, string updatedStatus)
    {
        _connection.Execute("UPDATE ToDo SET status = @updatedStatus WHERE id = @id;", new { id, updatedStatus});
    }

    public void UpdateTaskScheduledFor(int id, string updatedScheduleFor)
    {
        _connection.Execute("UPDATE ToDo SET scheduled_for = @updatedScheduleFor WHERE id = @id;", new { id, updatedScheduleFor });
    }

    public void DeleteItem(int id)
    {
        _connection.Execute("DELETE FROM ToDo WHERE id = @id;", new { id = id});
    }

    public (string task, string status, string scheduled_for) GetTaskDetails()
    {
        string taskAdd;
        do
        {
            Console.WriteLine("Please enter the task you want to add to your ToDo List");
            taskAdd = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(taskAdd))
            {
                Console.WriteLine("Oops, you forgot to enter the task. Please write a task to add to your ToDo List.");
            }
        } while (string.IsNullOrEmpty(taskAdd));
        
        Console.WriteLine("The status of your new task will start off as 'pending'");
        string statusAdd = "pending";
        Console.WriteLine("If you choose, you may write a day you plan to complete the task, OR you may write 'n/a'");
        string scheduled_forAdd = Console.ReadLine();

        return (taskAdd, statusAdd, scheduled_forAdd);
    }

    /*public int TryParseTaskId()
    {
        var taskId = 0;
        while(!int.TryParse(Console.ReadLine(), out taskId))
        {
            Console.WriteLine("Oops, you must enter a numeric value...");
            int.TryParse(Console.ReadLine(), out taskId);
        }
        return taskId;
    }*/
    public int GetTaskId()
    {
        int taskId;
        Console.WriteLine("What task id number would you like to update in your ToDo List? (Hint: enter the number of the id or type '0' to view all to do items.)");
        
        while(true)
        {
            if (int.TryParse(Console.ReadLine(), out taskId))
            {
                return taskId;
            }
            else
            {  
                Console.WriteLine("Oops, you must enter a numeric value...");
            }
        }
    }
    public int GetTaskIdToDelete()
    {
        int taskId;
        Console.WriteLine("What task id number would you like to delete in your ToDo List?");

        while(true)
        {
            if (int.TryParse(Console.ReadLine(), out taskId))
            {
                return taskId;
            }
            else
            {
                Console.WriteLine("Oops, you must enter a numeric value...");
            }
        }
        
    }
    
    public bool IsListEmpty()
    {
        var count = _connection.ExecuteScalar<int>("SELECT COUNT(*) FROM ToDo;");
        return count == 0;
    }

    public string DayToAccomplishTask()
    {
        string updatedDayOfWeek;
        do
        {
            Console.WriteLine("Ok, please enter the day of the week you plan to accomplish your task. (Example: Monday)");
        
             updatedDayOfWeek = Console.ReadLine();
            
        } while (string.IsNullOrEmpty(updatedDayOfWeek));

        return updatedDayOfWeek;
    }

    public string GetStatus()
    {
        string updatedStatus;
        do
        {
            Console.WriteLine("Ok, please enter an updated status for your task.");
            
            updatedStatus = Console.ReadLine();
        } while (string.IsNullOrEmpty(updatedStatus));

        return updatedStatus;
    }
}