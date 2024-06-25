using OstreCeTamtychSpodOkna;

public static class Rescue
{
    static Player player = PrawieSingleton.player;
    public static List<Pokemon> transferedPokemons = new List<Pokemon>();
    static int howManyTransferred = 0;
    public static void OpenRescueMenu()
    {
        Console.CursorVisible = true;
        bool con1 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Creatures Rescue Center:");
            Console.WriteLine("Esc to exit");
            Console.WriteLine("1 to buyout creatures");
            Console.WriteLine("2 to see transfered rescued Creatures");
            Console.WriteLine("3 to transfer all Creatures");
            Console.WriteLine($"You have transfered {howManyTransferred}.");
            if (player.rescuedPokemons.Count > 0)
            {
                Console.WriteLine("You can still transfer some.");
            }
            /*            foreach (Pokemon p in player.rescuedPokemons)
                        {
                            p.PokemonInfoPrint();
                        }
            */
            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con1 = false; break;
                case ConsoleKey.D1: BuyOut(); break;
                case ConsoleKey.D2: TransferedPokemons(); break;
                case ConsoleKey.D3: TransferAll(); break;
            }

        } while (con1);
        Console.CursorVisible = false;
        Display.QuitMenu();
    }
    private static void BuyOut()
    {
        bool con7 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Here you can BuyOut rescued Creatures");
            Console.WriteLine($"You have {player.tempIntFromNs} BuyOut currency");
            Console.WriteLine("Esc to exit");
            Console.WriteLine("press 1 to show transfered Creatures");
            Console.WriteLine();

            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con7 = false; break;
                case ConsoleKey.D1: BuyOutPhase2(); break;
            }

        } while (con7);
    }
    private static void BuyOutPhase2()
    {
        Console.Clear();
        Console.WriteLine("Welcome to buyout menu.");
        Console.WriteLine("You can pick Creature, remember you have to have enough currency.");
        Console.WriteLine("Also remember you have to transfer Creatures to see them(last transfers are at the bottom)");
        Console.WriteLine($"You have {player.tempIntFromNs} BuyOut currency");
        int number = 1;
        foreach (Pokemon p in transferedPokemons)
        {
            Console.WriteLine($"{number}. {p.Name} cost: {p.level.level}");
            number++;
        }

        //parsowanie i logika

        Console.WriteLine("Write number coresponding to creature to skill change - Enter to confirm. Or 0 to exit");
        Console.WriteLine();

        string choosenIndexOfPokemon = Console.ReadLine();
        if (int.TryParse(choosenIndexOfPokemon, out int numerIndexPokemon))
        {
            if (numerIndexPokemon == 0 || numerIndexPokemon - 1 > transferedPokemons.Count || transferedPokemons.Count == 0)
            {
                return;
            }
            Pokemon pokemonToBuyOut = transferedPokemons[numerIndexPokemon - 1];
            if (pokemonToBuyOut.level.level > player.tempIntFromNs)
            {
                Console.WriteLine("Not enough currency... Defeat more enemies");
                Console.WriteLine("press any key to continue");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine($"BoughtOut {pokemonToBuyOut.Name}");
                player.pokemonList.Add(pokemonToBuyOut);
                transferedPokemons.Remove(pokemonToBuyOut);
                player.tempIntFromNs -= pokemonToBuyOut.level.level;
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }
        }
    }
    private static void TransferAll()
    {
        int money = 0;
        foreach (Pokemon p in player.rescuedPokemons)
        {
            player.money += p.ExpAfterWin();
            money += p.ExpAfterWin();
            transferedPokemons.Add(p);
            howManyTransferred++;
        }
        player.rescuedPokemons.Clear();
        Console.WriteLine($"Transfered! and got {money} money. ");
        Console.WriteLine("press any key to continue");
        Console.ReadKey();
    }
    private static void TransferedPokemons()
    {
        bool con6 = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Transfered rescued Creatures info");
            Console.WriteLine("Esc to exit");
            Console.WriteLine();
            int number = 1;
            foreach (Pokemon p in transferedPokemons)
            {
                Console.WriteLine($"{number}. cost: {p.level.level}");
                p.PokemonInfoPrint();
                Console.WriteLine();
                number++;
            }
            Console.ReadKey();

            ConsoleKey pressedKey0 = Console.ReadKey(true).Key;
            switch (pressedKey0)
            {
                case ConsoleKey.Escape: con6 = false; break;
            }

        } while (con6);
    }
}
