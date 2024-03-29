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

            //TODO mapa sie inicjalizuje bez avatara naprawic jak bedzie gamestate

            while (true)
            {
                player.UpdatePos(currentMap);
                currentMap.UpdateMap();
            }
        }
    }
}
