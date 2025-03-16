using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWhoSimulation
{
    public class TimeLord : Creature
    {
        public Creature Companion { get; }
        public TimeLord(string name, int age, TARDIS tardis, Creature companion) : base(name, age)
        {
            IsTimeTraveler = true;
            Tardis = tardis;
            Companion = companion;

            // Реакція доктора на зміну статусів пльоту тардіс
            Tardis.FlightStatusChange += ReactToFlightStatusChange;

            // Підписка на переключання важелів доктором
            SwitchingLavers += () => companion.BoardTardis(Tardis);
            SwitchingLavers += Tardis.StartFlight;

            // Підписка на події тардіс
            Tardis.NotifyTimeJump(SubscribeTimeJump);
            Tardis.NotifyCloisterBell(ReactToCloisterBell);

            // Підписка на загрозу компаньйону
            Companion.NotifyThreat(ReactToThreatenedCompanion);
            Companion.death += ReactToCompanionDeath;
        }


        public TARDIS Tardis { get; }
        public bool TimeParadoxObserved { get; private set; } = false;

        // Подія перемикання важелів (підготовка до початку польоту
        // Метод подорождувати викликається тільки якщо доктор має компаньйона
        // І якщо не спостерігається часовий парадокс
        private event Action SwitchingLavers;
        public bool Travel()
        {
            if (Companion.IsAlive == false)
            {
                Log($"[{Name}] Неможливо подорожувати без друзів...");
                return false;
            }
            if (TimeParadoxObserved)
            {
                Log($"[{Name}] Неможливо подорожувати, поки не вирішиться часовий парадокс...");
                return false;
            }
            BoardTardis(Tardis);
            SwitchingLavers?.Invoke();
            return true;
        }

        public override string ToString()
        {
            return $"Ім'я: {Name}, Вік: {Age}, Тардіс: {Tardis.Model}, Компаньйон: {Companion.Name}";
        }

        public override void BoardTardis(TARDIS t)
        {
            Log($"[{Name}] Готуємось до польоту!");
        }

        // Доктор реагує по-різному на відомі йому планети
        public override void SubscribeTimeJump(object sender, TARDIS.TimeJumpEventArgs e)
        {
            if (e.Location == "Галіфрей")
            {
                Log($"[{Name}] Ми на Галіфреї! Це моя рідна планета!");
            }
            else if (e.Location == "Земля")
            {
                Log($"[{Name}] Ми на Землі! Це моя улюблена планета!");
            }
            else if (e.Location == "Скраро")
            {
                Log($"[{Name}] Ми на Скраро! Це планета далеків!");
            }
            else if (e.Location == "Мондас")
            {
                Log($"[{Name}] Ми на Мондасі! Це планета кіберлюдей!");
            }
            else if (e.Location == "Планета 14")
            {
                Log($"[{Name}] Ми на Планеті 14! Тут розвинувся один з підвидів кіберлюдей!");
            }
            else
            {
                base.SubscribeTimeJump(sender, e);
            }
            Log($"[{Name}] Зараз {e.Year} рік!");
        }

        private void ReactToFlightStatusChange(object sender, TARDIS.FlightStatus status)
        {
            if (status == TARDIS.FlightStatus.TakingOff)
            {
                Log($"[{Name}] Захисні енергетичні бар'єри активовано!");
                Log($"[{Name}] Cхоже ми взяли курс на нову планету!");
            }
        }

        private void ReactToThreatenedCompanion()
        {
            Log($"[{Name}] {Companion.Name} під загрозою! Повинен захистити її!");
            Log($"[{Name}] Швидше, {Companion.Name}, у Тардіс!");
        }

        private void ReactToCompanionDeath(Creature c)
        {
            Log($"[{Name}] {c.Name} померла. Я вже не можу подорожувати без неї. Лечу на Трензалор..");
        }

        private void ReactToCloisterBell()
        {
            Log($"[{Name}] Монастирський дзвін, ми занадто багато втручались у час...");
            Log($"[{Name}] Часовий парадокс, потрібно зачекати поки структура часу відновиться.");
            TimeParadoxObserved = true;
        }

        public override void Log(string message, ConsoleColor color = ConsoleColor.Blue)
        {
            base.Log(message, color);
        }
    }
}
