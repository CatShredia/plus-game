namespace PLUS_game
{

    using static System.Console;

    abstract class Object
    {
        public int CoefOfGame = 2;

        public static ConsoleColor defaultForeground = ConsoleColor.Gray;
        public static ConsoleColor defaultBackground = ConsoleColor.Black;

        public int HP;

        public static void SetDefaultColor() {
            ForegroundColor = defaultForeground;
            BackgroundColor = defaultBackground;
        }

        public static void PrintError(string err)
        {
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.Red;
            WriteLine($"Game error: {err}");
            
            SetDefaultColor();
        }

        public static void PrintHello()
        {
            WriteLine("Hello, this is PLUS-game!");
        }

        public static void PrintDefeate() {
            WriteLine("----");
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.Green;

            Write("Поражение");

            SetDefaultColor();

            Write("\n");

            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.Green;

            WriteLine("Ваша жизнь оборвалась......");

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