namespace PLUS_game
{

    using static System.Console;

    abstract class Object
    {
        public static ConsoleColor defaultForeground = ConsoleColor.Gray;
        public static ConsoleColor defaultBackground = ConsoleColor.Black;

        public int HP;

        public static void SetDefaultColor()
        {
            ForegroundColor = defaultForeground;
            BackgroundColor = defaultBackground;
        }

        public static void PrintError(string err)
        {
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.Red;
            Write($"Game error: {err}");

            SetDefaultColor();

            Write("\n");
        }

        public static void PrintDefeate()
        {
            
            PrintWithColor("Поражание, монстр начинает заживо пожирать вас", ConsoleColor.Black, ConsoleColor.DarkRed);
            PrintWithColor("Ваша жизнь оборвалась......", ConsoleColor.Black, ConsoleColor.Green);
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

        public static void PrintWithColor(string str, ConsoleColor foregrColor, ConsoleColor backgrColor) {
            ForegroundColor = foregrColor;
            BackgroundColor = backgrColor;
            Write(str);
            SetDefaultColor();
            Write("\n");
        }

        public int ReadIntFromPlayer(string name)
        {
            try
            {
                WriteLine("Введите " + name);
                int number = Convert.ToInt32(ReadLine());

                return number;
            }
            catch (Exception e)
            {
                PrintError("Возникло исключение при вводе числа, необходимо число " + e.Message);

                return ReadIntFromPlayer(name);
            }
        }

        public string ReadStringFromPlayer(string name)
        {
            try
            {
                WriteLine("Введите " + name);
                string str = ReadLine();

                return str;
            }
            catch (Exception e)
            {
                PrintError("Возникло исключение при вводе строки " + e.Message);

                return ReadStringFromPlayer(name);
            }
        }
    }
}