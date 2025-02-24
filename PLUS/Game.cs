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
                Clear();

                player.Room = dangeon.Level.LevelStr[player.Location[0], player.Location[1]];

                // dangeon.Level.SetPlayer();
                dangeon.Level.WriteLevel();

                Write("Комната игрока: ");
                ChoiseColor(player.Room);
                WriteLine($"{player.Room}");

                if (isNewLevel == false)
                {
                    player.Move();
                    dangeon.Action(player.Room);
                }
                else
                {
                    isNewLevel = false;

                    if (player.Room == "[B]")
                    {
                        dangeon.ToNewLevel();
                    }
                }
            }
        }
        public void MonsterFight(Monster monster)
        {
            bool isFight = true;
            while (isFight)
            {
                DisplayCombatStatus(monster);
                isFight = PerformPlayerAttack(monster);
                if (isFight)
                {
                    isFight = PerformMonsterAttack(monster);
                }
            }
        }

        private void DisplayCombatStatus(Monster monster)
        {
            PrintWithColor($"У вас: {player.HP}HP", ConsoleColor.Black, ConsoleColor.DarkRed);
            PrintWithColor($"У противника {monster.HP}HP", ConsoleColor.Black, ConsoleColor.DarkCyan);
        }

        private bool PerformPlayerAttack(Monster monster)
        {
            monster.HP -= player.Attack();
            if (monster.isNullHP())
            {
                HandleMonsterDefeat(monster);
                return false;
            }
            return true;
        }

        private bool PerformMonsterAttack(Monster monster)
        {
            player.HP -= monster.Attack();
            if (player.isNullHP())
            {
                isGame = false;
                return false;
            }
            return true;
        }

        private void HandleMonsterDefeat(Monster monster)
        {
            PrintWithColor($"С монстра выпало {monster.Cost} монет!", ConsoleColor.Black, ConsoleColor.DarkYellow);
            player.Wallet += monster.Cost;

            if (monster.Name == "BOSS")
            {
                dangeon.ToNewLevel();
            }
        }
    }
}