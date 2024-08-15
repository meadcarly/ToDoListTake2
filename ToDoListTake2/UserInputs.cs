namespace ToDoListTake2;

public class UserInputs
{
    public static int MainMenuUserResponse()
    {
        int.TryParse(Console.ReadLine(), out int userInput);
        while (userInput == null || userInput < 1 || userInput > 6)
        {
            Console.WriteLine("I'm sorry, that was not one of our options. Please type the number of the option you would like.");
            int.TryParse(Console.ReadLine(), out userInput);
        }

        return userInput;
    }
}