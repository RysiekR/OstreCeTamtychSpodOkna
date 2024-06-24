using OstreCeTamtychSpodOkna;

public static class Menu
{
    static Player player = PrawieSingleton.player;
    static List<Pokemon> pokemonList = player.pokemonList;

    public static void ShowMenu()
    {
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

            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con1 = false; break;
                case ConsoleKey.D1: PokemonListMenu(); break;
                case ConsoleKey.D2: PokemonInfoAll(); break;
                case ConsoleKey.D3: ItemsListMenu(); break;
                case ConsoleKey.D4: RescuedPokemons(); break;

            }
        } while (con1);
        Display.QuitMenu();
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

            foreach(Item i in player.itemsList)
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
        // from previous list which pokemon to chose skills to chose to change
        bool con3 = true;
        do
        {
            Console.WriteLine("Skill change Menu. \nPress Esc to exit. Press any to continue");
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
                    if (numerIndexPokemon == 0 || numerIndexPokemon - 1 >pokemonList.Count)
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
