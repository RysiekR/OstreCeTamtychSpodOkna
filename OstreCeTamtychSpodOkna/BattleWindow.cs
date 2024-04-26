using Microsoft.VisualBasic;
using Terminal.Gui;
using System.Data;
using OstreCeTamtychSpodOkna;
using System.Media;
using System;
using System.Security.Cryptography.X509Certificates;

public class BattleProgram
{
    public static void HujANieMain()
    {
        Console.WriteLine("Press alt+enter or f11");
        Console.ReadLine();
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        Console.Clear();

        Application.Init();
        
        Map cityMap = new Map(Sprites.city);
        Enemy enemy = new Enemy(1, 1, cityMap);
        Player aPlayer = new Player(cityMap);
        var battleWindow = new BattleWindow(aPlayer,enemy);
        Application.Run(battleWindow);

        Application.Shutdown();
    }
}
internal class BattleWindow : Window
{
    private Pokemon playerPokemon;
    private Pokemon enemyPokemon;
    private Player aPlayer;
    private Enemy enemy;
    private ProgressBar playerHPBar;
    private ProgressBar enemyHPBar;
    private Label playerHPLabel;
    private Label enemyHPLabel;

    public void ChoosePokemon(HasPokemonList entity)
    {
        foreach(Pokemon pokemon in entity.pokemonList)
        {
            if (pokemon.stats.IsAlive)
            {
                if (entity is Player)
                {
                    playerPokemon = pokemon; break;
                }     
                else if (entity is Enemy)
                {
                    enemyPokemon = pokemon; break;
                }
            }
            else
            {
                Console.WriteLine("Error: Wszystkie Pokemony są niezdolne do walki.");
            }
        }
    }
    public BattleWindow(Player aPlayer, Enemy enemy) : base("Battle")
    {
        this.aPlayer = aPlayer;
        this.enemy = enemy;
        ChoosePokemon(aPlayer);
        ChoosePokemon(enemy);

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

        playerHPLabel = new Label($"HP: {playerPokemon.stats.Hp}/{playerPokemon.stats.maxHp}")
        {
            X = Pos.Left(playerHPBar),
            Y = Pos.Top(playerHPBar) - 1,
        };

        enemyHPLabel = new Label($"HP: {enemyPokemon.stats.Hp}/{enemyPokemon.stats.maxHp}")
        {
            X = Pos.Left(enemyHPBar),
            Y = Pos.Top(enemyHPBar) - 1,
        };

        Add(playerHPLabel, enemyHPLabel);
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
        //menuButton.Clicked += () =>

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
        playerHPBar.Fraction = playerPokemon.stats.Hp / playerPokemon.stats.maxHp;
        enemyHPBar.Fraction = enemyPokemon.stats.Hp / enemyPokemon.stats.maxHp;

        playerHPLabel.Text = $"HP: {playerPokemon.stats.Hp}/{playerPokemon.stats.maxHp}";
        enemyHPLabel.Text = $"HP: {enemyPokemon.stats.Hp}/{enemyPokemon.stats.maxHp}";
    }
    private void CreateSkillsGrid(Dialog skillsDialog)
    {
        int rows = (playerPokemon.allSkills.Count + 1) / 2;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int skillIndex = i * 2 + j;
                if (skillIndex < playerPokemon.allSkills.Count)
                   // foreach(SkillCategory skill in playerPokemon.allSkills)
                {
                    SkillCategory skill = playerPokemon.allSkills[skillIndex];
                    var skillButton = new Button(skill.name)
                    {
                        X = j * 20,
                        Y = i * 2,
                       // ColorScheme = GetColorSchemeForSkillType(skill.Type)
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
    internal static void BattleStart(Player aPlayer, Enemy aEnemy)
    {
        Application.Init();

        var battleWindow = new BattleWindow(aPlayer,aEnemy);
        Application.Run(battleWindow);

        Application.Shutdown();
    }
}
public enum GameMode
{
    Exploration,
    Combat
}
