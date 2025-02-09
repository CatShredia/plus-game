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
        public Chest()
        {
            Random random = new Random();
            WriteLine("Сундук!");
            int number = random.Next(1, 7);
            switch (number)
            {
                case 1:
                    AbsTask(random);
                    break;
                case 2:
                    ArrayMinTask(random);
                    break;
                case 3:
                    ArrayMaxTask(random);
                    break;
                case 4:
                    ArrayMaxTask(random);
                    break;
                case 5:
                    Mystery("Не огонь, а жжётся.", "Крапива");
                    break;
                case 6:
                    Mystery("Золотое решето, чёрных домиков полно.", "Подсолнух");
                    break;
                case 7:
                    Mystery("Без рук, без ног, а ворота отворяет.", "Ветер");
                    break;
                default:
                    PrintError("Ошибка выбора задачи Chest: 40");
                    break;
            }
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
            WriteLine("Найдите наименьшее число в массиве: ");
            number = 100;
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