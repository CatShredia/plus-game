namespace PLUS_game
{
    class Game
    {
        public bool isGame = false;
        public Game()
        {
            isGame = true;
            Dangeon dangeon = new Dangeon();

            dangeon.GenerateLevel();

            // while (isGame)
            // {
                dangeon.WriteLevel();
            // }
        }
    }
}