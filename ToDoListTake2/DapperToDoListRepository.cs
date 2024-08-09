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

    public IEnumerable<ToDoList> ListAllToDoItems()
    {
        return _connection.Query<ToDoList>("SELECT * FROM ToDo;");
    }

    public void AddItem(string task, string status, string scheduled_for)
    {
        _connection.Execute(
            "INSERT INTO ToDo (task, status, scheduled_for) VALUES (@task, @status, @scheduled_for);",
            new { task = task, status = status, scheduled_for = scheduled_for });
    }

    public void UpdateTask(int id, string updatedStatus)
    {
        _connection.Execute("UPDATE ToDo SET status = @updatedStatus WHERE id = @id;", new { id, updatedStatus});
    }

    public void DeleteItem(int id)
    {
        _connection.Execute("DELETE FROM ToDo WHERE id = @id;", new { id = id});
    }
}
