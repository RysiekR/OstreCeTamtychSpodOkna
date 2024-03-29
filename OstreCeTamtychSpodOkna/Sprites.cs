namespace OstreCeTamtychSpodOkna
{
    internal class Sprites
    {
        public static readonly string obstacle = "╬┼╦╩╣╠";
        //minimapy
        public static readonly string[] map =
        {
    "WWWWWWW",
    "W     W",
    "W   P W",
    "W     W",
    "W     W",
    "WWWWWWW"
};
        public static readonly string[] map2 =
        {
    "WWWWWWWW",
    "W      W",
    "W      W",
    "W      W",
    "WWWWWWWW"
};
        //sprity
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
        public static readonly string[] spritePortal =
        {
            "     ",
            " .P. ",
            " PPP ",
            " .P. ",
            "     ",
            
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
        //wysokosc sprita potrzebna w map.spriteheight
        public static readonly int spriteHeight = spriteWall.Length;

        public static string[] ChoseSprite(char fromMap)
        {
            string[] bigSprite;
            switch (fromMap)
            {
                case 'W': bigSprite = spriteWall; break; //zamiast poprostu spriteWall uzyc chosewallsprite
                case ' ': bigSprite = spriteAir; break;
                case 'P': bigSprite = spritePortal; break;
                default: bigSprite = spriteAir; break;
            }
            return bigSprite;
        }

        //TODO zrobic zeby wybieralo dobra sciane !!! na koniec to !!! robic jak juz bedzie gra dzialala
        // co ma przyjmowac ? musi przyjmowac sasiadow czyli musi przyjac aktualna mape i z niej wziac row-1/row+1 i col-1/col+1
        public static string[] ChoseWallSprite(string[] mapSmallSprites)
        {
            string[] chosenSprite = new string[spriteHeight];
            //give me logic procedure
            return chosenSprite;
        }
    }
}


/* all the walls
 * 
╔╤╤╤═ ╔╤╤╤╗ ═╤╤╤═ ═╤╤╤╗
╟┘│└─ ║│││║ ─┘│└─ ─┘│└╢
╟─┼── ║│││║ ──┼── ──┼─╢
╟┐│┌─ ║│││║ ─┐│┌─ ─┐│┌╢
║│││╬ ║│││║ ╬│││╬ ╬│││║
                       
║│││╬ ║│││║       ╬│││║
╟┘│└─ ║│││║       ─┘│└╢
╟─┼── ║│││║       ──┼─╢
╟┐│┌─ ║│││║       ─┐│┌╢
║│││╬ ║│││║       ╬│││║
                       
╔════ ╬│││╬ ═════ ════╗
╟──── ─┘│└─ ───── ────╢
╟──── ──┼── ───── ────╢
╟──── ─┐│┌─ ───── ────╢
╚════ ╬│││╬ ═════ ════╝
                       
║│││╬ ║│││║ ╬│││╬ ╬│││║
╟┘│└─ ║│││║ ─┘│└─ ─┘│└╢
╟─┼── ║│││║ ──┼── ──┼─╢
╟┐│┌─ ║│││║ ─┐│┌─ ─┐│┌╢
╚╧╧╧═ ╚╧╧╧╝ ═╧╧╧═ ═╧╧╧╝

*/


/*     
 ♣♣♣ 
 ♣♣♣ 
  ¥  
     */

/*
  ▲  
 ▲▀▲ 
▐███▌
▐█▀▀▌
▐≡██▌*/

/*
  ▲  
 ▲▀▲ 
▐█▀▀▌
▐███▌
▐≡▄▄▌*/

