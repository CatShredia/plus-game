namespace PLUS_game
{

    using static System.Console;

    class Player : Object
    {
        public int maxHP = 100;

        public int[] Location;
        public int[] LastLocation;

        public string Room;

        public Item[] Inventory;

        public List<Weapon> weapons;

        public Player(int hp)
        {
            HP = hp;

            Location = new int[2];
            LastLocation = new int[2];

            Location[0] = 0;
            Location[1] = 0;

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

            if (Location[1] < CoefOfGame * 2)
            {
                Location[1]++;

                if ((CoefOfGame * 2) == Location[1])
                {
                    Location[1] = 0;
                    Location[0]++;
                }
            }

        }
        public void OpenInventory()
        {
            PrintWeapons();


            WriteLine("---Предметы---");
            WriteLine("Выберите предмет(введите порядковый номер, для выхода - 0):");


            for (int i = 0; i < Inventory.Length; i++)
            {
                WriteLine($"{i + 1}: {Inventory[i].Name} восстановит: {Inventory[i].Effect}HP ");
            }

            int number = Convert.ToInt32(ReadLine()) - 1;

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
            WriteLine("---Оружие---");

            for (int i = 0; i < weapons.Count; i++)
            {
                WriteLine($"{i + 1}: {weapons[i].Name} : {weapons[i].Damage} урона");
            }
        }

        public int Attack()
        {
            PrintWeapons();
            WriteLine("Скорее выбирай, чем ударишь!");

            int number = Convert.ToInt32(ReadLine()) - 1;

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
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}