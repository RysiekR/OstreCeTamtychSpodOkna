using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class GameStateRogue
    {
        // jak narazie przychowalnia map 
        private Map cityMap;
        private Map arenaMap;
        public Map currentMap;
        GameStateRogue(Map cityMap, Map arenaMap)
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
