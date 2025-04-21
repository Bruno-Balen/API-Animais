using API_csv.database.Models;
using System.Collections.Generic;
using System.IO;

namespace API_csv.database
{
    public class DBContext
    {
        private const string pathName =
            "C:\\Repositorios\\Paradigmas\\API-.NET\\API-csv\\animais.txt";

        private readonly List<Animal> _Animais = new();

        public DBContext()
        {
           string[] lines =  File.ReadAllLines(pathName);

            for (int i = 1; i < lines.Length; i++)
            {
               string[] coluns =  lines[i].Split(';');

               Animal animal = new Animal();
               animal.Id = int.Parse(coluns[0]);
               animal.Name = coluns[1];
               animal.Description = coluns[2];
               animal.Origin = coluns[3];
               animal.Reproduction = coluns[4];
               animal.Feeding = coluns[5];

               _Animais.Add(animal);
            }
        }
        public List<Animal> Animais => _Animais;
    }
}
