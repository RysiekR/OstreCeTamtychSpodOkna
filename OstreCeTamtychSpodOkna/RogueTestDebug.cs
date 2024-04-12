namespace OstreCeTamtychSpodOkna
{
    internal class RogueTestDebug
    {
        public static void NewMain()
        {
            Map cityMap = new Map(Sprites.map);
            Map arenaMap = new Map(Sprites.map2);
            Map currentMap = cityMap;
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(new Enemy(6, 6, currentMap));
            enemies.Add(new Enemy(7, 7, currentMap));
            enemies.Add(new Enemy(20, 20, currentMap));
            enemies.Add(new Enemy(30, 30, currentMap));
            enemies.Add(new Enemy(20, 20, currentMap));

            //TODO mapa sie inicjalizuje bez avatara naprawic jak bedzie gamestate
            Player player = new(currentMap);
            Display.InitializeDisplay(currentMap);
            while (true)
            {
                player.UpdatePos(currentMap);

                foreach (Enemy enemi in enemies)
                { enemi.UpdatePos(currentMap); }

                Display.SetNewDisplay(currentMap);
                Display.RenderDisplay();

            }
        }
    }
}
