using OstreCeTamtychSpodOkna;

public static class MapHolder
{
    public static Map cityMap = new Map(Sprites.city);
    public static Map tempArenaMap; // w Display.loadarena zrobic temp arena.clear() a pozniej temparena = new bla bla bla 

    public static void GenerateArena()
    {
        tempArenaMap = new Map(Sprites.arena,Math.Min(30,Math.Max(3,PrawieSingleton.player.pokemonList.Count)));
    }
    public static Enemy FindEnemyOnArenaAt(int row, int col)
    {
        Enemy enemy = null;
        foreach (Enemy enemi in PrawieSingleton.player.currentMap.enemyList)
        {
            if (enemi.row == row && enemi.col == col) 
            {
                return enemi;
            }
        }
        return enemy;
    }

}
