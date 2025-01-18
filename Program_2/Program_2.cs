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
        public (string name, int index, int equator, string error) GetPlanet(string name)
        {
            callCounter++;
            if (callCounter == 3)
            {
                callCounter = 0;
                return (name: null, index: 0, equator: 0, error: "Вы спрашиваете слишком часто");
            }

            foreach (Planet planet in planetsList)
            {
                if (planet.Name == name)
                {
                    return (name: name, index: planet.IndexNumber, equator: planet.EquatorLength, error: null);
                }
            }
            return (name: name, index: 0, equator: 0, error: "Не удалось найти планету");

        }
    }

    internal class Program_2
    {
        public static void PrintResult((string name, int index, int equator, string error) tuple)
        {
            if (tuple.name == null)
            {
                Console.WriteLine($"{tuple.error}\n");
            }
            else
            {
                Console.WriteLine($"Наименование планеты: {tuple.name}");
                if (tuple.error == null)
                {
                    Console.WriteLine($"Порядковый номер от Солнца: {tuple.index}");
                    Console.WriteLine($"Длина экватора: {tuple.equator} км\n");
                }
                else
                {
                    Console.WriteLine($"{tuple.error}\n");
                }
            }
        }
        static void Main(string[] args)
        {
            Planet venus = new Planet("Венера", 2, 38025, null);
            Planet earth = new Planet("Земля", 3, 40076, venus);
            Planet mars = new Planet("Марс", 4, 21344, earth);
            PlanetCatalog catalog = new PlanetCatalog(venus, earth, mars);

            PrintResult(catalog.GetPlanet("Земля"));
            PrintResult(catalog.GetPlanet("Лимония"));
            PrintResult(catalog.GetPlanet("Марс"));

            PrintResult(catalog.GetPlanet("Венера"));
            PrintResult(catalog.GetPlanet("Марс"));
            PrintResult(catalog.GetPlanet("Лимония"));
        }
    }
}
