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

                    for (int col = 0; col < display[row].Length; col++)
                    {
                        if (display[row][col] != newDisplay[row][col])
                        {
                            Console.SetCursorPosition(col, row);
                            Console.Write(newDisplay[row][col]);
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
            Console.SetCursorPosition(0,0);
            foreach (string row in display)
            {
                Console.WriteLine(row);
            }
        }

        public static void LoadArena()
        {
            InitializeDisplay(TempGameState.tempArenaMap);
            //TODO print player
        }

        public static void LoadCityMap()
        {
            // TODO umiesc gracza w miejscu bezpiecznym dla kodu
            InitializeDisplay(TempGameState.cityMap);
            //TODO print player
        }
    }
}
