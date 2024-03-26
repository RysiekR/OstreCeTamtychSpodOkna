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
//Klasa przechowująca stan gry
public class GameState
{
    public Pokemon PlayerPokemon { get; set; }
    public Pokemon EnemyPokemon { get; set; }
    public List<Skill> SkillList { get; set; }
    public GameMode CurrentMode { get; private set; }
    //public Map CurrentMap { get; set; }


    public GameState(Pokemon playerPokemon, Pokemon enemyPokemon, List<Skill> skillList)
    {
        PlayerPokemon = playerPokemon;
        EnemyPokemon = enemyPokemon;
        SkillList = skillList;
        CurrentMode = GameMode.Exploration;
        //CurrentMap = startingMap;
    }
    
    public void SwitchMode(GameMode mode)
    {
        SaveGameState();
        CurrentMode = mode;

        switch (mode)
        {
            case GameMode.Exploration:
                PrepareExplorationMode();
                break;
            case GameMode.Combat:
                PrepareCombatMode();
                break;
                //Możesz dodać więcej trybów jak coś wymyślisz.
        }
    }
    private void SaveGameState()
    {
        Console.WriteLine("Zapisywanie stanu gry...");
        //Tu można dać coś w stylu SaveToFile(gameState);
    }
    private void PrepareExplorationMode()
    {
        //np. wyświetlenie mapy, zresetowanie stanu walki.
        Console.WriteLine("Przygotowanie trybu eksploracji...");
        //Jakieś cuda typu: currentMap.Display();
        //combatSystem.Reset();
    }

    private void PrepareCombatMode()
    {
        //Tu jakiś wybór przeciwnika, inicjalizacja walki itd.
        Console.WriteLine("Przygotowanie trybu walki...");
        //Coś typu: enemySelector.ChooseEnemy();
        //combatSystem.Initialize(playerPokemon, enemyPokemon);
    }
}