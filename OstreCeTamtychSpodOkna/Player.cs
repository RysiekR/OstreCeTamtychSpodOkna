namespace OstreCeTamtychSpodOkna
{
    internal class Player
    {
        public bool isOnArena = false;
        private int grassPoints = 0;
        public int row = 7;
        public int col = 15;
        string obstacle = Sprites.obstacle;
        string logicLetters = "P123456789";
        string enemyString = "123456789";

        public List<string> jakaMapa = new List<string>();
        public List<string> cityMap = new List<string>();
        private bool hitObstacle = false;
        public int oldRow;
        public int oldCol;
        ConsoleKey consoleKeyPressed;// = ConsoleKey.None;

        private Dictionary<ConsoleKey, Action> movements;
        private Dictionary<char, Action> logicFromChars;

        public Player(Map currentMap)
        {
            InitializePlayerPosition(currentMap);
            movements = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.W, () => Movement(jakaMapa[row - 1][col]) },
                {ConsoleKey.S, () => Movement(jakaMapa[row + 1][col]) },
                {ConsoleKey.A, () => Movement(jakaMapa[row][col - 1]) },
                {ConsoleKey.D, () => Movement(jakaMapa[row][col + 1]) },
                {ConsoleKey.P, () => PrintPoints() },
                {ConsoleKey.M, () => ShowMenu() }
        };

            logicFromChars = new Dictionary<char, Action>
                {
                    //tutaj dodawac logike zwiazana z np sklepami i szpitalami
                    {'P', () => changeMapTo('P')},
                    {',', () => grassPoints++ },
                    {'A', () => changeMapTo('A') },
                    {'C', () => changeMapTo('C') }
                };
            //dodanie kazdego znaku przez ktory nie mozna przejsc
            foreach (char obstacleChar in obstacle)
            {
                logicFromChars[obstacleChar] = () => hitObstacle = true;
            }
            //dodanie znakow enemies
            foreach (char enemy in enemyString)
            {
                logicFromChars[enemy] = () => Console.Beep(500, 400); //TODO MIODEK walka
            }
        }
        public void UpdatePos(Map currentMap)
        {
            oldCol = col;
            oldRow = row;
            jakaMapa.Clear();
            jakaMapa.AddRange(currentMap.mapAsList);

            consoleKeyPressed = Console.ReadKey(true).Key;
            if (movements.TryGetValue(consoleKeyPressed, out var movementAction))
            {
                movementAction();

                //if wall was not hit: move player and clear old position
                if (!hitObstacle)
                {
                    //jak moze isc
                    jakaMapa[row] = jakaMapa[row].Insert(col, "#");
                    jakaMapa[row] = jakaMapa[row].Remove(col + 1, 1);
                    if (!enemyString.Contains(jakaMapa[oldRow][oldCol]) && ((col != oldCol) || (row != oldRow)))
                    {
                        jakaMapa[oldRow] = jakaMapa[oldRow].Insert(oldCol, " ");
                        jakaMapa[oldRow] = jakaMapa[oldRow].Remove(oldCol + 1, 1);
                    }

                }
                hitObstacle = false;

                //zapisanie zmodyfikowanej mapy
                currentMap.mapAsList.Clear();
                currentMap.mapAsList.AddRange(jakaMapa);
            }

        }

        // bierze i sprawdza char w kierunku w ktorym chcemy sie poruszyc
        // i sprawdza co tam jest i robi co trzeba(mam nadzieje)(teraz dolozylem slownik to juz wcale nie mam pewnosci)
        void Movement(char charInThisDirection)
        {
            //sprawdz biblioteke i jezeli cos trzeba to to zrob jak nie to wykonaj switch
            if (logicFromChars.TryGetValue(charInThisDirection, out var action))
            {
                action();
            }
            // tutaj jak moze normalnie chodzic to zmienia pozycje row / col
            if (!hitObstacle)
            {
                switch (consoleKeyPressed)
                {
                    case ConsoleKey.W:
                        { row--; }
                        break;
                    case ConsoleKey.S:
                        { row++; }
                        break;
                    case ConsoleKey.D:
                        { col++; }
                        break;
                    case ConsoleKey.A:
                        { col--; }
                        break;
                }
            }
        }

        void changeMapTo(char letterOfTheMap)
        {
            //TODO tu jest jeszcze do dopracowania , trzeba uzywac gamestatu
            hitObstacle = true;
            Map tempMap;// = new Map(Sprites.map2);
            switch (letterOfTheMap)
            {
                case 'P':
                    {
                        tempMap = new Map(Sprites.map2);
                        jakaMapa.Clear();
                        jakaMapa.AddRange(tempMap.mapAsList);
                        break;
                    }
                case 'A':
                    {
                        //uzywac listy enemies z gamestate
                        tempMap = new Map(Sprites.arena);

                        RogueTestDebug.enemies.Clear();
                        RogueTestDebug.enemies.Add(new Enemy(6, 6, tempMap));
                        RogueTestDebug.enemies.Add(new Enemy(7, 7, tempMap));
                        RogueTestDebug.enemies.Add(new Enemy(9, 9, tempMap));
                        RogueTestDebug.enemies.Add(new Enemy(15, 15, tempMap));


                            jakaMapa[oldRow] = jakaMapa[oldRow].Insert(oldCol, " ");
                            jakaMapa[oldRow] = jakaMapa[oldRow].Remove(oldCol + 1, 1);


                        cityMap.Clear();
                        cityMap.AddRange(jakaMapa);
                        jakaMapa.Clear();
                        jakaMapa.AddRange(tempMap.mapAsList);
                        isOnArena = true;
                        break;
                    }
                case 'C':
                    {
                        jakaMapa.Clear();
                        jakaMapa.AddRange(cityMap);
                        isOnArena = false;
                        break;
                    }
            }

        }

        private void InitializePlayerPosition(Map currentMap)
        {
            //pobranie mapy
            jakaMapa.Clear();
            jakaMapa.AddRange(currentMap.mapAsList);
            //wstawienie gracza
            jakaMapa[row] = jakaMapa[row].Insert(col, "#");
            jakaMapa[row] = jakaMapa[row].Remove(col + 1, 1);
            //wyplucie mapy z graczem
            currentMap.mapAsList.Clear();
            currentMap.mapAsList.AddRange(jakaMapa);
        }

        private void PrintPoints()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(80, i + 5);
                Console.Write(" ");
            }
            Console.SetCursorPosition(80, 5);
            Console.Write(grassPoints);
        }

        private void ShowMenu()
        {
            Console.Clear();
            bool condition = true;
            while (condition)
            {
                Console.WriteLine("Menu text, 1 to get back to game");
                string check = Console.ReadLine();
                if (check == "1")
                {

                    condition = false;
                }
                else if (check == "2")
                {
                    // cos zrob
                }
                else if (check == "3") { }
            }
            Console.Clear();
            Display.PrintToConsole();
        }
    }
}

