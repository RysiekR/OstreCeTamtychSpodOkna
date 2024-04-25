using Microsoft.VisualBasic;
using Terminal.Gui;
using System.Data;
using OstreCeTamtychSpodOkna;
using System.Media;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Press alt+enter or f11");
        Console.ReadLine();
        Console.Clear();
        RogueTestDebug.NewMain();

/*
        Application.Init();

        GameState gameState = new GameState(playerPokemon, enemyPokemon, skillList);

        var battleWindow = new BattleWindow(gameState);
        Application.Run(battleWindow);

        Application.Shutdown();*/
    }


}
