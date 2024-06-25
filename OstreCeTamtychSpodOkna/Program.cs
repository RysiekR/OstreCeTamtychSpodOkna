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
        Menu.ShowMenu();
        Console.WriteLine("You should go to Rescue Center: R. Buy Creature. \nYou can press M to see Menu.\n");

        //Start Game
        Display.InitializeDisplay(MapHolder.cityMap);
        Display.SetNewDisplay(player.currentMap);
        Display.RenderDisplay();
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