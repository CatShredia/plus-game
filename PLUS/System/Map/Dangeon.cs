namespace PLUS_game
{

    using static System.Console;

    class Dangeon : Object
    {
        private Game Game;

        public Level Level;

        public Dangeon(Game game)
        {
            Game = game;

            Level = new Level(Game);
        }

        public void Action(string action)
        {
            WriteLine("---");
            switch (action[1])
            {
                case '/':
                    StartRoom();
                    break;
                case 'E':
                    EnemyRoom();
                    break;
                case 'C':
                    ChestRoom();
                    break;
                case 'T':
                    TrapRoom();
                    break;
                case 'S':
                    StoreRoom();
                    break;
                case 'A':
                    Altar();
                    break;
                case 'B':
                    BossRoom();
                    break;

                default:
                    PrintError("Действия не существует, Dangeon: 106");
                    break;
            }

        }

        public void Activity()
        {
            string active;

            bool isInventory = true;

            while (isInventory && Game.isGame)
            {
                PrintWithColor($"У вас: {Game.player.HP}HP", ConsoleColor.Black, ConsoleColor.DarkRed);
                PrintWithColor($"У вас: {Game.player.Wallet} руб", ConsoleColor.Black, ConsoleColor.DarkYellow);

                active = ReadStringFromPlayer("любой символ (e - инвентарь)").ToLower();

                if (active != "")
                {
                    switch (active[0])
                    {
                        case '0':
                            isInventory = false;
                            break;
                        case 'e':
                            Game.player.OpenInventory();
                            continue;
                        default:
                            isInventory = false;
                            break;
                    }
                }
            }
            WriteLine("---");
        }

        public void Altar()
        {
            ChoiseColor(Game.player.Room);
            WriteLine("Алтарь. Поменять 40 HP на 20 урона оружия");
            SetDefaultColor();

            if (ConfirmActionFromUser("использовать алтарь"))
            {
                Game.player.HP -= 40;
                int number = ReadIntFromPlayer("порядковый номер оружия");
                try
                {
                    if (number > 0 || number < 3)
                    {
                        Game.player.weapons[number - 1].Damage = Game.player.weapons[number - 1].Damage + 20;
                        Game.player.HP = Game.player.HP - 40;
                    }
                }
                catch (Exception e)
                {
                    PrintError("Проблема с Алтарем: 102\n" + e);
                }
            }

            Activity();
        }

        public void StartRoom()
        {
            ChoiseColor(Game.player.Room);
            WriteLine("Стартовая комната.");
            SetDefaultColor();

            for (int i = 0; i < Game.gameWeaponsCollection.Count; i++)
            {
                WriteLine($"{i + 1}: {Game.gameWeaponsCollection[i].Name} наносит {Game.gameWeaponsCollection[i].Damage} урона");
            }

            int number;
            for (int i = 0; i < 2; i++)
            {
                number = ReadIntFromPlayer("порядковый номер оружия") - 1;

                if (number >= 0 && number < Game.gameWeaponsCollection.Count)
                {
                    PrintWithColor($"Выбран {Game.gameWeaponsCollection[number].Name}", ConsoleColor.Black, ConsoleColor.DarkBlue);
                    WriteLine();
                    Game.player.weapons.Add(Game.gameWeaponsCollection[number]);
                }
                else
                {
                    PrintError("Такого оружия нет");
                    i--;
                }
            }

            Activity();

        }
        public void EnemyRoom()
        {
            ChoiseColor(Game.player.Room);
            WriteLine("Вы наткнулись на монстра.");
            SetDefaultColor();

            Monster monster = new Monster("Монстр", 10);

            Game.MonsterFight(monster);

            Activity();

        }
        public void ChestRoom()
        {
            ChoiseColor(Game.player.Room);
            WriteLine("Перед вами сундук.");
            SetDefaultColor();

            Chest chest = new Chest(Game);

            Activity();

        }
        public void TrapRoom()
        {
            ChoiseColor(Game.player.Room);
            WriteLine("О нет, ловушка...");
            SetDefaultColor();

            Random random = new Random();

            int number = random.Next(10, 40);

            WriteLine($"Вы поранились на {number}HP");

            Game.player.HP -= number;

            if (Game.player.isNullHP())
            {
                Game.isGame = false;
            }

            Activity();

        }
        public void StoreRoom()
        {
            ChoiseColor(Game.player.Room);
            WriteLine("Усатый торговец медленно подползает к вам, возможно пора освободить место");
            SetDefaultColor();

            Activity();

            int number = Game.player.CheckPlaceToItem();
            if (number != -1)
            {
                string str = ReadStringFromPlayer("\'Не хотите ли вы купить зелье за 10 руб? (y/n)\'");

                if (str.Equals("y"))
                {
                    if (Game.player.Wallet < 10)
                    {
                        PrintWithColor("Денег нет, на покупку зелья!", ConsoleColor.Black, ConsoleColor.Green);
                    }
                    else
                    {
                        Game.player.Wallet -= 10;
                        Game.player.Inventory[number] = new Item("Зелье от торгаша", "зелье от торговца", 15);
                    }
                }
            }
            else
            {
                WriteLine("И так места нет, пойду дальше");
            }



        }
        public void BossRoom()
        {
            ChoiseColor(Game.player.Room);
            WriteLine("Чудовище преграждает вам дорогу");
            SetDefaultColor();

            Monster monster = new Monster("BOSS", 30);

            Game.MonsterFight(monster);

            Activity();
        }

        public void ToNewLevel()
        {
            Game.CoefOfGame += 1;
            Game.LevelNumber++;

            Game.isNewLevel = true;

            Level.GenerateLevel();

            // Level.LevelSize = [Game.CoefOfGame, Game.CoefOfGame];
            // Level.LevelStr = new string[Level.LevelSize[0], Level.LevelSize[1]];


            PrintWithColor($"Переход на {Game.LevelNumber} этаж", ConsoleColor.Black, ConsoleColor.White);
            // SetDefaultColor();

            Game.player.SetLocation();
        }
    }
}