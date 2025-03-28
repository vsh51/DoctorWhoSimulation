# **Doctor Who: Time Travel Simulation**

## **Опис проекту**

Ця програма моделює подорож Доктора у часі та просторі за допомогою TARDIS.
Доктор разом із компаньйоном потрапляє у випадкові епохи, де може зустріти ворогів (Далеки, Кіберлюди).


## **Lore**
Доктор - персонаж одноіменного британського науково-фантастичного серіалу "Доктор Хто". Сам серіал почав своє існування ще в далекому 1963 році й продовжується досі.
Головний герой, Доктор, є володарем часу з планети Галіфрей, має здатність до регенерації та подорожей у часі за допомогою космічного корабля TARDIS.
У його пригодах завжди є компаньйон - красива дівчина, яка допомагає йому в боротьбі з ворогами та вирішенні проблем.
Доктор пацифіст, тому коли є загроза, він використовує дипломатію, але ніколи не вбиває. Вороги самі пришвидшують свою загибель.
В цій симуляції коли настає небезпека, Доктор втікає на тардіс в пошуках кращої планети для боротьби.
Подорож закінчується коли його друг гине або коли стається часовий парадокс.
Далеки - це вороги Доктора, створені на планеті Скаро Давросом. Вони відомі своєю ненавистю до всього живого.
Кіберлюди - стадія еволюції людства, яка відбувається в майбутньому. На декількох планетах. У всіх паралельних вимірах.
Кібрелюди вважають, що органічна форма існування є недосконалою, тому вони намагаються модернізувати всіх живих істот, вони не мають статі та відчуттів.
Тардіс - тип космічного корабля, але разом з тим - жива істота, в серці якої протікає час. Тардіс доктора має вигляд Телефонної будки і не змінює маскування, оскільки була пошкодженa.

---

## **Функціонал**

- **TARDIS** генерує подію `TimeJump`, що повідомляє про зміну місця та часу, генерує подію "зміна статусу польоту"
- **Всі істоти** підписані на подію `TimeJump` і реагують на зміну епохи.
- **Companion** коментує подорожі, реагує на зміну статусу польоту, реагує на "перемикання важелів доктором", генерує події "смерть" та "небезпека", реагує на події "знищення" та "вдосконалення"
- **Доктор** реагує на зміну статусу польоту тардіс, генерує потію перемикання важелів
- **Enemy (Dalek / Cyberman)** може випадково з’явитися після стрибка у часі. (Умови описано коментарями в коді). Шанс нападу кіберлюдей значно менший ніж у далеків.
- **Enemy (Dalek / Cyberman)** атакують компаньйона, який може ухилитись від пострілу далека, але не може врятуватися від кіберлюдей.
- **Загибель компаньйона** призводить до завершення подорожі і сумної реакції Доктора.
- **TARDIS** може генерувати подію `Cloister Bell`, що означає можливий часовий парадокс, Доктор реагує на монастирський дзвін і завершує подорож.
- **Логування** подій у консоль із використанням різних кольорів для кращого сприйняття.
Детальніша схема подій додана тут: 

---

## **Приклад роботи програми**

```
Ім'я: Доктор, Вік: 1200, Тардіс: Type 40, Компаньйон: Роуз Тайлер
Ім'я: Далек, Вік: 1000
Ім'я: Кіберлюдина, Вік: 500
Модель: Type 40, Камуфляж: PoliceBox

[Далек] Знайдено ТАРДІС! Напевно там Доктор! Переслідуємо...
[Доктор] Готуємось до польоту!
[Роуз Тайлер] *звуки відчинення дверей*
[Роуз Тайлер] Вона більша всередині, ніж ззовні!
[Тардіс Type 40] Статус польоту: SettingCoordinates
[Тардіс Type 40] Статус польоту: TakingOff
[Доктор] Захисні енергетичні бар'єри активовано!
[Доктор] Cхоже ми взяли курс на нову планету!
[Тардіс Type 40] Статус польоту: InFlight
[Роуз Тайлер] Летимооо!
[Тардіс Type 40] Статус польоту: Landed
[Роуз Тайлер] Ми прибули!
[Роуз Тайлер] *звуки відчинення дверей*
[Доктор] Ми на Мондасі! Це планета кіберлюдей!
[Доктор] Зараз 2011 рік!
[Далек] Виявлено просторово-часову аномалію, сліди артронної енергії! Матеріалізуюся...
[Далек] Знищити! Знищити! Знищити!
[Далек] Знищити Роуз Тайлер! *звук пострілу з лазерної зброї*
[Роуз Тайлер] Що це за звуки!?
[Роуз Тайлер] Він промахнувся, втікаємо!
[Доктор] Роуз Тайлер під загрозою! Повинен захистити її!
[Доктор] Швидше, Роуз Тайлер, у Тардіс!
[Кіберлюдина] Знайдено ТАРДІС! Напевно там Доктор! Переслідуємо...
[Доктор] Готуємось до польоту!
[Роуз Тайлер] *звуки відчинення дверей*
[Роуз Тайлер] Я мандрівник у часі і просторі!
[Тардіс Type 40] Статус польоту: SettingCoordinates
[Тардіс Type 40] Статус польоту: TakingOff
[Доктор] Захисні енергетичні бар'єри активовано!
[Доктор] Cхоже ми взяли курс на нову планету!
[Тардіс Type 40] Статус польоту: InFlight
[Роуз Тайлер] Летимооо!
[Тардіс Type 40] Статус польоту: Landed
[Роуз Тайлер] Нова стара планета!
[Роуз Тайлер] *звуки відчинення дверей*
[Доктор] Ми на Планеті 14! Тут розвинувся один з підвидів кіберлюдей!
[Доктор] Зараз 2080 рік!
[Далек] Виявлено просторово-часову аномалію, сліди артронної енергії! Матеріалізуюся...
[Далек] Знищити! Знищити! Знищити!
[Далек] Знищити Роуз Тайлер! *звук пострілу з лазерної зброї*
[Роуз Тайлер] Що це за звуки!?
[Роуз Тайлер] Він промахнувся, втікаємо!
[Доктор] Роуз Тайлер під загрозою! Повинен захистити її!
[Доктор] Швидше, Роуз Тайлер, у Тардіс!
[Кіберлюдина] Виявлено просторово-часову аномалію! Матеріалізуюся...
[Кіберлюдина] Вітаю, Вас було обрано для вдосконалення!
[Кіберлюдина] Вдосконалити!
[Роуз Тайлер] Що це за звуки!?
[Роуз Тайлер] AAAA!
[Доктор] Роуз Тайлер померла. Я вже не можу подорожувати без неї. Лечу на Трензалор..
[Роуз Тайлер] Він вдосконалив мене! Я вдосконалений...
[Роуз Тайлер] Мої емоції... Вони зникли...
[Кіберлюдина] Вже переслідуємо Доктора!)
[Доктор] Неможливо подорожувати без друзів...
```

---

## **Логування**

- **Доктор** - блакитний
- **Компаньйон** - помаранчевий (злий вовк)
- **Далек** - червоний
- **Кіберлюдина** - білий
- **Тардіс** - жовтий

(в прикладі демонстрації на github не зображено можливостей логування, потрібно запустити код для перевірки)

---

![alt diagram](https://github.com/vsh51/DoctorWhoSimulation/blob/master/who.png?raw=true)

