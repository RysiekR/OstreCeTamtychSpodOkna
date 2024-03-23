using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OstreCeTamtychSpodOkna


{
    internal class Display
    {

        public static readonly string obstacleLetters = "WT";
        public static readonly string obstacleLettersWithLogic = "P";

        //no mapa kurwa
        public static readonly string[] baseMap =
    {
    "WWWWWWWWWWWW",
    "W          W",
    "W    T     W",
    "W          W",
    "W          W",
    "W          W",
    "W          W",
    "W          W",
    "W          W",
    "WWWWWWWWWWWW",
};
        public static readonly string[] baseMapBig =
    {
    "WWWWWWWWWWWWWWWWWWWWWWWWW",
    "W                       W",
    "W                       W",
    "W   P                   W",
    "W            ,,         W",
    "W         ,,,,          W",
    "W         ,,,           W",
    "W       ,,,,            W",
    "W                       W",
    "W                       W",
    "W               TT      W",
    "W               TT      W",
    "W                       W",
    "W                       W",
    "W                       W",
    "WWWWWWWWWWWWWWWWWWWWWWWWW",
};

        public static void Initialize(string[] map, Player gracz)
        {

            //narysowanie mapy "row by row"
            foreach (string row in map)
            {
                Console.WriteLine(row);

            }

            //wrzucenie gracza na mape
            Console.SetCursorPosition(gracz.col, gracz.row);
            Console.Write("#");
            Console.CursorVisible = false;
        }

    }
}
