/* Этот класс Chest представляет сундук в игре, который может предложить игроку различные задачи для решения. 
 В зависимости от случайно сгенерированного числа, игроку может быть предложено вычислить модуль числа, 
 найти минимальное или максимальное значение в массиве, или решить загадку. 
 Если игрок правильно отвечает на задачу, сундук открывается, иначе он остается закрытым.
*/
namespace PLUS_game
{
    using static System.Console;
    class Chest : Object
    {
        public Game Game;
        public Chest(Game game)
        {
            Game = game;
            Random random = new();
            int number = random.Next(1, 7);

            bool result;

            switch (number)
            {
                case 1:
                    result = AbsTask(random);
                    break;
                case 2:
                    result = ArrayMinTask(random);
                    break;
                case 3:
                    result = ArrayMaxTask(random);
                    break;
                case 4:
                    result = Mystery("Не огонь, а жжётся.", "Крапива");
                    break;
                case 5:
                    result = Mystery("Четыре братца под одной шляпкой стоят, одним пояском подпоясаны.", "Стол");
                    break;
                case 6:
                    result = Mystery("Золотое решето, чёрных домиков полно.", "Подсолнух");
                    break;
                case 7:
                    result = Mystery("Без рук, без ног, а ворота отворяет.", "Ветер");
                    break;
                default:
                    PrintError("Ошибка выбора задачи Chest: 40");
                    result = false;
                    break;
            }

            if (result == true)
            {
                OpenedChest();
            }
        }
        public void OpenedChest()
        {
            Game.player.Wallet += 20;
        }
        public bool AbsTask(Random random)
        {
            int number = random.Next(-10000, 1000);
            WriteLine("Вычислите модуль числа: " + number);
            number = Math.Abs(number);
            int numberAnswer = ReadIntFromPlayer("Ответ");
            if (number == numberAnswer)
            {
                WriteLine("Сундук открыт");
                return true;
            }
            else
            {
                WriteLine("Сундук не открыт, ответ неверный");
                return false;
            }
        }
        public bool ArrayMinTask(Random random)
        {
            int number = 20;
            int[] numbers = new int[number];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(-100, 100);
            }
            WriteLine("Найдите наименьшее число в массиве: ");
            number = 100;
            foreach (int item in numbers)
            {
                if (number > item)
                {
                    number = item;
                }
                Write(item + " ");
            }
            int numberAnswer = ReadIntFromPlayer("Ответ");
            if (number == numberAnswer)
            {
                WriteLine("Сундук открыт");
                return true;
            }
            else
            {
                WriteLine("Сундук не открыт, ответ неверный");
                WriteLine("Ответ: " + number);
                return false;
            }
        }
        public bool ArrayMaxTask(Random random)
        {
            int number = 20;
            int[] numbers = new int[number];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(-100, 100);
            }
            WriteLine("Найдите наибольшее число в массиве: ");
            number = -100;
            foreach (int item in numbers)
            {
                if (number < item)
                {
                    number = item;
                }
                Write(item + " ");
            }
            int numberAnswer = ReadIntFromPlayer("Ответ");
            if (number == numberAnswer)
            {
                WriteLine("Сундук открыт");
                return true;
            }
            else
            {
                WriteLine("Сундук не открыт, ответ неверный");
                WriteLine("Ответ: " + number);
                return false;
            }
        }
        public bool Mystery(string task, string answer)
        {
            WriteLine($"Решите загадку(ответ в виде одного слова): {task}");
            string playerAnswer = ReadStringFromPlayer("Ответ");
            if (playerAnswer.Equals(answer))
            {
                WriteLine("Сундук открыт");
                return true;
            }
            else
            {
                WriteLine("Сундук не открыт, ответ неверный");
                return false;
            }
        }
    }
}