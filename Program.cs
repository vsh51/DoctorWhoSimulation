using System;
using System.Text;

namespace DoctorWhoSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Встановлення кодування консолі для коректного відображення кириличних символів
            Console.OutputEncoding = Encoding.UTF8;

            TARDIS tardis = new TARDIS("Type 40");
            TimeLord doctor = new TimeLord("Доктор", 1200, tardis, new Creature("Роуз Тайлер", 25));
            Enemy dalek = new Dalek("Далек", 1000);
            Enemy cyberman = new Cyberman("Кіберлюдина", 500);

            Console.WriteLine(doctor);
            Console.WriteLine(dalek);
            Console.WriteLine(cyberman);
            Console.WriteLine(tardis);
            Console.WriteLine();

            Random random = new Random();
            do
            {
                if (random.Next(0, 3) == 0)
                {
                    cyberman.Chase(doctor);
                }
                else
                {
                    dalek.Chase(doctor);
                }
            }
            while (doctor.Travel());
        }
    }
}
