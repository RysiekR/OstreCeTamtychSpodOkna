﻿    public static class Display
    {
        private static List<string> display = new List<string>(); // ten ktory powinien juz byc wyswietlony, stary
        private static List<string> newDisplay = new List<string>(); // temp do ktorego trzeba wrzucic zaktualizowana mape do wyswietlenia

/*        private static readonly int spriteWidth = Sprites.spriteWall[0].Length;
        private static readonly int miniMapWidth = Sprites.map[0].Length;
        private static readonly int mapDisplayWidth = spriteWidth*miniMapWidth;

        private static readonly int spriteHeight = Sprites.spriteWall.Length;
        private static readonly int miniMapHeight = Sprites.map.Length;
        private static readonly int mapDisplayHeight = spriteHeight*miniMapHeight;
*/
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
                            if (Enemy.enemyString.Contains(newDisplay[row][col]))//jezeli rysujesz przeciwnika to rysuj uzywajac koloru czerwonego
                            {
                                // Set the color of the console output
                                // kolorki tutaj znajdz enemy w map holder masz metode do szukania

                                /*switch (MapHolder.FindEnemyOnArenaAt(row, col).pokemonList[0].type)
                                {
                                    case Type.Moist: Console.ForegroundColor = ConsoleColor.Blue; break;
                                    case Type.Lava: Console.ForegroundColor = ConsoleColor.Red; break;
                                    case Type.Mud: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                                    default: Console.ForegroundColor = ConsoleColor.Cyan; break;
                                }*/
                                if(MapHolder.FindEnemyOnArenaAt(row,col) == null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                }
                                else if (MapHolder.FindEnemyOnArenaAt(row, col).pokemonList[0].type == Type.Moist)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                }
                                else if (MapHolder.FindEnemyOnArenaAt(row, col).pokemonList[0].type == Type.Lava)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                }

                                //Console.ForegroundColor = ConsoleColor.Red;
                                
                                Console.SetCursorPosition(col, row);
                                Console.Write(newDisplay[row][col]);
                                Console.CursorVisible = false;
                                // Reset the color back to the default
                                Console.ResetColor();
                            }
                            else 
                            {
                            Console.SetCursorPosition(col, row);
                            Console.Write(newDisplay[row][col]);
                            Console.CursorVisible = false;
                            
                            }
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

        public static void LoadArena() // Player class is clearing console b4 using this
        {
            InitializeDisplay(MapHolder.tempArenaMap);
            //TODO print player
        }

        public static void LoadCityMap() // Player class is clearing console b4 using this
        {
            // TODO umiesc gracza w miejscu bezpiecznym dla kodu
            InitializeDisplay(MapHolder.cityMap);
            //TODO print player
        }
        public static void QuitMenu()
        {
            InitializeDisplay(PrawieSingleton.player.currentMap);

        }
    }
