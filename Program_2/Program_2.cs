using System;
using System.Reflection.Metadata.Ecma335;

namespace Tuple
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
        int callCounter = 0;
        public PlanetCatalog(params Planet[] planets)
        {
            foreach (Planet planet in planets)
            {
                planetsList.Add(planet);
            }
        }
        public (int index, int equator, string error) GetPlanet(string name)
        {
            callCounter++;
            if (callCounter == 3)
            {
                callCounter = 0;
                return (index: 0, equator: 0, error: "Вы спрашиваете слишком часто");
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

    internal class Program_2
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
            string n_venus = "Венера";
            string n_earth = "Земля";
            string n_mars = "Mars";
            string n_lemony = "Лимония";

            Planet venus = new Planet(n_venus, 2, 38025, null);
            Planet earth = new Planet(n_earth, 3, 40076, venus);
            Planet mars = new Planet(n_mars, 4, 21344, earth);
            PlanetCatalog catalog = new PlanetCatalog(venus, earth, mars);

            string[] check = { n_earth, n_lemony, n_mars, n_venus, n_mars, n_lemony };
            foreach (string s in check)
            {
                PrintResult(catalog.GetPlanet(s), s);
            }
        }
    }
}
