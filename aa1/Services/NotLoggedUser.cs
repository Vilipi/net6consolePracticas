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

        public void ShowFilms() {

            string filmsString = jsonService.GetFilmListFromFile();
            var films = jsonService.DeserializeJsonFile(filmsString);
            films.ForEach(e => {
                Console.WriteLine(e.Name);
            });
        }
    }
}
