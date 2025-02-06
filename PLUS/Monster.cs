namespace PLUS_game
{

    using static System.Console;

    class Monster : Object
    {
        public string Name;

        public int Damage;

        public Monster(string name)
        {
            Random random = new Random();
            Name = name;


            if (name.Equals("BOSS"))
            {
                HP = random.Next(100, 150);
                Damage = random.Next(10, 20);
            }
            else
            {
                HP = random.Next(20, 50);
                Damage = random.Next(1, 10);
            }
        }

        public int Attack() {
            Random random = new Random();

            return random.Next(1,3) * 10;
        }

        public bool isNullHP() {
            if(HP < 1) {
                return true;
            } else {
                return false;
            }
        }
    }
}