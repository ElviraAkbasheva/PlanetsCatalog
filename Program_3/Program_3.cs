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
        public (string name, int index, int equator, string error) GetPlanet(string name, Validator method)
        {
            var valResult = method(name);

            if (valResult.Item1)
            {
                return (name: null, index: 0, equator: 0, error: valResult.Item2);
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

    delegate (bool, string) Validator(string name);

    internal class Program_3
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
            int callCounter = 0;

            Planet venus = new Planet("Венера", 2, 38025, null);
            Planet earth = new Planet("Земля", 3, 40076, venus);
            Planet mars = new Planet("Марс", 4, 21344, earth);
            PlanetCatalog catalog = new PlanetCatalog(venus, earth, mars);

            Validator callVal = (name) =>
            {
                callCounter++;
                if (callCounter == 3)
                {
                    callCounter = 0;
                    return (res: true, err: "Вы спрашиваете слишком часто");
                }
                return (res: false, err: null);
            };

            Validator planetVal = (name) =>
            {
                if (name == "Лимония")
                {
                    return (res: true, err: "Это запретная планета");
                }
                return (res: false, err: null);
            };

            PrintResult(catalog.GetPlanet("Земля", callVal));
            PrintResult(catalog.GetPlanet("Лимония", callVal));
            PrintResult(catalog.GetPlanet("Марс", callVal));

            PrintResult(catalog.GetPlanet("Венера", planetVal));
            PrintResult(catalog.GetPlanet("Лимония", planetVal));
            PrintResult(catalog.GetPlanet("Марс", planetVal));
        }
    }
}
