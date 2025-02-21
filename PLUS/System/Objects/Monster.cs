/* Класс Monster представляет собой монстра в игре, с атрибутами имени, урона и здоровья. 
 Конструктор инициализирует здоровье и урон в зависимости от имени монстра, 
 с особыми значениями для монстра по имени "BOSS". 
 Метод Attack возвращает случайный урон, а метод isNullHP проверяет, 
 является ли здоровье монстра меньше 1.
*/ 

namespace PLUS_game
{
    using static System.Console;
    class Monster : Object
    {
        public string Name;
        public int Damage;
        public int Cost;

        public Monster(string name, int cost)
        {
            Random random = new Random();
            
            Name = name;
            Cost = cost;

            if (name.Equals("BOSS"))
            {
                HP = random.Next(100, 150);
                Damage = random.Next(10, 20);
            }
            else
            {
                HP = random.Next(20, 50);
                Damage = random.Next(1, 10);
            }
        }

        public int Attack()
        {
            Random random = new Random();
            return random.Next(1, 3) * 10;
        }

        public bool isNullHP()
        {
            if (HP < 1) { return true; }
            else { return false; }
        }
    }
}