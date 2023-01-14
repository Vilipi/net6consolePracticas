using aa1.Services;
using Spectre.Console;
using System.Globalization;

int exit = 0;

UserService _userService = new UserService();
NotLoggedUser _notLoggedUserService = new NotLoggedUser();

AnsiConsole.Write(new FigletText("Películas Favoritas").Color(Color.Cyan1));

while (exit == 0) {
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\nPelículas Favoritas app");
    Console.ResetColor();
    Console.WriteLine("Please, type a number to choose an option");
    Console.WriteLine(" - 1: User zone");
    Console.WriteLine(" - 2: Check films");
    Console.WriteLine(" - 3: exit");
    string option = Console.ReadLine();

    if (option == "1") {
        var patientMenu = _userService.UserMenu();
        exit = patientMenu;
    } else if (option == "2") {
        _notLoggedUserService.ShowFilms();
    } else if (option == "3") {
        Console.WriteLine("Bye!");
        exit = 1;
    } else {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please try again\n");
        Console.ResetColor();

    }
};


