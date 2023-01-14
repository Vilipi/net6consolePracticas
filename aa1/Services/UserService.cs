using aa1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace aa1.Services {
    public class UserService {

        public static string _path2 = $@"{Path.GetFullPath(Directory.GetCurrentDirectory())}/Resource/films.json"; //docker


        public static List<User> users = new List<User>
        {
            new User{Name = "Demo", LastName = "Demo", UserName = "demo", Password= "demo", Films= new List<Film>()} //usar de ejemplo
        };
        public static List<Film> films = new List<Film>();

        public int UserMenu() {
            //Console.WriteLine("Please write a number to choose an action:");
            Console.WriteLine(" - 1: Sign up");
            Console.WriteLine(" - 2: Sign in");
            Console.WriteLine(" - 3: Back");
            string login = Console.ReadLine();
            while (login != "1" && login != "2" && login != "3") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong number, try again");
                Console.ResetColor();
                login = Console.ReadLine();

            }

            if (login == "1") {
                return SignUp();
            } else if (login == "2") {
                var userlogged = SignIn();
                var menu = LoggedUserMenu(userlogged);

                while (menu != 0) {
                    menu = LoggedUserMenu(userlogged);
                }
                return 0;
            } else {
                return 0;
            }
        }

        private int SignUp() {
            var newUser = new User();
            Console.WriteLine("Name:");
            newUser.Name = Console.ReadLine();
            Console.WriteLine("LastName:");
            newUser.LastName = Console.ReadLine();
            Console.WriteLine("Date of birth (dd/MM/yyyy):");
            //var db = DateTime.Parse(Console.ReadLine());
            var db = Console.ReadLine();
            DateTime i;
            bool result = DateTime.TryParse(db, out i);
            while (!result) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong date format\n");
                Console.ResetColor();
                Console.WriteLine("Please repeat your date of birth:");
                db = Console.ReadLine();
                result = DateTime.TryParse(db, out i);
            }
            newUser.BirthDate = i;

            Console.WriteLine("Email:");
            newUser.Email = Console.ReadLine();
            Console.WriteLine("UserName:");
            newUser.UserName = Console.ReadLine();
            Console.WriteLine("Password:");
            newUser.Password = Console.ReadLine();
            newUser.Films = new List<Film>();

            Console.WriteLine("Please repeat your password:");
            var passwordRepeated = Console.ReadLine();

            while (passwordRepeated != newUser.Password) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYour passwords do not match!\n");
                Console.ResetColor();
                Console.WriteLine("Please repeat your password:");
                passwordRepeated = Console.ReadLine();
            }

            users.Add(newUser);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nUser {newUser.UserName} created !\n");
            //Console.WriteLine($"Your bd is {newUser.BirthDate.ToString("dd/MM/yyyy")}");
            Console.WriteLine("Next time login using your username and password\n");
            Console.ResetColor();
            return 0;
        }

        private User SignIn() {
            Console.WriteLine("Username: ");
            var inputUsername = Console.ReadLine();
            var existingUser = users.Find(x => x.UserName == inputUsername);
            while (existingUser == null) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found, try a different username");
                Console.ResetColor();
                Console.WriteLine("Username: ");
                inputUsername = Console.ReadLine();
                existingUser = users.Find(x => x.UserName == inputUsername);
            }
            Console.WriteLine("Password: ");
            var inputPassword = Console.ReadLine();
            while (inputPassword != existingUser.Password) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong password. Try again.");
                Console.ResetColor();
                Console.WriteLine("Password: ");
                inputPassword = Console.ReadLine();
                existingUser = users.Find(x => x.UserName == inputUsername);
            }
            Console.WriteLine($"\nHi {existingUser.Name} {existingUser.LastName}");

            return existingUser;
        }

        private int LoggedUserMenu() {
            var specilasitsString = GetFilmListFromFile();
            var specialiast = DeserializeJsonFile(specilasitsString);
            return 0;
        }

        private int LoggedUserMenu(User user) {
            Console.WriteLine("Please write one of the following numbers:");
            Console.WriteLine(" - 1: View all Films");
            Console.WriteLine(" - 2: View my favourite films");
            Console.WriteLine(" - 3: Log out");
            string loggedUserAction = Console.ReadLine();

            while (loggedUserAction != "1" && loggedUserAction != "2" && loggedUserAction != "3") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong number, try again");
                Console.ResetColor();
                loggedUserAction = Console.ReadLine();

            }
            if (loggedUserAction == "1") {
                string filmsString = GetFilmListFromFile();
                var films = DeserializeJsonFile(filmsString);
                films.ForEach(e => {
                    Console.WriteLine($" - {e.Id}: {e.Name}");
                });

                Console.WriteLine("\n1 - Add/Remove film to favorites");
                Console.WriteLine("2 - Rate film");
                Console.WriteLine("3 - Back");
                string filmsAction = Console.ReadLine();
                while (filmsAction != "1" && filmsAction != "2" && filmsAction != "3") {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong number, try again");
                    Console.ResetColor();
                    loggedUserAction = Console.ReadLine();

                }
                if (filmsAction == "1") {
                    Console.WriteLine("Write the Id of the film you want to add/remove from your favorite list");
                    string filmsSelected = Console.ReadLine();

                    int filmToAdd;
                    bool filmToAddBool = int.TryParse(filmsSelected, out filmToAdd);
                    //&& Int32.Parse(appointmentToCancel) >= 0 && Int32.Parse(appointmentToCancel) < patientAppointments.Count
                    while (filmToAddBool == false) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong number, try again");
                        Console.ResetColor();
                        filmsSelected = Console.ReadLine();
                        filmToAddBool = int.TryParse(filmsSelected, out filmToAdd);
                    }
                    var appointmentToCancelIndex = Int32.Parse(filmsSelected);
                    while (appointmentToCancelIndex < 1 || appointmentToCancelIndex > films.Count) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong number, try again");
                        Console.ResetColor();
                        appointmentToCancelIndex = Int32.Parse(Console.ReadLine());
                    }

                    var existingFilm = user.Films.Find(e => e.Id == appointmentToCancelIndex);
                    if (existingFilm == null) {
                        user.Films.Add(films[appointmentToCancelIndex - 1]);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Film added to Favorites");
                        Console.ResetColor();
                    } else {
                        user.Films.Remove(existingFilm);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Film removed from Favorites");
                        Console.ResetColor();
                    }
                    return 1;
                } else if (filmsAction == "2") {
                    films.ForEach(e => {
                        Console.WriteLine($" - {e.Id} - {e.Name}: {e.Rating}\n");
                    });
                    Console.WriteLine("Choose the film you want to rate");
                    string filmToRate = Console.ReadLine();

                    int filmToAdd;
                    bool filmToAddBool = int.TryParse(filmToRate, out filmToAdd);
                    //&& Int32.Parse(appointmentToCancel) >= 0 && Int32.Parse(appointmentToCancel) < patientAppointments.Count
                    while (filmToAddBool == false) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong number, try again");
                        Console.ResetColor();
                        filmToRate = Console.ReadLine();
                        filmToAddBool = int.TryParse(filmToRate, out filmToAdd);
                    }
                    var filmsToRateIndex = Int32.Parse(filmToRate);
                    while (filmsToRateIndex < 1 || filmsToRateIndex > films.Count) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong number, try again");
                        Console.ResetColor();
                        filmsToRateIndex = Int32.Parse(Console.ReadLine());
                    }

                    var filmSelectedToRate = films.Find(e => e.Id == filmsToRateIndex);
                    Console.WriteLine($"Rate from 0 to 10 {filmSelectedToRate.Name}:");
                    string rating = Console.ReadLine();

                    int ratingToAdd;
                    bool ratingToAddBool = int.TryParse(rating, out ratingToAdd);
                    //&& Int32.Parse(appointmentToCancel) >= 0 && Int32.Parse(appointmentToCancel) < patientAppointments.Count
                    while (ratingToAddBool == false) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong number, try again");
                        Console.ResetColor();
                        rating = Console.ReadLine();
                        ratingToAddBool = int.TryParse(rating, out filmToAdd);
                    }
                    var RateInt = Int32.Parse(rating);
                    while (RateInt < 0 || RateInt > 10) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong number, try again");
                        Console.ResetColor();
                        RateInt = Int32.Parse(Console.ReadLine());
                    }

                    var newRating = ((filmSelectedToRate.Rating * filmSelectedToRate.VotesGiven) + RateInt) / (filmSelectedToRate.VotesGiven + 1);
                    films.Find(e => e.Id == filmsToRateIndex).Rating = newRating;
                    films.Find(e => e.Id == filmsToRateIndex).VotesGiven++;
                    SerializeJsonFile(films);
                    Console.WriteLine("Film voted!");
                    return 1;

                } else {
                    return 1;
                }
            } else if (loggedUserAction == "2") {
                var favFilms = user.Films;
                if (favFilms.Count == 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou don not have any favorite film yet!!\n");
                    Console.ResetColor();
                    return 1;
                } else {
                    user.Films.ForEach(e => {
                        Console.WriteLine(e.Name);
                    });
                    return 1;
                }

            } else // salir
              {
                return 0;
            }
        }





        // Info from JSON
        private string GetFilmListFromFile() {
            string filmsJsonFromFile;

            var reader = new StreamReader(_path2);
            filmsJsonFromFile = reader.ReadToEnd();

            return filmsJsonFromFile;
        }
        private List<Film> DeserializeJsonFile(string filmsJsonFromFile) {
            return JsonConvert.DeserializeObject<List<Film>>(filmsJsonFromFile);
        }
        private void SerializeJsonFile(List<Film> films) {
            string filmsJson = JsonConvert.SerializeObject(films.ToArray(), Formatting.Indented);
            File.WriteAllText(_path2, filmsJson);
        }
    }
}
