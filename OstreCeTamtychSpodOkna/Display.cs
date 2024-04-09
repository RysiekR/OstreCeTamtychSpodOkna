namespace OstreCeTamtychSpodOkna
{
    internal static class Display
    {
        private static List<string> display = new List<string>(); // ten ktory powinien juz byc wyswietlony, stary
        private static List<string> newDisplay = new List<string>(); // temp do ktorego trzeba wrzucic zaktualizowana mape do wyswietlenia
        
        private static readonly int spriteWidth = Sprites.spriteWall[0].Length;
        private static readonly int miniMapWidth = Sprites.map[0].Length;
        private static readonly int mapDisplayWidth = spriteWidth*miniMapWidth;

        private static readonly int spriteHeight = Sprites.spriteWall.Length;
        private static readonly int miniMapHeight = Sprites.map.Length;
        private static readonly int mapDisplayHeight = spriteHeight*miniMapHeight;

        private static List<string> infoDisplay = new List<string>();
        private static List<string> newInfoDisplay = new List<string>();

        // Porównanie buforów, wypisanie ich na ekran i zapis zmian
        public static void RenderDisplay()
        {

            for (int row = 0; row < display.Count; row++)
            {
                if (display[row] != newDisplay[row])
                {

                    for (int i = 0; i < display[row].Length; i++)
                    {
                        if (display[row][i] != newDisplay[row][i])
                        {
                            Console.SetCursorPosition(i, row);
                            Console.Write(newDisplay[row][i]);
                            Console.CursorVisible = false;
                        }
                    }
                }
            }

            // Przekopiowanie bufora zmian na bufor renderowania
            display.Clear();
            display.AddRange(newDisplay);
        }
        public static void SetNewDisplay(Map map)
        {
            newDisplay.Clear();
            newDisplay.AddRange(map.mapAsList);
        }

        public static void InitializeDisplay(Map map)
        {
            display.Clear();
            display.AddRange(map.mapAsList);
            PrintToConsole();
        }
        public static void InitializeDisplay(List<string> map)
        {
            display.Clear();
            display.AddRange(map);
            PrintToConsole();
        }

        public static void PrintToConsole()
        {
            foreach (string row in display)
            {
                Console.WriteLine(row);
            }
        }
    }
}
