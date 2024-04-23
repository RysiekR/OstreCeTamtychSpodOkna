namespace OstreCeTamtychSpodOkna
{
    internal class RogueTestDebug
    {

        //TODO wrzucic liste do gamestate
        public static List<Enemy> enemies = new List<Enemy>();

        public static void NewMain()
        {
            Map cityMap = new Map(Sprites.city);
            Map currentMap = cityMap;

            Player player = new(currentMap);
            Display.InitializeDisplay(currentMap);
            while (true)
            {
                player.UpdatePos(currentMap);
                if (player.isOnArena)
                {
                    //TODO uzywac listy z gamestate
                    foreach (Enemy enemi in enemies)
                    { enemi.UpdatePos(currentMap); }
                }
                Display.SetNewDisplay(currentMap);
                Display.RenderDisplay();

            }
        }
    }
}
