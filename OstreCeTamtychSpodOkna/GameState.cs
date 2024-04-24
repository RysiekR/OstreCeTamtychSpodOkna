using OstreCeTamtychSpodOkna;
//Klasa przechowująca stan gry
/*internal class GameState
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
}*/