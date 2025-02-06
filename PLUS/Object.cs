namespace PLUS_game
{

    using static System.Console;

    abstract class Object
    {
        public int CoefOfGame = 1;

        public static ConsoleColor defaultForeground = ConsoleColor.Gray;
        public static ConsoleColor defaultBackground = ConsoleColor.Black;

        public static int HP;

        public static void PrintError(string err)
        {
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.Red;
            WriteLine($"Game error: {err}");
            ForegroundColor = defaultForeground;
            BackgroundColor = defaultBackground;
        }
    }
}