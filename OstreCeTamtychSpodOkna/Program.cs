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
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        Console.Clear();
        RogueTestDebug.NewMain();


        Application.Init();

        GameState gameState = new GameState(playerPokemon, enemyPokemon, skillList);

        var battleWindow = new BattleWindow(gameState);
        Application.Run(battleWindow);

        Application.Shutdown();
    }


}
public class BattleWindow : Window
{
    private GameState gameState;
    private ProgressBar playerHPBar;
    private ProgressBar enemyHPBar;

    public BattleWindow(GameState gameState) : base("Battle")
    {
        this.gameState = gameState;

        InitializeUI();
    }

    private void InitializeUI()
    {
        playerHPBar = new ProgressBar()
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(80),
            Width = Dim.Percent(80),
            Height = 1
        };
        enemyHPBar = new ProgressBar()
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(10),
            Width = Dim.Percent(80),
            Height = 1
        };
        Add(playerHPBar, enemyHPBar);

        var attackButton = new Button("Attack!")
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(90),
            ColorScheme = new ColorScheme()
            {
                Normal = Terminal.Gui.Attribute.Make(Color.BrightMagenta, Color.Red)
            }
        };
        var skillsButton = new Button("Skills")
        {
            X = Pos.Percent(30),
            Y = Pos.Percent(90)
        };
        var itemsButton = new Button("Items")
        {
            X = Pos.Percent(50),
            Y = Pos.Percent(90)
        };
        var fleeButton = new Button("Flee")
        {
            X = Pos.Percent(70),
            Y = Pos.Percent(90),
            ColorScheme = new ColorScheme()
            {
                Normal = Terminal.Gui.Attribute.Make(Color.BrightBlue, Color.Black)
            }
        };

        Add(attackButton, skillsButton, itemsButton, fleeButton);

        //Logika przycisków
        ConfigureButtonEvents(attackButton, skillsButton, itemsButton, fleeButton);
    }

    private void ConfigureButtonEvents(Button attackButton, Button skillsButton, Button itemsButton, Button fleeButton)
    {
        menuButton.Clicked += () =>

        skillsButton.Clicked += () =>
        {
            var skillsDialog = new Dialog("Choose a skill", 60, 20)
            {
                X = Pos.Center(),
                Y = Pos.Center()
            };

            CreateSkillsGrid(skillsDialog);

            Application.Run(skillsDialog);
        };
    }

    private void UpdateHPBars()
    {
        playerHPBar.Fraction = (float)gameState.PlayerPokemon.HP / gameState.PlayerPokemon.MaxHP;
        enemyHPBar.Fraction = (float)gameState.EnemyPokemon.HP / gameState.EnemyPokemon.MaxHP;

        playerHPBar.Text = $"{gameState.PlayerPokemon.HP}/{gameState.PlayerPokemon.MaxHP}";
        enemyHPBar.Text = $"{gameState.EnemyPokemon.HP}/{gameState.EnemyPokemon.MaxHP}";
    }
    private void CreateSkillsGrid(Dialog skillsDialog)
    {
        int rows = (gameState.SkillList.Count + 1) / 2;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int skillIndex = i * 2 + j;
                if (skillIndex < gameState.SkillList.Count)
                {
                    var skill = gameState.SkillList[skillIndex];
                    var skillButton = new Button(skill.Name)
                    {
                        X = j * 20,
                        Y = i * 2,
                        ColorScheme = GetColorSchemeForSkillType(skill.Type)
                    };
                    skillsDialog.Add(skillButton);
                }
            }
        }
        var returnButton = new Button("Return")
        {
            X = Pos.Center(),
            Y = Pos.Percent(90),
        };
        returnButton.Clicked += () => { skillsDialog.Running = false; };
        skillsDialog.Add(returnButton);
    }
    private ColorScheme GetColorSchemeForSkillType(string type)
    {
        var normalAttribute = Terminal.Gui.Attribute.Make(Color.White, Color.Black);
        var focusAttribute = Terminal.Gui.Attribute.Make(Color.White, Color.Black); 

        switch (type)
        {
            case "Electric":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightYellow, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.White, Color.Black);
                break;
            case "Fire":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.Red, Color.Black);
                break;
            case "Ice":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightBlue, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.Blue, Color.Black);
                break;
            case "Psychic":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightMagenta, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.Magenta, Color.Black);
                break;
        }

        return new ColorScheme()
        {
            Normal = normalAttribute,
            Focus = focusAttribute
        };
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