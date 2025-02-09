/* 
 Класс Player представляет игрока в игре, управляя его состоянием, перемещением, инвентарем и взаимодействиями. 
 Он включает методы для перемещения, открытия инвентаря, проверки наличия предметов, атаки и проверки здоровья игрока. 
 Игрок может хранить предметы и оружие, а также восстанавливать здоровье с помощью предметов.
*/
namespace PLUS_game
{
    using System.Diagnostics.Tracing;
    using static System.Console;
    class Player : Object
    {
        private Game Game;
        public int maxHP;
        public int[] Location = { 0, 0 };
        public int[] LastLocation = { 0, 0 };
        public string Room;
        public Item[] Inventory;
        public List<Weapon> weapons;
        public int Wallet = 10;
        public Player(Game game, int hp)
        {
            Game = game;
            maxHP = hp;
            HP = maxHP;
            Inventory = new Item[5];
            weapons = new List<Weapon>();
            // добавление изначальных предметов
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (i < 3)
                {
                    Inventory[i] = new Item("Зелье", "чудотворное", 20);
                }
                else
                {
                    Inventory[i] = new Item("Пустой карман", "ну, воздух на самом деле", 0);
                }
            }
        }
        public void Move()
        {
            LastLocation[0] = Location[0];
            LastLocation[1] = Location[1];
            if (Location[1] < Game.dangeon.Level.LevelSize)
            {
                Location[1]++;
                if (Game.dangeon.Level.LevelSize == Location[1])
                {
                    Location[1] = 0;
                    Location[0]++;
                }
            }
        }
        public void OpenInventory()
        {
            PrintWeapons();
            PrintWithColor("Предметы", ConsoleColor.Black, ConsoleColor.DarkBlue);
            for (int i = 0; i < Inventory.Length; i++)
            {
                WriteLine($"{i + 1}: {Inventory[i].Name} восстановит: {Inventory[i].Effect}HP ");
            }
            int number = ReadIntFromPlayer("порядковый номер, для выхода - 0") - 1;
            if (number != -1)
            {
                if (HP < maxHP)
                {
                    if (HP + Inventory[number].Effect >= maxHP)
                    {
                        HP = maxHP;
                    }
                    else
                    {
                        HP += Inventory[number].Effect;
                    }
                    Inventory[number] = new Item("Пустой карман", "ну, воздух на самом деле", 0);
                }
                else
                {
                    WriteLine("Крайне расточительно использовать сейчас лекарство!");
                }
            }
        }
        public int CheckPlaceToItem()
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].Name == "Пустой карман")
                {
                    return i;
                }
            }
            return -1;
        }
        public void PrintWeapons()
        {
            PrintWithColor("Оружие", ConsoleColor.Black, ConsoleColor.DarkBlue);
            for (int i = 0; i < weapons.Count; i++)
            {
                WriteLine($"{i + 1}: {weapons[i].Name} : {weapons[i].Damage} урона");
            }
        }
        public int Attack()
        {
            PrintWeapons();
            WriteLine("Скорее выбирай, чем ударишь!");
            int number = ReadIntFromPlayer("порядковый номер оружия") - 1;
            if (number >= 0 && number < weapons.Count)
            {
                return weapons[number].Damage;
            }
            else
            {
                WriteLine("Долго думаешь.");
                return 0;
            }
        }
        public bool isNullHP()
        {
            if (HP < 1)
            {
                PrintDefeate();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}