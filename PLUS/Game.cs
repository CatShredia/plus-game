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
            isGame = true;
            dangeon = new Dangeon();
            player = new Player(100 * CoefOfGame);

            weaponsCollection = [
                new Weapon("Меч", 10),
                new Weapon("Лук", 20),
                new Weapon("Арбалет", 40),
                new Weapon("Автомат", 100),
                new Weapon("Заточка", 1),
            ];
            // -----
            dangeon.GenerateLevel();

            // TODO: не забыть убрать!
            dangeon.Level[0,1] = $"[S]";

            while (isGame == true)
            {
                player.Room = dangeon.Level[player.Location[0], player.Location[1]];

                dangeon.SetPlayer(player);
                dangeon.WriteLevel();

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
                if(player.isNullHP()) {
                    isFight = false;
                    isGame = false;
                }
                
            }
        }
    }
}