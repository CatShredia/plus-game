namespace PLUS_game
{

    using static System.Console;

    class Game : Object
    {
        public static bool isGame = false;

        public static Player player;
        public static Dangeon dangeon;

        public static List<Weapon> weaponsCollection;

        public Game()
        {
            Clear();
            WriteLine("----");

            PrintHello();

            isGame = true;
            dangeon = new Dangeon();
            player = new Player(100);

            weaponsCollection = [
                new Weapon("Меч", 10),
                new Weapon("Лук", 20),
                new Weapon("Арбалет", 40),
                new Weapon("Автомат", 100),
                new Weapon("Заточка", 1),
            ];
            // -----
            dangeon.GenerateLevel();

            while (isGame == true)
            {
                player.Room = dangeon.Level[player.Location[0], player.Location[1]];

                dangeon.SetPlayer(player);
                WriteLine("----");
                dangeon.WriteLevel();
                WriteLine("----");
                Write("Комната игрока: ");
                ChoiseColor(player.Room);
                WriteLine($"{player.Room}");
                ForegroundColor = defaultForeground;
                WriteLine("----");
                dangeon.Action(player.Room);

                player.Move();
            }
        }

        public static void MonsterFight(Monster monster)
        {
            bool isFight = true;
            while (isFight)
            {
                WriteLine($"У вас {player.HP}HP");
                WriteLine($"У противника {monster.HP}HP");

                monster.HP -= player.Attack(); // удар игрока

                if (monster.isNullHP())
                {
                    isFight = false;
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
                WriteLine("---");
            }
        }

    }
}