namespace PLUS_game
{
    class Game : Object
    {
        public static bool isGame = false;

        public static Player player;
        public static Dangeon dangeon;

        public Game()
        {
            isGame = true;
            dangeon = new Dangeon();
            player = new Player(100 * CoefOfGame);

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