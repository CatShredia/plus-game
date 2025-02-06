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
        public string GetTypeOfRoom()
        {
            Random random = new Random();

            WriteLine(TypeOfRoom.Length);

            int number = random.Next(1, TypeOfRoom.Length);

            switch (number)
            {
                case 1:
                    break;
                default:
                    WriteLine("---");
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Red;
                    Write("Game error: don't find a type of room");
                    ForegroundColor = defaultForeground;
                    BackgroundColor = defaultBackground;
                    WriteLine("---");

                    break;
            }

            return null;
        }
        public void WriteLevel()
        {
            WriteLine("---");

            for (int i = 0; i < CoefOfGame * 2; i++)
            {
                for (int j = 0; j < CoefOfGame * 2; j++)
                {
                    Write(Level[i, j]);
                    // Write($" {i}/{j} ");

                    if (j == CoefOfGame * 2 - 1)
                    {
                        Write("\n");
                    }
                }
            }
            WriteLine("---");
        }
    }
}