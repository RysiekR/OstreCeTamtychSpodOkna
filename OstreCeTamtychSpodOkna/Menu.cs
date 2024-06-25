public static class Menu
{
    static Player player = PrawieSingleton.player;
    static List<Pokemon> pokemonList = player.pokemonList;

    public static void ShowMenu()
    {
        Console.CursorVisible = true;
        bool con1 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Press Esc to exit.");

            Console.WriteLine("This is Menu.");
            Console.WriteLine("Press 1 to see creatures");
            Console.WriteLine("Press 2 to see all info about creatures");
            Console.WriteLine("Press 3 to see items");
            Console.WriteLine("Press 4 to see rescued creatures");
            Console.WriteLine("Press 5 to see where you are");
            Console.WriteLine("Press 6 to see instructions");

            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con1 = false; break;
                case ConsoleKey.D1: PokemonListMenu(); break;
                case ConsoleKey.D2: PokemonInfoAll(); break;
                case ConsoleKey.D3: ItemsListMenu(); break;
                case ConsoleKey.D4: RescuedPokemons(); break;
                case ConsoleKey.D5: ShowWhereIsPlayer(); break;
                case ConsoleKey.D6: ShowInstructions(); break;
            }
        } while (con1);
        Console.CursorVisible = false;
        Console.Clear();
        Display.QuitMenu();
    }

    private static void ShowWhereIsPlayer()
    {
        Console.Clear();
        Console.WriteLine("Press any key exit.");
        Console.WriteLine("You are here.");


        Console.SetCursorPosition(player.col + 1, player.row); Console.Write('<');
        Console.SetCursorPosition(player.col - 1, player.row); Console.Write('>');
        Console.SetCursorPosition(player.col, player.row + 1); Console.Write('^');
        Console.SetCursorPosition(player.col, player.row - 1); Console.Write('!');

        ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
        switch (pressedKey0)
        {
            case ConsoleKey.Escape: return;

        }
    }
    private static void ShowInstructions()
    {
        Console.Clear();
        Console.WriteLine("Press any key to exit.");

        Console.WriteLine("You are: #");
        Console.WriteLine("To buy more Creatures: \n" +
            "1. Defeat Enemies\n" +
            "2. Transfer rescued Creatures in Rescue Center\n" +
            "3. You can exchange for BuyOut currency(get 1 after each win, those Ns are BuyOut currency)\n" +
            "4. Cost is equal to level of that Creature");
        Console.WriteLine("Shop: S");
        Console.WriteLine("Hospital: H");
        Console.WriteLine("Rescue Center: R");
        Console.WriteLine("Enemies: \n" +
            "1. Number = how many Creatures enemy has\n" +
            "2. If number is higher then 9 its gona be: %\n" +
            "3. Color of an enemy is a type of first Creature");
        Console.WriteLine("Creatures:\n" +
            "1. Classic pokemon-ish lvls, types, skills\n" +
            "2. When you defeat enemy's Creature, Creature that has defeated gains exp(ergo grow stronger)\n" +
            "3. Dont neglect your Creatures swap them in battle for all of them to gain exp\n" +
            "4. Moist beats Lava, Lava beats Mud, Mud beats Moist");
        Console.WriteLine("Money:\n" +
            "1. Here you have money for items in Shop and BuyOut currency for Rescue Center\n" +
            "2. Money is gained after transfering Creature in Rescue Center\n" +
            "3. BuyOut currency is gained after defeating Enemy");


        ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
        switch (pressedKey0)
        {
            case ConsoleKey.Escape: return;

        }
    }
    private static void PokemonListMenu()
    {
        bool con2 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Creature list menu.");
            Console.WriteLine("Esc to exit");
            Console.WriteLine("1 to change skills");
            Console.WriteLine("2 to see all info");
            int indexPokemon = 0;
            Console.WriteLine();
            Console.WriteLine("Your Creatures Are:");
            Console.WriteLine();
            foreach (Pokemon pokemon in pokemonList)
            {
                Console.WriteLine(indexPokemon + 1 + ".");
                pokemon.PokemonInfoPrint();
                Console.WriteLine();
                indexPokemon++;
            }
            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con2 = false; break;
                case ConsoleKey.D1: PokemonSkillsToChange(); break;
                case ConsoleKey.D2: PokemonInfoAll(); break;
            }
        } while (con2);
    }

    private static void PokemonInfoAll()
    {
        bool con4 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("All info menu");
            Console.WriteLine("Esc to exit");

            int indexPokemon = 0;
            foreach (Pokemon pokemon in pokemonList)
            {
                Console.WriteLine(indexPokemon + 1 + ".");
                pokemon.PokemonInfoPrint();
                Console.WriteLine();
                Console.WriteLine($"{pokemon.Name} skills are:");
                pokemon.AllSkillsInfoPrint();
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                indexPokemon++;
            }

            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con4 = false; break;
            }
        } while (con4);
    }
    private static void ItemsListMenu()
    {
        bool con5 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Items list menu");
            Console.WriteLine("Esc to exit");

            foreach (Item i in player.itemsList)
            {
                Console.WriteLine($"{i.Name} has power: {i.minPower}-{i.maxPower}");
            }

            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con5 = false; break;
            }

        } while (con5);
    }

    private static void RescuedPokemons()
    {
        bool con6 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Rescued Creatures info menu");
            Console.WriteLine("Esc to exit");

            foreach (Pokemon p in player.rescuedPokemons)
            {
                p.PokemonInfoPrint();
            }

            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con6 = false; break;
            }
        } while (con6);
    }
    private static void PokemonSkillsToChange()
    {
        bool con3 = true;
        do
        {
            Console.WriteLine("Skill change Menu. \nPress Esc to exit. Press any to continue");
            Console.WriteLine("Do or Do not abuse to have fun.");
            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con3 = false; break;
            }

            if (con3)
            {
                Console.Clear();
                Console.WriteLine("Change skills Menu.");
                int indexPokemon = 0;
                foreach (Pokemon pokemon in pokemonList)
                {
                    Console.WriteLine(indexPokemon + 1 + ".");
                    pokemon.PokemonInfoPrint();
                    Console.WriteLine();
                    indexPokemon++;
                }
                Console.WriteLine("Write number coresponding to creature to skill change - Enter to confirm. Or 0 to exit");
                Console.WriteLine();

                string choosenIndexOfPokemon = Console.ReadLine();
                if (int.TryParse(choosenIndexOfPokemon, out int numerIndexPokemon))
                {
                    if (numerIndexPokemon == 0 || (numerIndexPokemon - 1) > pokemonList.Count || pokemonList.Count == 0)
                    {
                        return;
                    }
                    Console.Clear();
                    Pokemon pokemonToChangeSkill = pokemonList[numerIndexPokemon - 1];
                    Console.WriteLine($"You choose {pokemonToChangeSkill.Name} and his skills are: ");

                    pokemonToChangeSkill.AllSkillsInfoPrint();

                    Console.WriteLine("Which skill to reroll? (use numbers not words, no wierd buttons)");
                    Console.WriteLine("0 to exit.");
                    Console.WriteLine();
                    string stringToInt = Console.ReadLine();
                    DeletePokemonSkill(stringToInt, pokemonToChangeSkill);
                }
                else
                {
                    Console.WriteLine("You have to put Integar");
                }
            }
        } while (con3);
    }
    private static void DeletePokemonSkill(string stringToInt, Pokemon pokemon)
    {
        int numerIndexPokemon = 0;

        if (int.TryParse(stringToInt, out int numer))
        {
            if (numer == 0 || numer > pokemon.allSkills.Count)
            {
                return;
            }
            numerIndexPokemon = numer - 1;
        }
        else
        {
            Console.WriteLine("Niepoprawny format. Podaj liczbę całkowitą.");
            return;
        }

        SkillCategory skillToRemove = pokemon.allSkills[numerIndexPokemon];
        pokemon.allSkills.Remove(skillToRemove);
        pokemon.FillSkillsUpToPokemonLevel();
    }
}
