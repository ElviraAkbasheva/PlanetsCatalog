using System;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lambda
{
    internal class Planet
    {
        public string Name { get; set; }
        public int IndexNumber { get; set; }
        public int EquatorLength { get; set; }
        public Planet PreviousPlanet { get; set; }
        public Planet(string name, int index, int equator, Planet previous)
        {
            Name = name;
            IndexNumber = index;
            EquatorLength = equator;
            PreviousPlanet = previous;
        }
    }
    internal class PlanetCatalog
    {
        List<Planet> planetsList = new List<Planet>();
        public PlanetCatalog(params Planet[] planets)
        {
            foreach (Planet planet in planets)
            {
                planetsList.Add(planet);
            }
        }
        public (int index, int equator, string error) GetPlanet(string name, Validator method)
        {
            var valResult = method(name);

            if (!string.IsNullOrEmpty(valResult))
            {
                return (index: 0, equator: 0, error: valResult);
            }

            foreach (Planet planet in planetsList)
            {
                if (planet.Name == name)
                {
                    return (index: planet.IndexNumber, equator: planet.EquatorLength, error: null);
                }
            }
            return (index: 0, equator: 0, error: "Не удалось найти планету");
        }
    }

    delegate string Validator(string name);

    internal class Program_3
    {
        public static void PrintResult((int index, int equator, string error) tuple, string name)
        {
            if (tuple.error == null)
            {
                Console.WriteLine($"Наименование планеты: {name}");
                Console.WriteLine($"Порядковый номер от Солнца: {tuple.index}");
                Console.WriteLine($"Длина экватора: {tuple.equator} км\n");
            }
            else
            {
                Console.WriteLine($"{tuple.error}\n");
            }
        }

        static void Main(string[] args)
        {
            int callCounter = 0;

            string n_venus = "Венера";
            string n_earth = "Земля";
            string n_mars = "Mars";
            string n_lemony = "Лимония";

            Planet venus = new Planet(n_venus, 2, 38025, null);
            Planet earth = new Planet(n_earth, 3, 40076, venus);
            Planet mars = new Planet(n_mars, 4, 21344, earth);
            PlanetCatalog catalog = new PlanetCatalog(venus, earth, mars);

            Validator callVal = (name) =>
            {
                callCounter++;
                if (callCounter == 3)
                {
                    callCounter = 0;
                    return "Вы спрашиваете слишком часто";
                }
                return null;
            };

            Validator planetVal = (name) =>
            {
                if (name == "Лимония")
                {
                    return "Это запретная планета";
                }
                return null;
            };

            string[] check = { n_earth, n_lemony, n_mars};
            foreach (string s in check)
            {
                PrintResult(catalog.GetPlanet(s, callVal), s);
            }
            foreach (string s in check)
            {
                PrintResult(catalog.GetPlanet(s, planetVal), s);
            }
        }
    }
}
