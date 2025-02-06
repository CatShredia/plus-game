namespace PLUS_game
{
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
            ];
            // -----
            dangeon.GenerateLevel();

            while (isGame == true)
            {
                player.Room = dangeon.Level[player.Location[0], player.Location[1]];

                dangeon.SetPlayer(player);
                dangeon.WriteLevel();

                dangeon.Action(player.Room);

                player.Move();
            }
        }
    }
}