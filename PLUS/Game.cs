namespace PLUS_game
{

    using static System.Console;

    class Game : Object
    {
        public static bool isGame = false;

        public static Player player;
        public static Dangeon dangeon;

        public static List<Weapon> weaponsCollection;

        public static int LevelNumber = 1;

        public Game()
        {
            Clear();

            PrintWithColor("Добро пожаловать в PLUS\n", ConsoleColor.Black, ConsoleColor.DarkBlue);

            isGame = true;
            player = new Player(100);
            dangeon = new Dangeon();

            weaponsCollection = [
                new Weapon("Меч", 10),
                new Weapon("Лук", 20),
                new Weapon("Арбалет", 40),
                new Weapon("Автомат", 1000),
                new Weapon("Заточка", 1),
            ];
            // -----
            dangeon.GenerateLevel();

            while (isGame == true)
            {
                player.Room = dangeon.Level[player.Location[0], player.Location[1]];

                dangeon.SetPlayer(player);

                dangeon.WriteLevel();

                Write("Комната игрока: ");
                // Write($"Местоположение {player.Location[0]}/{player.Location[1]} \n");
                // Write($"Местоположение last {player.LastLocation[0]}/{player.LastLocation[1]} \n");
                ChoiseColor(player.Room);
                WriteLine($"{player.Room}");
                ForegroundColor = defaultForeground;

                dangeon.Action(player.Room);

                if (Dangeon.isNewLevel == false)
                {
                    player.Move();
                }
                else
                {
                    Dangeon.isNewLevel = false;
                }
            }
        }

        public static void MonsterFight(Monster monster)
        {
            bool isFight = true;
            while (isFight)
            {
                PrintWithColor($"У вас: {player.HP}HP", ConsoleColor.Black, ConsoleColor.DarkRed);

                PrintWithColor($"У противника {monster.HP}HP", ConsoleColor.Black, ConsoleColor.DarkCyan);

                monster.HP -= player.Attack(); // удар игрока

                if (monster.isNullHP())
                {
                    isFight = false;

                    if (monster.Name == "BOSS")
                    {
                        dangeon.ToNewLevel();
                    }
                }
                else
                {
                    player.HP -= monster.Attack(); // удар монстра
                }
                if (player.isNullHP())
                {
                    isFight = false;
                    isGame = false;
                }
            }
        }

    }
}