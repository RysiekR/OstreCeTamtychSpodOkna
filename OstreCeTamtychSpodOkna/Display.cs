namespace OstreCeTamtychSpodOkna
{
    internal static class Display
    {
        private static List<string> display = new List<string>(); // ten ktory powinien juz byc wyswietlony, stary
        private static List<string> newDisplay = new List<string>(); // temp do ktorego trzeba wrzucic zaktualizowana mape do wyswietlenia


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
                            //display[row][i] = newDisplay[row][i];
                        }
                    }

                }
                /*else
                {
                    Console.WriteLine(display[row]);
                    Console.WriteLine(newDisplay[row]);
                }*/
            }

            // Przekopiowanie bufora zmian na bufor renderowania
            display.Clear();
            display.AddRange(newDisplay);
        }
        public static void SetNewDisplay(Map map)
        {
            //newDisplay = map.mapAsList;
            newDisplay.Clear();
            newDisplay.AddRange(map.mapAsList);
        }

        public static void InitializeDisplay(Map map)
        {
            //display = map.mapAsList;
            display.Clear();
            display.AddRange(map.mapAsList);
            PrintToConsole();
        }
        public static void InitializeDisplay(List<string> map)
        {
            //display = map.mapAsList;
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
