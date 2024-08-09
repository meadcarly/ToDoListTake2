namespace ToDoListTake2;

public interface IToDoListRepository
{
    public IEnumerable<ToDoList> ListAllToDoItems();

    public void AddItem(string task, string status, string scheduled_for);

    public void UpdateTask(int id, string updatedStatus);

    public void DeleteItem(int id);
}