using OstreCeTamtychSpodOkna;

public static class MapHolder
{
    public static Map cityMap = new Map(Sprites.city);
    public static Map tempArenaMap; // w Display.loadarena zrobic temp arena.clear() a pozniej temparena = new bla bla bla 

    public static void GenerateArena()
    {
        tempArenaMap = new Map(Sprites.arena,5);
/*
        tempArenaMap.enemyList.Clear();
        tempArenaMap.enemyList.Add(new Enemy(6, 6, tempArenaMap));
        tempArenaMap.enemyList.Add(new Enemy(7, 7, tempArenaMap));
        tempArenaMap.enemyList.Add(new Enemy(9, 9, tempArenaMap));
        tempArenaMap.enemyList.Add(new Enemy(15, 15, tempArenaMap));*/
    }
    public static Enemy FindEnemyOnArenaAt(int row, int col)
    {
        Enemy enemy = null;
        foreach (Enemy enemi in tempArenaMap.enemyList)
        {
            if (enemi.row == row && enemi.col == col) 
            {
                return enemi;
            }
        }
        return enemy;
    }

}
