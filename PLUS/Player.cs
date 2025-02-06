namespace PLUS_game
{

    using static System.Console;

    class Player : Object
    {

        public int[] Location;
        public int[] LastLocation;

        public string Room;

        public Item[] Inventory;

        public Player(int hp)
        {
            HP = hp;

            Location = new int[2];
            LastLocation = new int[2];

            Location[0] = 0;
            Location[1] = 0;

            Inventory = new Item[5];

            // добавление изначальных предметов
            for(int i = 0; i < 3;i++) {
                Inventory[i] = new Item("Зелье", "чудотворное", 20);
            }
        }

        public void Move()
        {
            LastLocation[0] = Location[0];
            LastLocation[1] = Location[1];

            if (Location[1] < CoefOfGame * 2)
            {
                Location[1]++;

                if ((CoefOfGame * 2) == Location[1])
                {
                    Location[1] = 0;
                    Location[0]++;
                }
            }

        }
        public void OpenInventory()
        {
            WriteLine("Выберите предмет:");

            for (int i = 0; i < Inventory.Length; i++)
            {
                if(Inventory[i] != null){
                    WriteLine($"{i + 1}: {Inventory[i].Name} ");
                } else {
                    WriteLine($"{i + 1}: Пустой карман.. ");
                }
            }
        }
    }
}