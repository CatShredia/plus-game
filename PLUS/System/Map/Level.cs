/* 
 Данный класс Level отвечает за генерацию и управление уровнем игры. 
 Он включает в себя методы для создания уровня, установки случайных комнат, 
 отображения уровня и управления местоположением игрока. 
 Класс использует массивы для хранения типов комнат и их количества, 
 а также случайный генератор для выбора комнат в процессе генерации уровня.
*/
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
            A - Altar
            B - boss
        */
        public char[] TypeOfRoom = ['/', 'E', 'C', 'T', 'S', 'A', 'B' ];
        public int[] RandomOfRoom = [ 0, 0, 0, 0, 0, 0, 0 ];
        public int CountOfRoom;
        public Random random;
        public Level(Game game)
        {
            Game = game;
            random = new Random();
        }
        public void GenerateLevel()
        {
            LevelSize = Game.CoefOfGame;
            LevelStr = new string[LevelSize, LevelSize];
            CountOfRoom = Game.CoefOfGame * Game.CoefOfGame;

            // Установка для проверки
            SetRandomOfRooms();
            for (int i = 0; i < LevelSize; i++)
            {
                for (int j = 0; j < LevelSize; j++)
                {
                    LevelStr[i, j] = $"[{GetRandomOfRoom(i, j)}]";
                    // LevelStr[i, j] = $"[A]";
                }
            }
            // LevelStr[0,0] = $"[/]";
        }
        public char GetRandomOfRoom(int i, int j)
        {
            while (true)
            {
                if (i == 0 && j == 0 && Game.LevelNumber == 1)
                {
                    RandomOfRoom[0]--;
                    return TypeOfRoom[0];
                }
                if (i == LevelSize - 1 && j == LevelSize - 1)
                {
                    RandomOfRoom[RandomOfRoom.Length - 1]--;
                    return TypeOfRoom[RandomOfRoom.Length - 1];
                }
                int number = random.Next(1, RandomOfRoom.Length - 1);
                if (RandomOfRoom[number] != 0)
                {
                    RandomOfRoom[number]--;
                    return TypeOfRoom[number];
                }
            }
        }
        public void SetRandomOfRooms()
        {
            for (int i = 0; i < RandomOfRoom.Length; i++)
            {
                // ! room - [/Start room]
                if (i == 0)
                {
                    if (Game.LevelNumber == 1)
                    {
                        RandomOfRoom[i] = 1; 
                    }
                    else
                    {
                        RandomOfRoom[i] = 0;
                    }
                }
                else if (i == 1)
                {
                    // ! room - [Enemy]
                    RandomOfRoom[i] = Game.LevelNumber * Game.LevelNumber; 
                }
                else if (i == 2)
                {
                    // ! room - [Chest]
                    RandomOfRoom[i] = (Game.LevelNumber - 1) * Game.LevelNumber; 
                }
                else if (i == 3)
                {
                    // ! room - [Trap]
                    RandomOfRoom[i] = (Game.LevelNumber - 1) * Game.LevelNumber - Game.LevelNumber;
                }
                else if (i == 4)
                {
                    // ! room - [Store / Shop]
                    RandomOfRoom[i] = Game.LevelNumber * Game.LevelNumber;
                }
                else if (i == 5)
                {
                    // ! room - [Altar]
                    RandomOfRoom[i] = (Game.LevelNumber - 1) * Game.LevelNumber - Game.LevelNumber;
                }
                else if (i == 6)
                {
                    // ! room - [Boss!!!]
                    RandomOfRoom[i] = 1; 
                }
                CountOfRoom -= RandomOfRoom[i];
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
    }
}