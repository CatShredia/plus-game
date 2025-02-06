namespace PLUS_game
{

    using static System.Console;

    class Dangeon : Object
    {
        public string[,] Level;

        /*
            / - player or start room
            E - enemy
            C - chest
            T - trap
            S - store
            B - boss
        */
        public char[] TypeOfRoom = ['/', 'E', 'C', 'T', 'S', 'B'];

        public Dangeon()
        {
            Level = new string[CoefOfGame * 2, CoefOfGame * 2];
        }
        public void GenerateLevel()
        {
            for (int i = 0; i < CoefOfGame * 2; i++)
            {
                for (int j = 0; j < CoefOfGame * 2; j++)
                {
                    Level[i, j] = $"[{GetTypeOfRoom()}]";
                }
            }
        }
        public char GetTypeOfRoom()
        {
            Random random = new Random();

            int number = random.Next(1, TypeOfRoom.Length);

            return TypeOfRoom[number];
        }
        public void WriteLevel()
        {
            WriteLine("---");

            for (int i = 0; i < CoefOfGame * 2; i++)
            {
                for (int j = 0; j < CoefOfGame * 2; j++)
                {
                    ChoiseColor(Level[i,j]);
                    Write(Level[i, j]);
                    ForegroundColor = defaultForeground;

                    if (j == CoefOfGame * 2 - 1)
                    {
                        Write("\n");
                    }
                }
            }
            WriteLine("---");
        }

        public void ChoiseColor(string str)
        {
            ForegroundColor = str[1] switch
            {
                '/' => ConsoleColor.Cyan,
                'E' => ConsoleColor.Red,
                'C' => ConsoleColor.Yellow,
                'T' => ConsoleColor.Blue,
                'S' => ConsoleColor.Green,
                'B' => ConsoleColor.DarkRed,
                _ => defaultForeground,
            };
        }
    }
}