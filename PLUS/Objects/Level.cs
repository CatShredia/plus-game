namespace PLUS_game
{
    using static System.Console;

    class Level : Object
    {
        private Game Game;
        public string[,] LevelStr;

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

        public Level(Game game)
        {
            Game = game;
        }

        // public void GenerateLevel()
        // {
        //     WriteLine("Генерация левела");
        // }

        public void WriteLevel()
        {
            PrintWithColor($"Уровень {Game.LevelNumber}", ConsoleColor.Black, ConsoleColor.DarkBlue);
            WriteLine();
            for (int i = 0; i < LevelSize[0]; i++)
            {
                for (int j = 0; j < LevelSize[1]; j++)
                {
                    ChoiseColor(LevelStr[i, j]);
                    Write(LevelStr[i, j]);
                    ForegroundColor = defaultForeground;

                    if (j == LevelSize[0] - 1)
                    {
                        Write("\n");
                    }
                }
            }
        }

        // устанавливаем текущее местоположение и предыдущее местоположение
        public void SetPlayer()
        {
            LevelStr[Game.player.Location[0], Game.player.Location[1]] = "[/]";

            if (!((Game.player.Location[0] == 0) && (Game.player.Location[1] == 0)))
            {
                LevelStr[Game.player.LastLocation[0], Game.player.LastLocation[1]] = $"[.]";
            }
        }

        // TODO: переписать

        public void GenerateLevel()
        {
            LevelSize = [Game.CoefOfGame, Game.CoefOfGame];
            LevelStr = new string[LevelSize[0], LevelSize[1]];

            Game.player.Location = [0, 0];
            Game.player.LastLocation = [0, 0];

            for (int i = 0; i < LevelSize[0]; i++)
            {
                for (int j = 0; j < LevelSize[1]; j++)
                {
                    LevelStr[i, j] = $"[{GetTypeOfRoom()}]";
                }
            }

            if (Game.LevelNumber == 1)
            {
                LevelStr[0, 0] = $"[/]";
            }

            LevelStr[LevelSize[0] - 1, LevelSize[1] - 1] = $"[B]";
        }
        public char GetTypeOfRoom()
        {
            Random random = new Random();

            int number = random.Next(1, TypeOfRoom.Length - 1);

            return TypeOfRoom[number];
        }
    }
}