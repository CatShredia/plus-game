namespace PLUS_game
{
    using System.Runtime.CompilerServices;
    using static System.Console;

    class Level : Object
    {
        private Game Game;
        public string[,] LevelStr;

        public int LevelSize;

        /*
            / - player or start room
            E - enemy
            C - chest
            T - trap
            S - store
            B - boss
        */
        public char[] TypeOfRoom = ['/', 'E', 'C', 'T', 'S', 'B'];
        public int[] RandomOfRoom = [0, 0, 0, 0, 0, 0];
        public int CountOfRoom;

        public Level(Game game)
        {
            Game = game;
        }

        public void GenerateLevel()
        {
            WriteLine($"Генерация левела {Game.CoefOfGame} коэф");

            LevelSize = Game.CoefOfGame;

            LevelStr = new string[LevelSize, LevelSize];

            CountOfRoom = Game.CoefOfGame * Game.CoefOfGame;
            SetRandomOfRooms();

            for(int i = 0; i < LevelSize; i++) {
                for(int j = 0; j < LevelSize; j++) {
                    WriteLine($"i: {i}/ j: {j}");

                    LevelStr[i,j] = $"[{GetRandomOfRoom(i,j)}]";
                }
            }

            WriteLine();
        }
        public char GetRandomOfRoom(int i, int j) {

            if(i == 0 && j == 0) {
                RandomOfRoom[0]--;
                return TypeOfRoom[0];
            }
            if(i == LevelSize - 1 && j == LevelSize - 1) {
                RandomOfRoom[RandomOfRoom.Length - 1]--;
                return TypeOfRoom[RandomOfRoom.Length - 1];
            }

            Random random = new Random();
            int number = random.Next(1, RandomOfRoom.Length - 1);

            if(RandomOfRoom[number] != 0) {
                RandomOfRoom[number]--;
                return TypeOfRoom[number];
            } else {
                return GetRandomOfRoom(i,j);
            }
        }
        public void SetRandomOfRooms()
        {
            for (int i = 0; i < RandomOfRoom.Length; i++)
            {
                // ! room - [/Start room]
                if (i == 0)
                {
                    RandomOfRoom[i] = 1; // 1
                }
                else if (i == 1)
                {
                    // ! room - [Enemy]
                    RandomOfRoom[i] = Game.CoefOfGame / 2; // 1 -> 2
                }
                else if (i == 2)
                {
                    // ! room - [Chest]
                    RandomOfRoom[i] = Game.CoefOfGame - 2; // 0 -> 2
                }
                else if (i == 4)
                {
                    // ! room - [Store / Shop]
                    RandomOfRoom[i] = Game.CoefOfGame / 2; // 0 -> 2
                }
                else if (i == 5)
                {
                    // ! room - [Boss!!!]
                    RandomOfRoom[i] = 1; // 1
                }

                CountOfRoom -= RandomOfRoom[i];
            }

            if (CountOfRoom != 0)
            {
                RandomOfRoom[3] = CountOfRoom; // 1
                CountOfRoom = 0;
            }
        }
        public void WriteLevel()
        {
            PrintWithColor($"Уровень {Game.LevelNumber}", ConsoleColor.Black, ConsoleColor.DarkBlue);
            WriteLine();
            for (int i = 0; i < LevelSize; i++)
            {
                for (int j = 0; j < LevelSize; j++)
                {
                    ChoiseColor(LevelStr[i, j]);
                    Write(LevelStr[i, j]);
                    ForegroundColor = defaultForeground;

                    if (j == LevelSize - 1)
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

        // public void GenerateLevel()
        // {
        //     LevelSize = [Game.CoefOfGame, Game.CoefOfGame];
        //     LevelStr = new string[LevelSize[0], LevelSize[1]];

        //     Game.player.Location = [0, 0];
        //     Game.player.LastLocation = [0, 0];

        //     for (int i = 0; i < LevelSize[0]; i++)
        //     {
        //         for (int j = 0; j < LevelSize[1]; j++)
        //         {
        //             LevelStr[i, j] = $"[{GetTypeOfRoom()}]";
        //         }
        //     }

        //     if (Game.LevelNumber == 1)
        //     {
        //         LevelStr[0, 0] = $"[/]";
        //     }

        //     LevelStr[LevelSize[0] - 1, LevelSize[1] - 1] = $"[B]";
        // }
        // public char GetTypeOfRoom()
        // {
        //     Random random = new Random();

        //     int number = random.Next(1, TypeOfRoom.Length - 1);

        //     return TypeOfRoom[number];
        // }
    }
}