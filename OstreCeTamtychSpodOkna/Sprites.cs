using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class Sprites
    {

        //TODO wez kartke i dlugopis i rozrysuj to sobie po kazdym jednym znaczku
        public static void Draw(string[] map, string[] sprite)
        {
            int x = 0;
            int y = 0;
            int xCursor = x * 5;
            int yCursor = y * 5;

            foreach (string row in map)
            {
                //wypisuje jedna linie z tablicy mapa
                //Console.WriteLine(row);

                foreach (char signOnMap in row)
                {

                    //wypisuje pojedynczy znak z mapy
                    //Console.Write(signOnMap);
                    foreach (string rowSprite in sprite)
                    {
                        
                        //Console.WriteLine(rowSprite);
                        // powypisaniu sprita przeskocz namiejsce kolejnnego sprita (moze row,signOnMap)
                        //Console.SetCursorPosition(xCursor,yCursor);
                        foreach (char spriteDetail in rowSprite)
                        {
                            Console.Write(spriteDetail); //wypisana 1 linia sprita
                        }
                        x++;
                        Console.SetCursorPosition(yCursor, xCursor);
                        //tu juz powinien byc wypisany caly sprite

                    }
                    y++;

                    //Console.SetCursorPosition(xCursor, yCursor);

                }

            }

        }
        public static readonly string[] wall =
        {
            "+---+",
            "|+++|",
            "|+++|",
            "|+++|",
            "+---+"
        } ;
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

    }
}
