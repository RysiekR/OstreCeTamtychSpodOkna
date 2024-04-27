using Microsoft.VisualBasic;
using Terminal.Gui;
using System.Data;
using OstreCeTamtychSpodOkna;
using System.Media;

public class Program
{
    public static void Main()
    {
        //Application.Init();
        Console.WriteLine("Press alt+enter or f11");
        Console.ReadLine();
        Console.Clear();

        //Start Game
        Player player = new(MapHolder.cityMap);
        for (int i = 0; i < 10; i++) //Init Debug Dummies
        {
            MapHolder.cityMap.enemyList.Add(new Enemy(6, i + 10, MapHolder.cityMap));
        }

        Display.InitializeDisplay(MapHolder.cityMap);

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