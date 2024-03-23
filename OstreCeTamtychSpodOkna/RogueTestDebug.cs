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
        public static void newMain(SoundPlayer test)
        {
            Player player = new();
            Display.Initialize(Display.baseMapBig, player);
            // tworzenie clasy ktora trzyma .wav i uzycie go
          //  if (OperatingSystem.IsWindows())
            //{
                
            //}

            // !!! ACHTUNG ! ACHTUNG !!! endless loop
            //oddanie kontroli nad pozycja gracza w rece gracza
            while (true)
            {
                //TODO Display.UpdateMap()

                player.UpdatePos();

                test.Stop();
            }
        }
    }
}
