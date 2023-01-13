using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aa1.Models;
using Newtonsoft.Json;

namespace aa1.Services
{
    public class NotLoggedUser
    {
        public static string _path = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}/Utils/films.json";

        public void ShowFilms()
        {
            //Console.WriteLine(_path);
            string filmsString = GetFilmListFromFile();
            var films = DeserializeJsonFile(filmsString);
            films.ForEach(e =>
            {
                Console.WriteLine(e.Name);
            });
        }

        // Info from JSON
        private string GetFilmListFromFile() {
            string filmsJsonFromFile;

            var reader = new StreamReader(_path);
            filmsJsonFromFile = reader.ReadToEnd();
            
            return filmsJsonFromFile;
        }
        private List<Film> DeserializeJsonFile(string filmsJsonFromFile) {
            return JsonConvert.DeserializeObject<List<Film>>(filmsJsonFromFile);
        }
    }
}
