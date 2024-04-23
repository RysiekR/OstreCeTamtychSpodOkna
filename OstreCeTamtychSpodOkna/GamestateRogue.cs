using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class GameStateRogue
    {
        //docelowo ma trzymac mape miasta i generowac arene
        // jak narazie przychowalnia map 
        public Map cityMap;
        public Map arenaMap;
        public Map currentMap;
        GameStateRogue(Map cityMap)
        {
            if (cityMap is object)
            {
                currentMap = cityMap;
            }
            else
            {
                Console.WriteLine("!!!   cityMap not generated yet while GamestateRogue was using it   !!!");
            }
        }

        //TODO funkcje zapisujace current map do city map celem przechowania i czyszczace arena map
    }
}
