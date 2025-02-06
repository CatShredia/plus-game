namespace PLUS_game
{
    class Game : Object
    {
        public static bool isGame = false;

        public Game()
        {
            isGame = true;
            Dangeon dangeon = new Dangeon();
            Player player = new Player(100 * CoefOfGame);

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