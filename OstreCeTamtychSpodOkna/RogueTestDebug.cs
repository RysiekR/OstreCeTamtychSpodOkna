﻿namespace OstreCeTamtychSpodOkna
{
    internal static class TempGameState
    {
        public static Map cityMap = new Map(Sprites.city);
        public static Map tempArenaMap; // w Display.loadarena zrobic temp arena.clear() a pozniej temparena = new bla bla bla 

        public static void GenerateArena()
        {
            tempArenaMap = new Map(Sprites.arena);

            tempArenaMap.enemyList.Clear();
            tempArenaMap.enemyList.Add(new Enemy(6, 6, tempArenaMap));
            tempArenaMap.enemyList.Add(new Enemy(7, 7, tempArenaMap));
            tempArenaMap.enemyList.Add(new Enemy(9, 9, tempArenaMap));
            tempArenaMap.enemyList.Add(new Enemy(15, 15, tempArenaMap));
        }
    }
    internal class RogueTestDebug
    {

/*        //TODO wrzucic liste do gamestate
        public static List<Enemy> enemies = new List<Enemy>();
*/
        public static void NewMain()
        {
            Player player = new(TempGameState.cityMap);
            Display.InitializeDisplay(TempGameState.cityMap);
            while (true)
            {
                player.UpdatePos();
                if (player.isOnArena)
                {
                    //TODO uzywac listy z gamestate
                    foreach (Enemy enemi in TempGameState.tempArenaMap.enemyList)
                    { enemi.UpdatePos(); }
                }
                Display.SetNewDisplay(player.currentMap);
                Display.RenderDisplay();

            }
        }
    }

}
