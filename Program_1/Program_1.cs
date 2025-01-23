

namespace AnonymousType
{
    internal class AnonymousType
    {
        public void SolarSystemPlanets()
        {
            var venus = new
            {
                Name = "Венера",
                IndexNumber = 2,
                EquatorLength = 38025,
                PreviousPlanet = (Type)null
            };
            var earth = new
            {
                Name = "Земля",
                IndexNumber = 3,
                EquatorLength = 40076,
                PreviousPlanet = venus
            };
            var mars = new
            {
                Name = "Марс",
                IndexNumber = 4,
                EquatorLength = 21344,
                PreviousPlanet = earth
            };
            var venus_second = new
            {
                Name = "Венера",
                IndexNumber = 2,
                EquatorLength = 38025,
                PreviousPlanet = (Type)null
            };

            Print(venus);
            Print(earth);
            Print(mars);
            Print(venus_second);

            void Print(dynamic anonim)
            {
                Console.WriteLine($"Название планеты: {anonim.Name}");
                Console.WriteLine($"Порядковый номер от Солнца: {anonim.IndexNumber}");
                Console.WriteLine($"Длина экватора: {anonim.EquatorLength} км");
                Console.WriteLine($"Предыдущая планета: {(anonim.PreviousPlanet == null ? "нет" : anonim.PreviousPlanet.Name)}");
                Console.WriteLine($"Эквивалентна Венере? {(anonim.Equals(venus) == false ? "нет" : "да")}\n");
            }
        }
    }
    internal class Program_1
    {
        static void Main(string[] args)
        {
            var ant = new AnonymousType();
            ant.SolarSystemPlanets();
        }
    }
}
