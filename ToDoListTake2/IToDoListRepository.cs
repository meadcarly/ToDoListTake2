namespace ToDoListTake2;

public interface IToDoListRepository
{
    public void ListAllToDoItems();

    public void AddItem(string task, string status, string scheduled_for);

    public void UpdateTaskStatus(int id, string updatedStatus);

    public void UpdateTaskScheduledFor(int id, string updatedScheduleFor);

    public void DeleteItem(int id);
}