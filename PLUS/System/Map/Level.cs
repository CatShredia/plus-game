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
        public char[] TypeOfRoom = ['/', 'E', 'C', 'T', 'S', 'A', 'B'];
        public int[] RandomOfRoom = [0, 0, 0, 0, 0, 0, 0];
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

            // Инициализация уровня пустыми комнатами
            for (int i = 0; i < LevelSize; i++)
            {
                for (int j = 0; j < LevelSize; j++)
                {
                    LevelStr[i, j] = "[.]";
                }
            }

            // Начальная позиция
            int x = LevelSize / 2;
            int y = LevelSize / 2;

            // Количество шагов
            int steps = LevelSize * LevelSize * 2;

            for (int i = 0; i < steps; i++)
            {
                // Случайное направление
                int direction = random.Next(0, 4);
                switch (direction)
                {
                    case 0: x = Math.Max(0, x - 1); break; // Влево
                    case 1: x = Math.Min(LevelSize - 1, x + 1); break; // Вправо
                    case 2: y = Math.Max(0, y - 1); break; // Вверх
                    case 3: y = Math.Min(LevelSize - 1, y + 1); break; // Вниз
                }

                // Устанавливаем комнату
                LevelStr[x, y] = $"[{GetRandomRoomType()}]";
            }

            // Устанавливаем стартовую комнату и комнату босса
            LevelStr[0, 0] = "[/]";
            LevelStr[LevelSize - 1, LevelSize - 1] = "[B]";
        }

        private char GetRandomRoomType()
        {
            char[] roomTypes = ['E', 'C', 'T', 'S', 'A'];
            return roomTypes[random.Next(roomTypes.Length)];
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