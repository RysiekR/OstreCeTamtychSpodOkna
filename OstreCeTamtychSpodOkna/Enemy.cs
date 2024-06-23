
//enemy class with movement logic, in the future will be holding pokemons 
using OstreCeTamtychSpodOkna;

public class Enemy : HasPokemonList
{
    public int row { get; private set; }
    public int col { get; private set; }
    string enemyAvatar;
    public static string enemyString = "123456789%";
    string obstacleString = "AC#PN";
    public Map currentMap;
    public Pokemon Pokemon { get; set; }

    public Enemy(int rowSpawn, int colSpawn, Map currentMap)
    {
        this.currentMap = currentMap;
        this.row = rowSpawn;
        this.col = colSpawn;
        pokemonList.Clear();
        GeneratePokemonsInList(PrawieSingleton.player.pokemonList.Count());
        if (pokemonList.Count > 9)
        {
            enemyAvatar = "%";
        }
        else
        {

            enemyAvatar = pokemonList.Count.ToString();
        }
        Pokemon = pokemonList[0];
        InitializeEnemyPosition();
    }
    private int oldCol;
    private int oldRow;
    //public List<string> jakaMapa = new();
    Random rnd = new Random();
    private bool hitObstacle = false;
    int randomMove;

    private void GeneratePokemonsInList(int howMany)
    {
        if (howMany <= 1)
        {
            howMany = 1;
        }
        else if (howMany>9)
        {
            howMany += rnd.Next(-4, 5);
        }
        else
        {
            howMany += rnd.Next(-1, 2);
        }
        for (int i = 0; i < howMany; i++)
        {
            pokemonList.Add(new Pokemon(PrawieSingleton.GetAverageLevelOfPlayerPokemons()));
        }
    }

    public void UpdatePos()
    {
        oldCol = col;
        oldRow = row;
        /*            jakaMapa.Clear();
                    jakaMapa.AddRange(currentMap.mapAsList);
        */
        char charUp = currentMap.mapAsList[row - 1][col];
        char charDown = currentMap.mapAsList[row + 1][col];
        char charLeft = currentMap.mapAsList[row][col - 1];
        char charRight = currentMap.mapAsList[row][col + 1];
        randomMove = rnd.Next(1, 5);
        switch (randomMove)
        {
            case 1:
                Movement(charUp);
                break;
            case 2:
                Movement(charDown);
                break;
            case 3:
                Movement(charLeft);
                break;
            case 4:
                Movement(charRight);
                break;
        }


        //if wall was not hit: move enemy and clear old position
        if (!hitObstacle)//tutaj jak moze isc
        {
            currentMap.mapAsList[row] = currentMap.mapAsList[row].Insert(col, enemyAvatar);
            currentMap.mapAsList[row] = currentMap.mapAsList[row].Remove(col + 1, 1);
            currentMap.mapAsList[oldRow] = currentMap.mapAsList[oldRow].Insert(oldCol, " ");
            currentMap.mapAsList[oldRow] = currentMap.mapAsList[oldRow].Remove(oldCol + 1, 1);
        }
        hitObstacle = false;
    }

    // i sprawdza co tam jest i robi co trzeba(mam nadzieje)
    void Movement(char charInThisDirection)
    {
        //sprawdzenie czy char gdzie idziemy jest charem ze string z przeszkodami nie do przejscia
        if (Sprites.obstacle.Contains(charInThisDirection) || obstacleString.Contains(charInThisDirection)/* == 'P' || charInThisDirection == '#'*/)
        {
            unpassableObstacle(charInThisDirection);
        }
        // tutaj jak moze normalnie chodzic to zmienia pozycje row / col
        else
        {
            switch (randomMove)
            {
                case 1:
                    { row--; }
                    break;
                case 2:
                    { row++; }
                    break;
                case 3:
                    { col--; }
                    break;
                case 4:
                    { col++; }
                    break;
            }
        }
    }

    void unpassableObstacle(char charToLogic)
    {
        if (Sprites.obstacle.Contains(charToLogic) || obstacleString.Contains(charToLogic) || enemyString.Contains(charToLogic))
        {
            hitObstacle = true;
        }
        else if (charToLogic == '#')
        {
            Console.Beep(); // przeciwnik w nas wszedl
        }

        else { Console.WriteLine("Error ! ! ! Enemy.UpdatePos.UnpassableObstacle"); }

    }

    private void InitializeEnemyPosition()
    {
        //wstawienie enemy
        currentMap.mapAsList[row] = currentMap.mapAsList[row].Insert(col, enemyAvatar);
        currentMap.mapAsList[row] = currentMap.mapAsList[row].Remove(col + 1, 1);
    }

    public void GetRidOfThisAvatar()
    {
        currentMap.mapAsList[row] = currentMap.mapAsList[row].Insert(col, "N");
        currentMap.mapAsList[row] = currentMap.mapAsList[row].Remove(col + 1, 1);

    }
}
