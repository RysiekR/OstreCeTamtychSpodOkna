using Microsoft.VisualBasic;
using Terminal.Gui;
using System.Data;
using OstreCeTamtychSpodOkna;
using System.Media;
using System.Security.Cryptography.X509Certificates;
public class Program
{
    public static void Main()
    {
    /*    Console.WriteLine("Press alt+enter or f11");
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight =Console.WindowHeight;
        Console.ReadLine();
        Console.Clear();
        RogueTestDebug.NewMain();*/


        Application.Init();

        Pokemon playerPokemon = new Pokemon("Pikachu", 100, 30, 10);
        Pokemon enemyPokemon = new Pokemon("Charmander", 100, 20, 5);
        List<Skill> skillList = new List<Skill>()
{
    new Skill ("Thunderbolt", "Electric", 90, 100),
    new Skill ("Flamethrower", "Fire", 90, 100),
    new Skill ("Ice Beam", "Ice", 90, 100),
    new Skill ("Psychic", "Psychic", 90, 100)
};
        
        GameState gameState = new GameState(playerPokemon, enemyPokemon, skillList);
        
        var battleWindow = new BattleWindow(gameState);
        Application.Run(battleWindow);
        
        Application.Shutdown();
    }
}

public class Skill
{
    public string Name { get; }
    public string Type { get; }
    public int Power { get; }
    public int Accuracy { get; }

    public Skill(string name, string type, int power, int accuracy)
    {
        Name = name;
        Type = type;
        Power = power;
        Accuracy = accuracy;
    }
}
public enum GameMode
{
    Exploration,
    Combat
}
