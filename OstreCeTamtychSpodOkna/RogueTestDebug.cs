﻿using System;
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

            /*string test = "asdf";
            Console.WriteLine(test);
            test = test.Insert(1, "q");
            Console.WriteLine(test);
            test = test.Remove((1 + 1),1);
            Console.WriteLine(test);
            Console.ReadLine();*/


            Map cityMap = new Map(Sprites.map);
            Map arenaMap = new Map(Sprites.map2);
            Player player = new();
            Map currentMap = cityMap;
            
            /*
            List<string> map;
            map = cityMap.mapAsList;
*/

            /*
                        //laduje sie mapa na spritach jeszcze z logika stara na tablicy
                        Sprites.CreateFinallMap(map,Sprites.map);


                        //Display.Initialize(Display.baseMapBig, player);
                        Sprites.Initialize(map, player);
            */


            currentMap.PrintToConsole(player);



            while (true)
            {

                player.UpdatePos(currentMap);
                currentMap.UpdateMap(player);
            }
        }
    }
}
