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

        public static readonly int spriteHeight = spriteWall.Length;

        //TODO wez kartke i dlugopis i rozrysuj to sobie po kazdym jednym znaczku
        //nie dziala, ale jest zrobione nowe
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




        //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);


        //test
        /*
        List<string> mapaDoWydruku = new();

        CreateFinallMap(mapaDoWydruku, map);

        foreach (string row in mapaDoWydruku)
        {
            Console.WriteLine(row);
        }
        */

        public static void CreateFinallMap(List<string> finalna, string[] mapSmallSprites)
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
                AddVertically(finalna, temp);
            }
        }


        // wyrysuj mape do wywalenia
        /*void WyrysujMapeUzywajacSpritow()
        {
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

            //potrzebne ustawienie okna na max size inaczej wywala errory ze nie moze wejsc w dana komorke(no nie wejdzie w nia bo jej nie ma)
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

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
        }
        */
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


        //TODO zmien zeby przyjmowalo liste i tablice i wypluwalo nowa liste(nie wazne, tak tez dziala i juz jest czesciowo zaimplementowane)
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

        //TODO try making metode dodawanie verticalnego
        public static void AddVertically(List<string> bazowa, string[] dodawana)
        {
            for (int i = 0; i < dodawana.Length; i++)
            {
                bazowa.Add(dodawana[i]);
            }
        }

    }
}


//to na dole nie dziala jak trzeba jest zrobione nowe

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