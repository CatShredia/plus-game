// Этот класс представляет предмет в игре, включая его имя, описание, эффект и стоимость. 
// Конструктор позволяет инициализировать имя, описание и эффект предмета.

namespace PLUS_game
{
    using static System.Console;
    class Item
    {
        public string Name;
        public string Description;
        public int Effect;
        public int Cost;
        public Item(string name, string description, int effect) {
            Name = name;
            Description = description;
            Effect = effect;
        }
    }
}