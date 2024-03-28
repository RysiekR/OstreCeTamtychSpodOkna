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
            List<string> map = new();





            //laduje sie mapa na spritach jeszcze z logika stara na tablicy
            Sprites.CreateFinallMap(map,Sprites.map);
            
            
            //Display.Initialize(Display.baseMapBig, player);
            Sprites.Initialize(map, player);


            while (true)
            {
                //TODO Display.UpdateMap()

                player.UpdatePos(map);
            }
        }
    }
}
