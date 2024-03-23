using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class RogueTestDebug
    {
        public static void newMain()
        {
            Player player = new();
            Display.Initialize(Display.baseMapBig, player);

            // !!! ACHTUNG ! ACHTUNG !!! endless loop
            //oddanie kontroli nad pozycja gracza w rece gracza
            while (true)
            {

                player.UpdatePos();

            }
        }
    }
}
