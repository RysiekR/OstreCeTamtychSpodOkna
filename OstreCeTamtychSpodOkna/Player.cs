namespace OstreCeTamtychSpodOkna
{
    internal class Player
    {


        public int row = 7;
        public int col = 15;
        string obstacle = Sprites.obstacle;
        string logicLetters = "P123456789";
        string enemyString = "123456789";

        public List<string> jakaMapa = new List<string>();
        private bool hitObstacle = false;
        public int oldRow;
        public int oldCol;
        ConsoleKey consoleKeyPressed;// = ConsoleKey.None;
        char charUp;
        char charDown;
        char charLeft;
        char charRight;

        private Dictionary<ConsoleKey, Action> movements;/* = new Dictionary<ConsoleKey, Action> { };*/
        private Dictionary<char, Action> logicFromChars;

        public Player(Map currentMap)
        {
            InitializePlayerPosition(currentMap);
            movements = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.W, () => Movement(charUp) },
                {ConsoleKey.S, () => Movement(charDown) },
                {ConsoleKey.A, () => Movement(charLeft) },
                {ConsoleKey.D, () => Movement(charRight) }
            };

            logicFromChars = new Dictionary<char, Action>
                {
                    {'P', () => changeMapTo('P')}
                };
        }
        public void UpdatePos(Map currentMap)
        {
            oldCol = col;
            oldRow = row;
            jakaMapa.Clear();
            jakaMapa.AddRange(currentMap.mapAsList);
            charUp = jakaMapa[row - 1][col];
            charDown = jakaMapa[row + 1][col];
            charLeft = jakaMapa[row][col - 1];
            charRight = jakaMapa[row][col + 1];


            /*switch (inputFromPlayer())
            {
                case ConsoleKey.W:
                    Movement(charUp);
                    break;
                case ConsoleKey.S:
                    Movement(charDown);
                    break;
                case ConsoleKey.A:
                    Movement(charLeft);
                    break;
                case ConsoleKey.D:
                    Movement(charRight);
                    break;
            }*/

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
                    if (!enemyString.Contains(jakaMapa[oldRow][oldCol]))
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
        // i sprawdza co tam jest i robi co trzeba(mam nadzieje)
        void Movement(char charInThisDirection)
        {
            //sprawdzenie czy char gdzie idziemy jest charem ze string z przeszkodami nie do przejscia
            if (obstacle.Contains(charInThisDirection))
            {
                unpassableObstacle(charInThisDirection);
            }
            // miejsce z logika znaków z ktorymi jest jakas interakcja
            else if (logicLetters.Contains(charInThisDirection))
            {
                if (charInThisDirection == 'P')
                {
                    changeMapTo(charInThisDirection);
                }
                else if (enemyString.Contains(charInThisDirection))
                {
                    Console.Beep(500, 400);// tutaj wywolac walke TODO MIODEK
                }
            }
            // tutaj jak moze normalnie chodzic to zmienia pozycje row / col
            else
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

        void unpassableObstacle(char charToLogic)
        {
            if (obstacle.Contains(charToLogic))
            {
                hitObstacle = true;
            }

            else { Console.WriteLine("Error Player.UpdatePos.UnpassableObstacle"); }

        }

        void changeMapTo(char letterOfTheMap)
        {
            //TODO tu jest jeszcze do dopracowania , trzeba uzywac gamestatu

            Map tempMap = new Map(Sprites.map2);
            switch (letterOfTheMap)
            {
                case 'P':
                    {
                        jakaMapa.Clear();
                        jakaMapa.AddRange(tempMap.mapAsList);
                        break;
                    }
            }
        }




        //TODO zrobic check czy ten klawisz ma zastosowanie
        //jak nie ma to jeszcze raz go zczytac
        //i wyswietlic "nie dotykaj klawiszy ktore nic nie robia łobuzie"
        // metoda ktora zczytuje i zwraca konkretny przycisk z klawiatury
        ConsoleKey inputFromPlayer()
        {
            consoleKeyPressed = Console.ReadKey(true).Key;
            return consoleKeyPressed;
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
    }
}




