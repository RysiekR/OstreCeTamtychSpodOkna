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



/*
string[] map =
{
    "WWWWWWW",
    "W     W",
    "W     W",
    "W     W",
    "W     W",
    "W     W",
    "W     W",
    "W     W",
    "WWWWWWW"
};

string[] spriteWall =
{
    "╬╦╬╦╬",
    "╠┼┼┼╣",
    "╬┼┼┼╬",
    "╠┼┼┼╣",
    "╬╩╩╩╬"
};

string[] spriteAir =
{
    "     ",
    "     ",
    "     ",
    "     ",
    "     ",
};

//kolumna i wiersz mapy liczone od 0
int kolumnaNaMapie = 0;
int wierszNaMapie = 0;

//kolumna i wiersz na konsoli liczone od 0 liczone pozniej w foreachach ( w skrocie konsolowe = mapowe * roziar sprita)
int kolumna;
int wiersz;

//TODO uzaleznic wysokosc i szerokosc mapy od tablicy ktora jest ta mapa
int mapHeight;
int mapWidth = 7;

//TODO albo zakladamy ze sprity sa zawsze takie same albo trzeba uzaleznic te 2 inty od tych wielkosci
int spriteHeight = 5;
int spriteWidth = 5;
//Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
*//*Console.WindowHeight = (mapHeight * spriteHeight) + 1;
Console.WindowWidth = (mapWidth * spriteWidth) + 1;
*//*
foreach (string row in map)
{
    // przypisanie zmiennej wiersz(t.j. pozycji nr2 w setcursorposition) wartosci zaleznej od wysokosci sprita
    if (wierszNaMapie == 0) { wiersz = 0; }
    else
    {
        wiersz = (wierszNaMapie * spriteHeight);
    }
    //petla odpowiedzialna za kazdego chara w wierszu z tablicy map
    foreach (char miniSprite in row)
    {
        //przypisanie zmiennej kolumna(t.j. pozycji nr1 w setcursorposition) wartosci zaleznej od szerokosci sprita
        if (kolumnaNaMapie == 0) { kolumna = 0; }
        else
        {
            kolumna = (kolumnaNaMapie * spriteWidth);
        }
        //wypisanie kazdej lini string z tablicy string odpowiedniego sprita
        foreach (string rowInSprite in ChoseSprite(miniSprite))
        {
            Console.SetCursorPosition(kolumna, wiersz);
            Console.Write(rowInSprite);
            wiersz++;
        }
        kolumnaNaMapie++;
        wiersz -= spriteHeight;
    }
    wierszNaMapie++;
    kolumnaNaMapie -= mapWidth;

}


string[] ChoseSprite(char fromMap)
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

*/