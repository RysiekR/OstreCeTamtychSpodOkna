using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class RogueTestDebug
    {
        public static void NewMain()
        {
            Map cityMap = new Map(Sprites.map);
            Map arenaMap = new Map(Sprites.map2);
            Player player = new();
            Map currentMap = cityMap;
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(new Enemy(6,6));
            enemies.Add(new Enemy(7,7));
            enemies.Add(new Enemy(20,20));
            //TODO mapa sie inicjalizuje bez avatara naprawic jak bedzie gamestate
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
