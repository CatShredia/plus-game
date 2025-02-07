namespace PLUS_game
{

    using static System.Console;

    class Dangeon : Object
    {
        public string[,] Level;

        public int[] LevelSize;

        /*
            / - player or start room
            E - enemy
            C - chest
            T - trap
            S - store
            B - boss
        */
        public char[] TypeOfRoom = ['/', 'E', 'C', 'T', 'S', 'B'];

        public static bool isNewLevel = false;

        public Dangeon()
        {
            
        }
        public void GenerateLevel()
        {
            LevelSize = [CoefOfGame, CoefOfGame];
            Level = new string[LevelSize[0], LevelSize[1]];

            Game.player.Location = [0,0];
            Game.player.LastLocation = [0,0];

            for (int i = 0; i < LevelSize[0]; i++)
            {
                for (int j = 0; j < LevelSize[1]; j++)
                {
                    Level[i, j] = $"[{GetTypeOfRoom()}]";
                }
            }

            if (Game.LevelNumber == 1)
            {
                Level[0, 0] = $"[/]";
            }

            Level[LevelSize[0] - 1, LevelSize[1] - 1] = $"[B]";
        }
        public char GetTypeOfRoom()
        {
            Random random = new Random();

            int number = random.Next(1, TypeOfRoom.Length - 1);

            return TypeOfRoom[number];
        }
        public void WriteLevel()
        {
            PrintWithColor($"Уровень {Game.LevelNumber}", ConsoleColor.Black, ConsoleColor.DarkBlue);
            WriteLine();
            for (int i = 0; i < LevelSize[0]; i++)
            {
                for (int j = 0; j < LevelSize[1]; j++)
                {
                    ChoiseColor(Level[i, j]);
                    Write(Level[i, j]);
                    ForegroundColor = defaultForeground;

                    if (j == LevelSize[0] - 1)
                    {
                        Write("\n");
                    }
                }
            }
        }

        public void SetPlayer(Player player)
        {
            Level[player.Location[0], player.Location[1]] = "[/]";

            if (!((player.Location[0] == 0) && (player.Location[1] == 0)))
            {
                Level[player.LastLocation[0], player.LastLocation[1]] = $"[.]";
            }
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

            for (int i = 0; i < Game.weaponsCollection.Count; i++)
            {
                WriteLine($"{i + 1}: {Game.weaponsCollection[i].Name} наносит {Game.weaponsCollection[i].Damage} урона");
            }

            int number;
            for (int i = 0; i < 2; i++)
            {
                number = ReadIntFromPlayer("порядковый номер оружия") - 1;

                if (number >= 0 && number < Game.weaponsCollection.Count)
                {
                    PrintWithColor($"Выбран {Game.weaponsCollection[number].Name}", ConsoleColor.Black, ConsoleColor.DarkBlue);
                    Game.player.weapons.Add(Game.weaponsCollection[number]);
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
                string str = ReadStringFromPlayer("\'Не хотите ли вы купить зелье? (yes/no)\'");

                if (str.Equals("yes"))
                {
                    Game.player.Inventory[number] = new Item("Зелье от шапки", "зелье от торговца", 15);
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
            CoefOfGame += 1;
            isNewLevel = true;

            LevelSize = [CoefOfGame, CoefOfGame];
            Level = new string[LevelSize[0], LevelSize[1]];

            Game.LevelNumber++;

            PrintWithColor($"Переход на {Game.LevelNumber} этаж", ConsoleColor.Black, ConsoleColor.White);
            SetDefaultColor();

            Game.dangeon.GenerateLevel();
        }
    }
}