using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWhoSimulation
{
    // Доктор говорив з тардіс, вона в якомусь сенсі істота
    public class TARDIS : Creature
    {
        public TARDIS(string model) : base("ТАРДІС", int.MaxValue)
        {
            Model = model;
        }

        // Інформація про стрибок у часі
        public class TimeJumpEventArgs : EventArgs
        {
            public TimeJumpEventArgs(string location, int year)
            {
                Location = location;
                Year = year;
            }

            public string Location { get; }
            public int Year { get; }
        }

        // Базові характеристики і властивості ТАРДІС
        public List<Creature> Passengers { get; } = new List<Creature>();
        public enum CamouflageType
        {
            PoliceBox,
            House
        };
        private List<(string, int)> visited_planets = new List<(string, int)>();
        public string Model { get; }
        public CamouflageType Camouflage { get; }
        public (int, int) SupportedTimeRange { get; private set; } = (2000, 2100);

        // Список можливих пунктів призначення
        private readonly List<string> destinations = new List<string>(){
            "Галіфрей",
            "Скраро",
            "Земля",
            "Мондас",
            "Планета 14",
        };

        // Подія стрибка у часі
        // На подію підписуються істоти: Доктор, Далеки, Кіберлюди, компаньйон доктора
        // Кожен реагує по-своєму на подію
        private event EventHandler<TimeJumpEventArgs> TimeJump;
        public void NotifyTimeJump(EventHandler<TimeJumpEventArgs> handler)
        {
            TimeJump += handler;
        }

        // Монастирський дзвін, реакція тардіс на часові флуктуації
        private Action CloisterBell;
        public void NotifyCloisterBell(Action action)
        {
            CloisterBell += action;
        }

        // EventHandler, бо потрібно мати інормацію про об'єкт, який викликав подію
        public event EventHandler<FlightStatus> FlightStatusChange;
        public enum FlightStatus
        {
            SettingCoordinates,
            TakingOff,
            InFlight,
            Landed
        }
        public FlightStatus _status = FlightStatus.Landed;
        public FlightStatus Status
        {
            get => _status;
            private set
            {
                _status = value;
               Log($"[Тардіс {Model}] Статус польоту: {value}");
                FlightStatusChange?.Invoke(this, value);
            }
        }


        public void StartFlight()
        {
            Status = FlightStatus.SettingCoordinates;
            Random random = new Random();
            string planet = destinations[random.Next(0, destinations.Count)];
            int year = random.Next(SupportedTimeRange.Item1, SupportedTimeRange.Item2);
            TakeOff(planet, year);
        }

        private void TakeOff(string planet, int year)
        {
            Status = FlightStatus.TakingOff;
            Status = FlightStatus.InFlight;
            Status = FlightStatus.Landed;
            TimeJump?.Invoke(this, new TimeJumpEventArgs(planet, year));

            // Якщо планету відвідано більше ніж 3 рази, то стається часовий парадокс
            if (!visited_planets.Exists(p => p.Item1 == planet))
            {
                visited_planets.Add((planet, 1));
            }
            else
            {
                int index = visited_planets.FindIndex(p => p.Item1 == planet);
                if (index != -1)
                {
                    visited_planets[index] = (visited_planets[index].Item1, visited_planets[index].Item2 + 1);
                }
            }

            if (visited_planets.Exists(p => p.Item2 > 3))
            {
                CloisterBell?.Invoke();
            }
        }

        public override string ToString()
        {
            return $"Модель: {Model}, Камуфляж: {Camouflage}";
        }

        // Якщо пасажир помер, він видаляється зі списку пасажирів
        // Такого пасажира не потрібно оповіщати про стрибок у часі і зміну статусу польоту
        public void SubscribeToPassengerDeath(Creature c)
        {
            Passengers.Remove(c);
            TimeJump -= c.SubscribeTimeJump;
            FlightStatusChange -= c.SubscribeToFlightStatusChange;
        }

        public override void Log(string message, ConsoleColor color = ConsoleColor.Yellow)
        {
            base.Log(message, color);
        }
    }
}
