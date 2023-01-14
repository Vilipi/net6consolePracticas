using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aa1.Models;
using Newtonsoft.Json;
using practicas2daw.Services;
using Serilog;

namespace aa1.Services {
    public class NotLoggedUser {
        JsonService jsonService = new JsonService();

        string dateEnv = Environment.GetEnvironmentVariable("env_date");

        public void ShowFilms() {

            string filmsString = jsonService.GetFilmListFromFile();
            var films = jsonService.DeserializeJsonFile(filmsString);
            films.ForEach(e => {
                if(dateEnv == "us")
                {
                    Console.WriteLine($"{e.Name} - {e.CreationDate.ToString("MM/dd/yyyy")}");
                }
                else
                {
                    Console.WriteLine($"{e.Name} - {e.CreationDate.ToString("dd MMMM yyyy")}");
                }
            });
        }
    }
}
