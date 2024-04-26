
using OstreCeTamtychSpodOkna;

public class Player : HasPokemonList
    {
        public bool isOnArena = false;
        private int grassPoints = 0;
        private int tempIntFromNs = 0;
        public int row = 7;
        public int col = 15;
        private int possibleRow;
        private int possibleCol;
        string enemyString = "123456789";
        public Map currentMap;

        private bool hitObstacle = false;
        ConsoleKey consoleKeyPressed;// = ConsoleKey.None;

        private Dictionary<ConsoleKey, Action> movements;
        private Dictionary<char, Action> logicFromChars;

        public Player(Map currentMap)
        {
            this.currentMap = currentMap;
            InitializePlayerPosition();
            pokemonList.Clear();
            pokemonList.Add(new Pokemon("Pikachu"));
            pokemonList.Add(new Pokemon("Bulbazaur"));
            pokemonList.Add(new Pokemon("Squirtle"));
            pokemonList.Add(new Pokemon("Charmander"));

            //slowniki
            movements = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.W, () => Movement(row - 1,col) },
                {ConsoleKey.S, () => Movement(row + 1, col) },
                {ConsoleKey.A, () => Movement(row, col - 1) },
                {ConsoleKey.D, () => Movement(row, col + 1) },
                {ConsoleKey.P, () => PrintPoints() },
                {ConsoleKey.N, () => PrintNPoints() },
                {ConsoleKey.M, () => ShowMenu() }
        };

            logicFromChars = new Dictionary<char, Action>
                {
                    //tutaj dodawac logike zwiazana z np sklepami i szpitalami
                    {'P', () => changeMapTo('P')},
                    {',', () => grassPoints++ },
                    {'N', () => tempIntFromNs++ },
                    {'A', () => changeMapTo('A') },
                    {'C', () => changeMapTo('C') },
                    //{'F', () => FightyFight() }
                };
            //dodanie kazdego znaku przez ktory nie mozna przejsc
            foreach (char obstacleChar in Sprites.obstacle)
            {
                logicFromChars[obstacleChar] = () => hitObstacle = true;
            }
            //dodanie znakow enemies
            foreach (char enemy in enemyString)
            {
                logicFromChars[enemy] = () => FightyFight(); //TODO MIODEK walka
            }
        }
        public void UpdatePos()
        {
            consoleKeyPressed = Console.ReadKey(true).Key;

            if (movements.TryGetValue(consoleKeyPressed, out var movementAction))
            {
                movementAction(); // tu jest wykonanie slownika
            }
        }

        // bierze i sprawdza char w kierunku w ktorym chcemy sie poruszyc
        // i sprawdza co tam jest i robi co trzeba(mam nadzieje)(teraz dolozylem slownik to juz wcale nie mam pewnosci)
        void Movement(int futureRow, int futureCol)
        {
            char charInThisDirection = this.currentMap.mapAsList[futureRow][futureCol];
            int oldCol = col;
            int oldRow = row;
            possibleRow = futureRow;
            possibleCol = futureCol;

            //sprawdz slownik i jezeli cos sie da zrobic to zrob
            if (logicFromChars.TryGetValue(charInThisDirection, out var action))
            {
                action(); //wykonanie akcji slownika z literkami logicznymi z mapy
            }

            // tutaj jak wykonaniu ze slownika mozna chodzic to zmienia pozycje row / col
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

                //wpisanie ruchu do mapy
                currentMap.mapAsList[row] = currentMap.mapAsList[row].Insert(col, "#");
                currentMap.mapAsList[row] = currentMap.mapAsList[row].Remove(col + 1, 1);
                currentMap.mapAsList[oldRow] = currentMap.mapAsList[oldRow].Insert(oldCol, " ");
                currentMap.mapAsList[oldRow] = currentMap.mapAsList[oldRow].Remove(oldCol + 1, 1);
            }
            hitObstacle = false;



        }

        void changeMapTo(char letterOfTheMap)
        {
            hitObstacle = true;
            switch (letterOfTheMap)
            {
                case 'A':
                    {
                        MapHolder.GenerateArena();
                        currentMap = MapHolder.tempArenaMap;
                        InitializePlayerPosition();
                        Display.LoadArena();

                        int oldCol = col;
                        int oldRow = row;
                        MapHolder.cityMap.mapAsList[oldRow] = MapHolder.cityMap.mapAsList[oldRow].Insert(oldCol, " ");
                        MapHolder.cityMap.mapAsList[oldRow] = MapHolder.cityMap.mapAsList[oldRow].Remove(oldCol + 1, 1);

                        isOnArena = true;
                        break;
                    }
                case 'C':
                    {
                        currentMap = MapHolder.cityMap;
                        InitializePlayerPosition();
                        Display.LoadCityMap();
                        isOnArena = false;
                        break;
                    }
            }

        }

        private void InitializePlayerPosition()
        {
            //wstawienie gracza
            currentMap.mapAsList[row] = currentMap.mapAsList[row].Insert(col, "#");
            currentMap.mapAsList[row] = currentMap.mapAsList[row].Remove(col + 1, 1);
        }

        private void PrintPoints()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(80 + i ,5);
                Console.Write(" ");
            }
            Console.SetCursorPosition(80, 5);
            Console.Write(grassPoints);
        }
        private void PrintNPoints()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(80 + i,6);
                Console.Write(" ");
            }
            Console.SetCursorPosition(80, 6);
            Console.Write(tempIntFromNs);
        }

        private void ShowMenu()
        {
            Console.Clear();
            bool condition = true;
            while (condition)
            {
                Console.WriteLine("Menu text, 1 to get back to game");
                string? check = Console.ReadLine();
                if (check == "1")
                {

                    condition = false;
                }
                else if (check == "2")
                {
                    //ChosePokemon();
                    Console.WriteLine($"Chosen Pokemon: {ChosePokemon().Name}");
                    Console.ReadLine();
                }
                else if (check == "3") { }
            }
            Console.Clear();
            Display.PrintToConsole();
        }
        public void PutAvatarOnMap(Map whichMap)
        {

        }
        //TODO miodek
        public void FightyFight()
        {
            hitObstacle = true;
            Console.Beep();
            List<Enemy> enemiesToRemove = new List<Enemy>();
            foreach (Enemy enemy in currentMap.enemyList)
            {
                if (enemy.row == possibleRow && enemy.col == possibleCol)
                {
                    BattleProgram.HujANieMain(this, enemy);

                    enemiesToRemove.Add(enemy);//tutaj wywolac walke, nie zabijac jeszcze !!!!
                }
            }
            foreach (Enemy enemy in enemiesToRemove)
            {
                enemy.GetRidOfThisAvatar();
                currentMap.enemyList.Remove(enemy);//tutaj zabic !!
            }
        }
        private Pokemon ChosePokemon()
        {
            Console.WriteLine("Chose pokemon, this is your list");
            for (int i = 0; i < pokemonList.Count; i++)
            {
                Console.WriteLine($"{i}. {pokemonList[i].Name}");
            }

            bool chosenWrong = true;
            while (chosenWrong)
            {
                string toParse = Console.ReadLine();
                if (int.TryParse(toParse, out int index))
                {
                    // Ensure the index is within the bounds of the pokemonList array
                    if (index >= 0 && index < pokemonList.Count)
                    {
                        return pokemonList[index];
                    }
                    else
                    {
                        Console.WriteLine("Index is out of range.");
                        chosenWrong = true;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                    chosenWrong = true;
                }
            }
            Console.WriteLine("cos sie wyjebało wiec zwracam pierwszego pokemona");
            return pokemonList[0];
        }
    }


/*public struct Position
{
    public Position(int rowIn, int colIn)
    {
        _row = rowIn; _col = colIn;
    }
    public int row { get => _row; set { if (value < 0 && _row + value < 0) { _row = 0; } else { _row += value; } } }
    public int col { get => _col; set { if (value < 0 && _col + value < 0) { _col = 0; } else { _col += value; } } }

    private int _row;
    private int _col;
}*/