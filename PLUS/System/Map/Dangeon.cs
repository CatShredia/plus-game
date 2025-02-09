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
            WriteLine("Вы наткнулись на монстра");

            Monster monster = new Monster("Монстр");

            Game.MonsterFight(monster);

            Activity();

        }
        public void ChestRoom()
        {
            WriteLine("Сундук начинает разговор");

            Chest chest = new Chest();

            Activity();

        }
        public void TrapRoom()
        {
            Random random = new Random();

            int number = random.Next(10, 40);

            WriteLine("О нет, ловушка...");
            WriteLine($"Вы поранились на {number}HP");

            Game.player.HP -= number;

            Activity();

        }
        public void StoreRoom()
        {
            WriteLine("Усатый торговец медленно подползает к вам, возможно пора освободить место");

            Activity();

            int number = Game.player.CheckPlaceToItem();
            if (number != -1)
            {
                string str = ReadStringFromPlayer("\'Не хотите ли вы купить зелье за 10 руб? (yes/no)\'");

                if (str.Equals("yes"))
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
            WriteLine("Чудовище преграждает вам дорогу");

            Monster monster = new Monster("BOSS");

            Game.MonsterFight(monster);

            Activity();
        }

        public void ToNewLevel()
        {
            // Game.CoefOfGame += 1;
            // Game.isNewLevel = true;

            // Level.LevelSize = [Game.CoefOfGame, Game.CoefOfGame];
            // Level.LevelStr = new string[Level.LevelSize[0], Level.LevelSize[1]];

            // Game.LevelNumber++;

            // PrintWithColor($"Переход на {Game.LevelNumber} этаж", ConsoleColor.Black, ConsoleColor.White);
            // SetDefaultColor();

        }
    }
}