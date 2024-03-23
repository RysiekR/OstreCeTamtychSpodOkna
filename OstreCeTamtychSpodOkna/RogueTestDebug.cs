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
            while (true)
            {

                player.UpdatePos();

            }
        }
    }
}
