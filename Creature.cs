using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWhoSimulation
{
    public class Creature
    {
        // Конструктор
        public Creature(string name, int age)
        {
            Name = name;
            Age = age;
        }


        // Базові властивості істоти
        public string Name { get; }
        public int Age { get; }
        public bool IsTimeTraveler { get; protected set; } = false;
        public UInt16 Agility { get; protected set; } = 6;


        // Подія смерть і відповідна властивість в істоти
        // На смерть підписується тардіс (при посадці) і доктор (при знайомстві)
        public Action<Creature> death;
        private bool _isAlive = true;
        public void Die()
        {
            Log($"[{Name}] AAAA!");
            _isAlive = false;
            death?.Invoke(this);
        }
        public bool IsAlive => _isAlive;


        // Подія загрози і відповідна властивість в істоти
        // На загрозу підписується доктор
        private Action Threat;
        public void NotifyThreat(Action action)
        {
            Threat += action;
        }


        // Список можливих фраз які може сказати істота при посадці в ТАРДІС
        private readonly List<string> board_phrases = new List<string>()
        {
            "Це не телефонна будка!",
            "Це не телефонна будка! Це ТАРДІС!",
            "Я мандрівник у часі і просторі!"
        };

        // Список можливих фраз при приземленні ТАРДІС
        private readonly List<string> landed_phrases = new List<string>()
        {
            "Це була довга подорож...",
            "Ми приземлились!",
            "Ми на місці!",
            "Ми прибули!",
            "Нова стара планета!"
        };


        // Посадка істоти в ТАРДІС
        // При посадці, ТАРДІС підписується на смерть істоти
        // Істота підписується на подію зміни статусу польоту ТАРДІС
        public virtual void BoardTardis(TARDIS t)
        {
            death += t.SubscribeToPassengerDeath;
            t.FlightStatusChange += SubscribeToFlightStatusChange;
            Log($"[{Name}] *звуки відчинення дверей*");
            t.Passengers.Add(this);
            if (!IsTimeTraveler)
            {
                Log($"[{Name}] Вона більша всередині, ніж ззовні!");
                IsTimeTraveler = true;
            }
            else
            {
                Log($"[{Name}] {board_phrases[new Random().Next(0, board_phrases.Count)]}");
            }
        }

        // Висадка істоти з ТАРДІС
        // При висадці, ТАРДІС відписується від смерті істоти
        // Істота відписується від події зміни статусу польоту ТАРДІС
        public void LeaveTardis(TARDIS t)
        {
            death -= t.SubscribeToPassengerDeath;
            t.FlightStatusChange -= SubscribeToFlightStatusChange;

            Log($"[{Name}] *звуки відчинення дверей*");
            t.Passengers.Remove(this);
        }


        // Підписка на подію подорожі в часі
        public virtual void SubscribeTimeJump(object sender, TARDIS.TimeJumpEventArgs e)
        {
            Log($"[{Name}] Ого! ми на планеті {e.Location}, але коли..?");
        }


        // Підписка на подію зміни статусу польоту ТАРДІС
        public void SubscribeToFlightStatusChange(object sender, TARDIS.FlightStatus e)
        {
            if (e == TARDIS.FlightStatus.Landed)
            {
                Log($"[{Name}] {landed_phrases[new Random().Next(0, landed_phrases.Count)]}");
                LeaveTardis(sender as TARDIS);
            }
            if (e == TARDIS.FlightStatus.InFlight)
            {
                Log($"[{Name}] Летимооо!");
            }
        }


        // Підписка на знищення далеком (чим краща спритність, тим більші шанси ухилитись від пострілу)
        public bool SubscribeExtermination(Creature c)
        {
            Log($"[{Name}] Що це за звуки!?");
            if (new Random().Next(0, Agility) == 0)
            {
                Die();
                return true;
            }
            else
            {
                Log($"[{Name}] Він промахнувся, втікаємо!");
                Threat?.Invoke();
            }
            return false;
        }


        // Підписка на вдосконалення кіберлюдиною
        // Від вдосконалення неможливо втекти
        public void SubscribeUpgrade(Creature c)
        {
            Log($"[{Name}] Що це за звуки!?");
            Die();
            Log($"[{Name}] Він вдосконалив мене! Я вдосконалений...");
            Log($"[{Name}] Мої емоції... Вони зникли...");
        }

        // Перевизначення методу ToString
        public override string ToString()
        {
            return $"Ім'я: {Name}, Вік: {Age}";
        }

        // Метод для виведення повідомлення на консоль окремим кольором
        public virtual void Log(string message, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
