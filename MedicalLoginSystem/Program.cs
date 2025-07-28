using MedicalLoginSystemConsole.Models;
using MedicalLoginSystemConsole.Services;

//Console.WriteLine("=== Medistra - Login & Registration ===");
//Console.WriteLine("A program designed for the authentication of patients and administrators.");
//Console.WriteLine("All data is stored locally and verified via an SMS code.\n");

bool showTitle = true;

while (true)
{

    if (showTitle)
    {

        Console.WriteLine("=== Medistra – Login & Registration ===");
        Console.WriteLine("A program designed for the authentication of patients and administrators.");
        Console.WriteLine("All data is stored locally and verified via an SMS code.\n");

        showTitle = false;

    } else Console.Clear();

    //Console.Clear();

    Console.WriteLine("\n1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");
    Console.Write("Choose an option: ");
    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.Clear();
            Register();
            showTitle = true;
            break;
        case "2":
            Console.Clear();
            Login();
            showTitle = true;
            break;
        case "3":
            Console.WriteLine("Thank you for using our program!");
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            Thread.Sleep(1500);
            Console.Clear();
            break;
    }
}

void Register()
{
    string email, password, phone, role;
    string surname, name, fullName;

    while (true)
    {
        Console.Write("Surname: ");
        surname = Console.ReadLine()!;
        Console.Write("Name: ");
        name = Console.ReadLine()!;
        fullName = $"{surname} {name}";

        Console.WriteLine($"Your full name is: {fullName}");
        Console.Write("Is this correct? (Y/N): ");
        var confirm = Console.ReadLine()!.Trim().ToLower();

        if (confirm == "y" || confirm == "Y") break;

        Console.WriteLine("Please reenter your information.\n");
    }

    Console.Write("Email: ");
    email = Console.ReadLine()!;
    Console.Write("Password: ");
    password = Console.ReadLine()!;
    Console.Write("Phone number (+407...): ");
    phone = Console.ReadLine()!;
    Console.Write("Role (User/Admin): ");
    role = Console.ReadLine()!;

    if (UserManager.EmailExists(email))
    {
        Console.WriteLine("This email already exists.");
        Console.WriteLine("\nPress 0 to return to the main menu...");
        while (Console.ReadKey(true).KeyChar != '0')
        {
            // Wait until '0' is pressed
        }
        Console.Clear();
        return;
    }

    try
    {
        SmsService.SendCode(phone);
        Console.Write("Please enter the code received via SMS: ");
        string code = Console.ReadLine()!;

        if (code != SmsService.LatestCode)
        {
            Console.WriteLine("Incorrect code. Failed to Register.");
            Console.WriteLine("\nPress 0 to return to the main menu...");
            while (Console.ReadKey(true).KeyChar != '0')
            {
                // Wait until '0' is pressed
            }
            Console.Clear();
            return;
        }

        var user = new User(email, fullName, password, role, phone);
        UserManager.SaveUser(user);
        Console.WriteLine($"Account created successfully! Welcome, {fullName}");

        Console.WriteLine("\nPress 0 to return to the main menu...");
        while (Console.ReadKey(true).KeyChar != '0')
        {
            // Wait until '0' is pressed
        }
        Console.Clear();

    }
    catch (Exception ex)
    {
        Console.WriteLine("Error -> SMS could not be sent: " + ex.Message);
        Console.WriteLine("\nPress 0 to return to the main menu...");
        while (Console.ReadKey(true).KeyChar != '0')
        {
            // Wait until '0' is pressed
        }
        Console.Clear();
    }
}

void Login()
{
    Console.Write("Email: ");
    string email = Console.ReadLine()!;
    Console.Write("Password: ");
    string password = Console.ReadLine()!;

    var user = UserManager.ValidateLogin(email, password);
    if (user == null)
    {
        Console.WriteLine("Incorrect authentication data.");
        return;
    }

    Console.WriteLine($"\nHello, {user.Name}! You are logged in as {user.Role}.");

    if (user.Role.ToLower() == "admin" || user.Role.ToLower() == "Admin")
    {
        Console.WriteLine("- Accessing the admin panel...");
    }
    else if (user.Role.ToLower() == "user" || user.Role.ToLower() == "User")
    {
        Console.WriteLine("- Accessing the user panel...");
    }

    Console.WriteLine("\nPress 0 to return to the main menu...");
    while (Console.ReadKey(true).KeyChar != '0')
    {
        // Wait until '0' is pressed
    }
    Console.Clear();

}