namespace PLUS_game
{
    class Player : Object
    {

        public int[] Location;
        public int[] LastLocation;

        public string Room;

        public Player(int hp) {
            HP = hp;

            Location = new int[2];
            LastLocation = new int[2];

            Location[0] = 0;
            Location[1] = 0;
        }

        public void Move() {
            LastLocation[0] = Location[0];
            LastLocation[1] = Location[1];

            if (Location[1] < CoefOfGame * 2) {
                Location[1]++;

                if((CoefOfGame * 2) == Location[1]) {
                    Location[1] = 0;
                    Location[0]++;
                }
            }
            
        }
    }
}