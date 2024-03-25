namespace OstreCeTamtychSpodOkna
{
    internal class Sprites
    {

        public static readonly string[] map =
        {
    "WWWWWWW",
    "W     W",
    "W     W",
    "W     W",
    "W     W",
    "W     W",
    "WWWWWWW"
};

        public static readonly string[] map2 =
        {
    "WWW",
    "W W",
    "WWW"
};

        public static readonly string[] spriteWall =
        {
    "╬╦╬╦╬",
    "╠┼┼┼╣",
    "╬┼┼┼╬",
    "╠┼┼┼╣",
    "╬╩╩╩╬"
};

        public static readonly string[] spriteAir =
        {
    ", , ,",
    " , , ",
    ", , ,",
    " , , ",
    ", , ,",
};

        public static readonly string[] wall =
        {
            "+---+",
            "|+++|",
            "|+++|",
            "|+++|",
            "+---+"
        };
        public static readonly string[] tree =
        {
            " *^* ",
            "*#@#*",
            "'*$*'",
            ")|:|(",
            " |^| "
        };
        public static readonly string[] empty =
        {
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
        };

        public static readonly int spriteHeight = spriteWall.Length;

        public static void CreateFinallMap(List<string> final, string[] mapSmallSprites)
        {
            //no jedziemy z koksem !!!

            foreach (string row in mapSmallSprites)
            {
                string[] temp = new string[spriteHeight];
                foreach (char smallSprite in row)
                {
                    //tutaj trzeba przypisac do tempa sprity z lini w mapie
                    temp = AddTwoStringsTablesHorizontally(temp, ChoseSprite(smallSprite));

                }
                //po dodaniu jednej lini spritow mamy gotowa cala linie spritow do wrzucenia do listy
                AddVertically(final, temp);
            }
        }

        public static string[] ChoseSprite(char fromMap)
        {
            string[] bigSprite;
            switch (fromMap)
            {
                case 'W': bigSprite = spriteWall; break;
                case ' ': bigSprite = spriteAir; break;
                default: bigSprite = spriteAir; break;
            }
            return bigSprite;
        }

        //TODO zrobic zeby wybieralo dobra sciane
        // co ma przyjmowac ?
        public static string[] ChoseWallSprite(string[] mapSmallSprites)
        {
            string[] chosenSprite = new string[spriteHeight];
            //give me logic procedure

            return chosenSprite;
        }

        public static string[] AddTwoStringsTablesHorizontally(string[] first, string[] second)
        {
            string[] temps = new string[first.Length];
            first.CopyTo(temps, 0);
            for (int i = 0; i < first.Length; i++)
            {
                temps[i] += second[i];
            }
            return temps;
        }

        public static void AddVertically(List<string> bazowa, string[] dodawana)
        {
            for (int i = 0; i < dodawana.Length; i++)
            {
                bazowa.Add(dodawana[i]);
            }
        }

    }
}

