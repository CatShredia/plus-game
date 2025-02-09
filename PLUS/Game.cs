/*
 Данный код представляет собой класс игры в пространстве имен PLUS_game. 
 Класс Game управляет основными аспектами игры, 
 включая инициализацию игрока, создание подземелья и взаимодействие с монстрами. 
 В конструкторе класса происходит приветствие игрока, создание экземпляров игрока и подземелья, а также коллекция оружия. 
 Игра продолжается в цикле, пока не будет достигнут конец игры. В классе также реализована логика боя между игроком и монстрами, 
 где игрок может атаковать, а также получать урон от противников. При победе над монстром, если это босс, происходит переход на новый уровень.
*/

namespace PLUS_game
{
    using static System.Console;
    class Game : Object
    {
        // ход игры
        public static bool isGame = false;
        public static bool isNewLevel;
        public int CoefOfGame = 2;

        public static Player player;
        public static Dangeon dangeon;
        public static List<Weapon> gameWeaponsCollection;
        public static int LevelNumber = 1;

        // основная логика игры, где находится рабочий цикл игры
        public Game()
        {
            // Clear();

            // * -----
            PrintWithColor("Добро пожаловать в PLUS\n", ConsoleColor.Black, ConsoleColor.DarkBlue);
            isGame = true;
            player = new Player(this, 100);
            dangeon = new Dangeon(this);

            gameWeaponsCollection = new List<Weapon>
            {
                new Weapon("Меч", 10),
                new Weapon("Лук", 20),
                new Weapon("Арбалет", 40),
                new Weapon("Заточка", 1),
            };
            // * -----
            // генерим левел
            dangeon.Level.GenerateLevel();
            while (isGame == true)
            {
                player.Room = dangeon.Level.LevelStr[player.Location[0], player.Location[1]];

                dangeon.Level.SetPlayer();
                dangeon.Level.WriteLevel();

                Write("Комната игрока: ");
                ChoiseColor(player.Room);
                WriteLine($"{player.Room}");

                dangeon.Action(player.Room);
                if (player.Room == "[B]")
                {
                    dangeon.ToNewLevel();
                }

                if (isNewLevel == false)
                {
                    player.Move();
                }
                else
                {
                    isNewLevel = false;
                }
            }
        }
        public static void MonsterFight(Monster monster)
        {
            bool isFight = true;
            while (isFight)
            {
                PrintWithColor($"У вас: {player.HP}HP", ConsoleColor.Black, ConsoleColor.DarkRed);
                PrintWithColor($"У противника {monster.HP}HP", ConsoleColor.Black, ConsoleColor.DarkCyan);
                monster.HP -= player.Attack(); // удар игрока
                if (monster.isNullHP())
                {
                    isFight = false;
                    if (monster.Name == "BOSS")
                    {
                        dangeon.ToNewLevel();
                    }
                }
                else
                {
                    player.HP -= monster.Attack(); // удар монстра
                }
                if (player.isNullHP())
                {
                    isFight = false;
                    isGame = false;
                }
            }
        }
    }
}