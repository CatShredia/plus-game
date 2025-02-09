// Этот класс представляет оружие в игре, включая его название и урон.
namespace PLUS_game
{
    using static System.Console;
    class Weapon
    {
        public string Name;
        public int Damage;
        public Weapon(string name, int damage) {
            Name = name;
            Damage = damage;
        }
    }
}