﻿using OstreCeTamtychSpodOkna.Equipment;
using Terminal.Gui;

public class Player : HasPokemonList
{
    public bool isOnArena = false;
    private int grassPoints = 0;
    public int tempIntFromNs = 1;
    public int row = 11;
    public int col = 51;
    private int possibleRow;
    private int possibleCol;
    string enemyString = "123456789";
    public Map currentMap;
    public List<Item> itemsList = new List<Item>();
    public int money = 400;

    private bool hitObstacle = false;
    ConsoleKey consoleKeyPressed;
    private Dictionary<ConsoleKey, Action> movements;
    private Dictionary<char, Action> logicFromChars;

    public List<Pokemon> rescuedPokemons;

    public Player(Map currentMap)
    {
        rescuedPokemons = new List<Pokemon>();
        this.currentMap = currentMap;
        InitializePlayerPosition();
        pokemonList.Clear();
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

        logicFromChars = new Dictionary<char, Action>//tutaj dodawac logike zwiazana z np sklepami i szpitalami
                {
                    {'P', () => ChangeMapTo('P')},
                    {',', () => grassPoints++ },
                    {'N', () => tempIntFromNs++ },
                    {'A', () => ChangeMapTo('A') },
                    {'C', () => ChangeMapTo('C') },
                    {'H', () => UseHospital() },
                    {'S', () => UseShop() },
                    {'R', () => RescueMenu() },
                };

        foreach (char obstacleChar in Sprites.obstacle)//dodanie kazdego znaku przez ktory nie mozna przejsc
        {
            logicFromChars[obstacleChar] = () => hitObstacle = true;
        }

        foreach (char enemy in enemyString)//dodanie znakow enemies
        {
            logicFromChars[enemy] = () => FightyFightChecker(); //TODO MIODEK walka
        }
        PrawieSingleton.player = this;
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
    // i sprawdza co tam jest i robi co trzeba(mam nadzieje)
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
    void ChangeMapTo(char letterOfTheMap)
    {
        hitObstacle = true;
        Console.Clear();
        switch (letterOfTheMap)
        {
            case 'A':
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
            case 'C':

                row = 12;
                col = 14;
                currentMap = MapHolder.cityMap;
                InitializePlayerPosition();
                Display.LoadCityMap();
                isOnArena = false;
                break;
        }
    }
    void UseHospital()
    {
        hitObstacle = true;
        Hospital.HealPokemons();
    }
    void UseShop()
    {
        Application.Init();
        hitObstacle = true;
        var shopDialog = new Dialog("Buy an item", 60, 20)
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        ShopWindow shopWindow = new ShopWindow(this);

        shopWindow.CreateShopGrid(shopDialog);

        Application.Run(shopDialog);
        Application.Shutdown();

    }
    private void RescueMenu()
    {
        hitObstacle = true;
        Rescue.OpenRescueMenu();
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
            Console.SetCursorPosition(80 + i, 5);
            Console.Write(" ");
        }
        Console.SetCursorPosition(80, 5);
        Console.Write(grassPoints);
    }
    private void PrintNPoints()
    {
        for (int i = 0; i < 4; i++)
        {
            Console.SetCursorPosition(80 + i, 6);
            Console.Write(" ");
        }
        Console.SetCursorPosition(80, 6);
        Console.Write(tempIntFromNs);
    }
    private void ShowMenu()
    {
        Menu.ShowMenu();
    }
    public void FightyFightChecker()
    {
        hitObstacle = true;
        if (pokemonList.Count > 0)//sprawdzenie czy ma jakiego kolwiek pokemona jak nie ma to sie tylko odbija
        {
            FightyFight();
        }
        else
        {
            Application.Init();
            var messageDialog = new Dialog("Brak Pokemonów", 60, 7);

            var tryAgainButton = new Button("Zdobądź Pokemony!")
            {
                X = Pos.Percent(60),
                Y = Pos.Center(),
            };
            tryAgainButton.Clicked += () =>
            {
                Application.RequestStop(); //Zamknięcie tylko tego okna dialogowego
            };

            messageDialog.AddButton(tryAgainButton);
            Application.Run(messageDialog);
            Application.Shutdown();
            return;
        }
    }


    public void FightyFight()
    {
        hitObstacle = true;
        // Sprawdzenie, czy gracz ma jakiekolwiek żywe Pokemony przed rozpoczęciem walki
        if (!pokemonList.Any(p => p.stats.IsAlive))
        {
            Application.Init();
            var messageDialog = new Dialog("Brak Pokemonów", 60, 7);

            var tryAgainButton = new Button("Ulecz pokemony!")
            {
                X = Pos.Percent(60),
                Y = Pos.Center(),
            };
            tryAgainButton.Clicked += () =>
            {
                Application.RequestStop(); //Zamknięcie tylko tego okna dialogowego
            };

            messageDialog.AddButton(tryAgainButton);
            Application.Run(messageDialog);
            Application.Shutdown();
            return; //Zakończenie metody, jeśli gracz nie ma Pokemonów
        }

        Enemy enemyDefeated = null;
        Enemy enemy = MapHolder.FindEnemyOnArenaAt(possibleRow, possibleCol);
        if (enemy != null) 
        {
            BattleProgram.BattleWindowHolder(this, enemy); // WALKA NA KONKRETNYM PRZECIWNIKU TUTAJ, TUTAJ MASZ ENEMY KTORY JEST W TRAKCIE WALKI
            enemy.AssignAvatar();
            if (!(enemy.pokemonList.Any(p => p.stats.IsAlive)))
            {
                enemyDefeated = enemy;
                foreach(Pokemon pokemon in enemy.RollForRescuedPokemons()) // mechanika ratowania(losowa)
                {
                    rescuedPokemons.Add(pokemon);
                }
            }
        }
        if (enemyDefeated != null)
        {
            enemyDefeated.GetRidOfThisAvatar();
            currentMap.enemyList.Remove(enemyDefeated); // Tutaj zabić !!
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
public static class Hospital
{
    public static void HealPokemons()
    {
        foreach (Pokemon p in PrawieSingleton.player.pokemonList)
        {
            p.ResetStats();
        }
    }
}