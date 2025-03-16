using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorWhoSimulation
{
    public class Enemy : Creature
    {
        public Enemy(string name, int age) : base(name, age) { }

        public TimeLord DoctorWho { get; private set; }

        // Лиходій починає слідкувати за переміщеннями тардіс Доктора
        public virtual void Chase(TimeLord tl)
        {
            if (DoctorWho == null)
            {
                DoctorWho = tl;
                DoctorWho.Tardis.NotifyTimeJump(SubscribeTimeJump);
            }
        }
    }

    public class Dalek : Enemy
    {
        public readonly List<(int, int)> invaded_epochs = new List<(int, int)>
        {
            (2000, 2100),
            (2300, 2400),
            (3000, 3100),
            (3500, 4000),
            (4100, 4300)
        };

        public Dalek(string name, int age) : base(name, age) { }

        // Функціонал знищення жертви
        // Жертва сама слідкує за тим чи попав у неї далек пострілом з бластера і "повідомляє" йому це
        public event Func<Creature, bool> Extermination;
        public bool Exterminate(Creature creature)
        {
            return Extermination(creature);
        }
        public override void SubscribeTimeJump(object sender, TARDIS.TimeJumpEventArgs e)
        {
            // Далеки можуть матеріалізуватися лише в певних епохах, які вони вже захопили
            if (invaded_epochs.Exists(epoch => (e.Year >= epoch.Item1 && e.Year <= epoch.Item2)))
            {
                Log($"[Далек] Виявлено просторово-часову аномалію, сліди артронної енергії! Матеріалізуюся...");
                Log($"[Далек] Знищити! Знищити! Знищити!");
                if (DoctorWho.Companion.IsAlive)
                {
                    Creature victim = DoctorWho.Companion;
                    Extermination -= victim.SubscribeExtermination;
                    Extermination += victim.SubscribeExtermination;
                    Log($"[Далек] Знищити {victim.Name}! *звук пострілу з лазерної зброї*");
                    if (Exterminate(victim))
                    {
                        Log($"[Далек] {victim.Name} знищено.");
                    }
                }
                else
                {
                    Log($"[Далек] Немає жодної живої цілі для знищення...");
                }
            }
        }

        // Довизначення методу переслідування для гарного логування відповідним кольором
        public override void Chase(TimeLord tl)
        {
            if (DoctorWho != null)
            {
                Log($"[{Name}] Вже переслідуємо Доктора!)");
            }
            else
            {
                Log($"[{Name}] Знайдено ТАРДІС! Напевно там Доктор! Переслідуємо...");
            }
            base.Chase(tl);
        }

        public override void Log(string message, ConsoleColor color = ConsoleColor.Red)
        {
            base.Log(message, color);
        }
    }

    public class Cyberman : Enemy
    {
        public Cyberman(string name, int age) : base(name, age) { }

        // Функціонал вдосконалення жертви
        public event Action<Creature> Upgrade;
        public void UpgradeCreature(Creature creature)
        {
            Log($"[Кіберлюдина] Вдосконалити!");
            Upgrade?.Invoke(creature);
        }

        public override void SubscribeTimeJump(object sender, TARDIS.TimeJumpEventArgs e)
        {
            // Кіберлюди можуть матеріалізуватися лише на Мондасі або Планеті 14 (їхніх планетах)
            if (e.Location == "Мондас" || e.Location == "Планета 14")
            {
                Log($"[Кіберлюдина] Виявлено просторово-часову аномалію! Матеріалізуюся...");
                Creature victim = DoctorWho.Companion;
                if (victim.IsAlive)
                {
                    Log($"[Кіберлюдина] Вітаю, Вас було обрано для вдосконалення!");
                    Upgrade -= victim.SubscribeUpgrade;
                    Upgrade += victim.SubscribeUpgrade;
                    UpgradeCreature(victim);
                }
                else
                {
                    Log($"[Кіберлюдина] Неможливо вдосконалити мертвого пасажира...");
                }
            }
        }

        // Довизначення методу переслідування для гарного логування відповідним кольором
        public override void Chase(TimeLord tl)
        {
            if (DoctorWho != null)
            {
                Log($"[{Name}] Вже переслідуємо Доктора!)");
            }
            else
            {
                Log($"[{Name}] Знайдено ТАРДІС! Напевно там Доктор! Переслідуємо...");
            }
            base.Chase(tl);
        }

        public override void Log(string message, ConsoleColor color = ConsoleColor.White)
        {
            base.Log(message, color);
        }
    }
}
