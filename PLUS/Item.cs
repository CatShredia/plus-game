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