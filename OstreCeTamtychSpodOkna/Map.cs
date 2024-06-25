public class Map
{
    //w założeniu ma byc obiektem w ktorym mozna trzymac mape
    //np obiekt mapaMiasta, mapaAreny
    //i mozna go nadpisac zeby bo wczytaniu drugiej mapy gdzies bylo to co po nas zostalo
    private readonly int spriteHeight = Sprites.spriteHeight;
    public List<string> mapAsList = new List<string>();
    public List<Enemy> enemyList = new List<Enemy>();

    //chyba trzeba zrobic rozmiar mapy int vert;int horr; i w konstruktorze foreachem przeiterowac te vert++ i horr++ na ostatnim row
    public Map(string[] miniMapa)
    {
        CreateFinallMap(mapAsList, miniMapa);
    }
    public Map(string[] miniMapa, int numberOfEnemies)
    {
        CreateFinallMap(mapAsList, miniMapa);
        GenerateEnemies(numberOfEnemies);
    }



    //czysci i buduje nowa mape
    public void CreateFinallMap(List<string> final, string[] mapSmallSprites)
    {
        final.Clear();
        foreach (string row in mapSmallSprites)
        {
            string[] temp = new string[this.spriteHeight];
            foreach (char smallSprite in row)
            {
                //tutaj trzeba przypisac do tempa sprity z lini w mapie
                temp = AddTwoStringsTablesHorizontally(temp, Sprites.ChoseSprite(smallSprite));

            }
            //po dodaniu jednej lini spritow mamy gotowa cala linie spritow do wrzucenia do listy
            AddVertically(final, temp);
        }
    }

    //czy moze byc static(w sumie nie bede tego potrzebowal poza ta klasa)
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

    public void GenerateEnemies(int howMany)
    {
        Random rnd = new Random();
        int tempRow;
        int tempCol;
        int maxCol = mapAsList[0].Length;
        int maxRow = mapAsList.Count;
        for (int i = 0; i < howMany; i++)
        {
            do
            {
                tempRow = rnd.Next(spriteHeight, maxRow); //tempRow = rnd.Next(spriteHeight,mapAsList.Count-spriteHeight);
                tempCol = rnd.Next(spriteHeight, maxCol); //tempCol = rnd.Next(spriteHeight, mapAsList[tempRow].Length-spriteHeight);
            } while (!IsFreeSpace(tempRow, tempCol));

            this.enemyList.Add(new Enemy(tempRow, tempCol, this));
        }
    }
    private bool IsFreeSpace(int row, int col)
    {
        if (row < 0 || col < 0) return false;
        if (row > mapAsList.Count) return false;
        if (col > mapAsList[0].Length) return false;
        if (mapAsList[row][col] == ' ' || mapAsList[row][col] == ',')
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

