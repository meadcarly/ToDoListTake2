namespace ToDoListTake2;

public class UserInputs
{
    public static string userAnswer = "";
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

    public static string UserResponseYesNo()
    {
        string yesOrNo = "";
        yesOrNo = Console.ReadLine().ToLower();
        while (yesOrNo != "yes" && yesOrNo != "no")
        {
            Console.WriteLine("I'm sorry, that was not one of our options. Please type 'yes' or 'no'");
            yesOrNo = Console.ReadLine().ToLower();
        }
        return yesOrNo;
    }

    public static void IfUserAnswerNoEnter()
    {
        Console.WriteLine("Ok, since you don't need to enter any more tasks, I'll return you to the main menu.");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    public static void IfUserAnswerNoUpdates()
    {
        Console.WriteLine("Ok, since you don't need to update any more tasks, I'll return you to the main menu.");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    public static void IfUserAnswerNoDelete()
    {
        Console.WriteLine("Ok, since you don't need to delete any more tasks, I'll return you to the main menu.");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}