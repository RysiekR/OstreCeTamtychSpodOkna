using OstreCeTamtychSpodOkna;

public class Program
{
    public static void Main()
    {
        Player player = new(MapHolder.cityMap);
        //Application.Init();
        Console.WriteLine("Press alt+enter or f11");
        Console.WriteLine("If fullscreen then continue");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        /*        Console.Clear();
                Console.WriteLine("You are: #");
                Console.WriteLine("You should go to Rescue Center: R. Buy Creature. \nYou can press M to see Menu.\n");
                Console.WriteLine("To buy more Creatures: \n" +
                    "1. Defeat Enemies\n" +
                    "2. Transfer rescued Creatures in Rescue Center\n" +
                    "3. You can exchange for BuyOut currency(get 1 after each win, those Ns are BuyOut currency)\n" +
                    "4. Cost is equal to level of that Creature");
                Console.WriteLine("Shop: S");
                Console.WriteLine("Hospital: H");
                Console.WriteLine("Press any key to continue...");
                Console.SetCursorPosition(player.col + 1, player.row); Console.Write('<');
                Console.SetCursorPosition(player.col - 1, player.row); Console.Write('>');
                Console.SetCursorPosition(player.col, player.row + 1); Console.Write('^');
                Console.SetCursorPosition(player.col, player.row - 1); Console.Write('!');
                Console.ReadKey();
                Console.Clear();
        */
        Menu.ShowMenu();
        //Start Game
        Display.InitializeDisplay(MapHolder.cityMap);
        for (int i = 0; i < 10; i++) //Init Debug Dummies
        {
            MapHolder.cityMap.enemyList.Add(new Enemy(6, i + 10, MapHolder.cityMap));
        }
        Display.SetNewDisplay(player.currentMap);
        Display.RenderDisplay();
        Rescue.transferedPokemons.Add(new Pokemon());
        Rescue.transferedPokemons.Add(new Pokemon());
        Rescue.transferedPokemons.Add(new Pokemon());
        Rescue.transferedPokemons.Add(new Pokemon());


        while (true)
        {
            player.UpdatePos();
            if (player.isOnArena)
            {
                foreach (Enemy enemi in MapHolder.tempArenaMap.enemyList)
                { enemi.UpdatePos(); }
            }
            Display.SetNewDisplay(player.currentMap);
            Display.RenderDisplay();
        }
    }
}
public class HasPokemonList
{
    public List<Pokemon> pokemonList = new List<Pokemon>();

}