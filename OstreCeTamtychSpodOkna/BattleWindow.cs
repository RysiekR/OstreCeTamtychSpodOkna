using Microsoft.VisualBasic;
using Terminal.Gui;
using System.Data;
using OstreCeTamtychSpodOkna;
using System.Media;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Numerics;

public class BattleProgram
{
    public static void HujANieMain(Player aPlayer, Enemy enemy)
    {
      
        Application.Init();
        
       Map cityMap = new Map(Sprites.city);
        Enemy bEnemy = new Enemy(1, 1, cityMap);
        Player bPlayer = new Player(cityMap);

        var battleWindow = new BattleWindow(aPlayer,enemy);
        Application.Run(battleWindow);
        Application.Shutdown();
    }
}
public class BattleWindow : Window
{
    private Pokemon? playerPokemon;
    private Pokemon? enemyPokemon;
    private Player aPlayer;
    private Enemy enemy;
    private ProgressBar? playerHPBar;
    private ProgressBar? playerShieldBar;
    private ProgressBar? enemyHPBar;
    private ProgressBar? enemyShieldBar;
    private Label? playerHPLabel;
    private Label? playerShieldLabel;
    private Label? enemyHPLabel;
    private Label? enemyShieldLabel;
    private Label? playerPokemonNameLabel;
    private TextView battleLog;
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
            if (playerPokemon == null || enemyPokemon == null)
            {
                Console.WriteLine("Error: All Pokemon are unable to fight.");
                Application.RequestStop(); //This will close the application if no Pokemon can fight
            }
        }
    }
    private void ChoosePokemonDirectly(Player player)
    {
        var pokemons = player.pokemonList.Where(p => p.stats.IsAlive).ToList();
        var names = pokemons.Select(p => p.Name).ToArray();

        Application.Init();
        var dialog = new Dialog("Wybierz pokemona", 60, 20);
        var listView = new ListView(new Rect(0, 0, 50, 14), names);
        listView.OpenSelectedItem += (args) =>
        {
            int index = args.Item;
            if (index >= 0 && index < pokemons.Count)
            {
                playerPokemon = pokemons[index];
                Application.RequestStop();
                InitializeUI();
                UpdateHPBars();
            }
        };
        dialog.Add(listView);

        Application.Run(dialog);
    }


    public BattleWindow(Player aPlayer, Enemy enemy) : base("Battle")
    {
        this.aPlayer = aPlayer;
        this.enemy = enemy;
        //ChoosePokemon(aPlayer);
        ChoosePokemon(enemy);
        ChoosePokemonDirectly(aPlayer);
        //AddAsciiPokemonArt();
        InitializeUI();
        UpdateHPBars();
    }

    private void InitializeUI()
    {
        playerHPBar = new ProgressBar()
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(80),
            Width = Dim.Percent(70),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Green, Color.Black) }
        };

        playerShieldBar = new ProgressBar()
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(80),
            Width = Dim.Percent(35),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Red, Color.Black) }
        };

        enemyHPBar = new ProgressBar()
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(10),
            Width = Dim.Percent(70),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Green, Color.Black) }
        };

        enemyShieldBar = new ProgressBar()
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(10),
            Width = Dim.Percent(35),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Cyan, Color.Black) }
        };
        Add(playerShieldBar, enemyShieldBar);
        Add(playerHPBar, enemyHPBar);

        playerHPLabel = new Label($"HP: {playerPokemon.stats.Hp}/{playerPokemon.stats.maxHp}")
        {
            X = Pos.Left(playerHPBar),
            Y = Pos.Top(playerHPBar) - 1,
        };

        playerShieldLabel = new Label($"Shield: {playerPokemon.stats.shield}/{playerPokemon.stats.maxShield}")
        {
            X = Pos.Left(playerHPBar),
            Y = Pos.Top(playerHPBar) + 1,
        };

        enemyHPLabel = new Label($"HP: {enemyPokemon.stats.Hp}/{enemyPokemon.stats.maxHp}")
        {
            X = Pos.Left(enemyHPBar),
            Y = Pos.Top(enemyHPBar) - 1,
        };

        enemyShieldLabel = new Label($"Shield: {enemyPokemon.stats.shield}/{enemyPokemon.stats.maxShield}")
        {
            X = Pos.Left(enemyHPBar),
            Y = Pos.Top(enemyHPBar) + 1,
        };

        var playerPokemonNameLabel = new Label($"Nazwa: {playerPokemon.Name}")
        {
            X = Pos.Left(playerHPBar),
            Y = Pos.Top(playerHPBar) - 2,
        };

        var enemyPokemonNameLabel = new Label($"Nazwa: {enemyPokemon.Name}")
        {
            X = Pos.Left(enemyHPBar),
            Y = Pos.Top(enemyHPBar) - 2,
        };
        Add(playerPokemonNameLabel, enemyPokemonNameLabel);

        Add(playerHPLabel, enemyHPLabel, enemyShieldLabel,playerShieldLabel);

        var attackButton = new Button("Attack")
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(95)
        };

        var skillsButton = new Button("Skills")
        {
            X = Pos.Right(attackButton) + 2,
            Y = Pos.Percent(95)
        };

        var itemsButton = new Button("Items")
        {
            X = Pos.Right(skillsButton) + 2,
            Y = Pos.Percent(95)
        };

        var fleeButton = new Button("Flee")
        {
            X = Pos.Right(itemsButton) + 2,
            Y = Pos.Percent(95)
        };

        var buttonsTable = new FrameView("Actions")
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(90),
            Width = Dim.Percent(30),
            Height = 5
        };
        buttonsTable.Add(attackButton, skillsButton);
       Add(buttonsTable);

        battleLog = new TextView()
        {
            X = Pos.Right(this) - 44,
            Y = Pos.Bottom(this) - 14,
            Width = 40,
            Height = 10,
            ReadOnly = true,
            Text = "Battle started!\n",
            ColorScheme = new ColorScheme()
            {
                Normal = Terminal.Gui.Attribute.Make(Color.White, Color.DarkGray)
            }
        };
        var logFrame = new FrameView("Event Log")
        {
            X = Pos.Left(battleLog) - 1,
            Y = Pos.Top(battleLog) - 1,
            Width = Dim.Width(battleLog) + 2,
            Height = Dim.Height(battleLog) + 2
        };
        logFrame.Add(battleLog);
        Add(logFrame);

        //Logika przycisków
        ConfigureButtonEvents(attackButton, skillsButton, itemsButton, fleeButton);
    }

    private void ConfigureButtonEvents(Button attackButton, Button skillsButton, Button itemsButton, Button fleeButton)
    {
        fleeButton.Clicked += () =>
        {
            Application.RequestStop();
        };

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

        attackButton.Clicked += () =>
        {

            if (playerPokemon.allSkills[0] is OffensiveSkill)
            {
                OffensiveSkill skillConverted = (OffensiveSkill)playerPokemon.allSkills[0];
                skillConverted.DealDamage(enemyPokemon);
                UpdateHPBars();
                UpdateBattleLog($"{playerPokemon.Name} używa ataku!");
                if (!enemyPokemon.stats.IsAlive)
                {
                    Application.Top.Running = false;
                }
                //CheckForBattleEnd();
            };
            
        };
    }

    private void UpdateHPBars()
    {
        playerHPBar.Fraction = Math.Max(0, playerPokemon.stats.Hp / playerPokemon.stats.maxHp);
        playerShieldBar.Fraction = Math.Max(0, playerPokemon.stats.shield / playerPokemon.stats.maxShield);

        enemyShieldBar.Fraction = Math.Max(0, enemyPokemon.stats.shield / enemyPokemon.stats.maxShield);
        enemyHPBar.Fraction = Math.Max(0, enemyPokemon.stats.Hp / enemyPokemon.stats.maxHp);

        playerHPLabel.Text = $"HP: {playerPokemon.stats.Hp}/{playerPokemon.stats.maxHp}";
        playerShieldLabel.Text = $"Shield: {playerPokemon.stats.shield}/{playerPokemon.stats.maxShield}";

        enemyHPLabel.Text = $"HP: {enemyPokemon.stats.Hp}/{enemyPokemon.stats.maxHp}";
        enemyShieldLabel.Text = $"Shield: {enemyPokemon.stats.shield}/{enemyPokemon.stats.maxShield}";


        //UpdateShield for player
        if (playerPokemon.stats.shield > 0)
        {
            playerShieldBar.Fraction = playerPokemon.stats.shield / playerPokemon.stats.maxShield;
            playerShieldBar.ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Red, Color.Black) };
        }
        else
        {
            playerShieldBar.Fraction = 0; 
            playerShieldBar.ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Black) };
        }
        //UpdateShield for enemy
        if (enemyPokemon.stats.shield > 0)
        {
            enemyShieldBar.Fraction = enemyPokemon.stats.shield / enemyPokemon.stats.maxShield;
            enemyShieldBar.ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Cyan, Color.Black) };
        }
        else
        {
            enemyShieldBar.Fraction = 0; 
            enemyShieldBar.ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Black) };
        }
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
                        //ColorScheme = GetColorSchemeForSkillType(skill.Type)
                    };
                    skillButton.Clicked += () =>
                    {
                        if (skill is OffensiveSkill)
                        {
                            OffensiveSkill skillConverted = (OffensiveSkill)skill;
                            float damageDealt = skillConverted.DealDamage(enemyPokemon);
                            UpdateHPBars();
                            UpdateBattleLog($"{playerPokemon.Name} używa {skillConverted.name}, zadając {damageDealt} obrażeń!");
                            if (!enemyPokemon.stats.IsAlive)
                            {
                                Application.RequestStop();
                                Application.Top.Running = false;
                            }
                        }
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
    public static void BattleStart(Player aPlayer, Enemy aEnemy)
    {
        if (aPlayer == null || aEnemy == null)
        {
            throw new InvalidOperationException("Player or Enemy is not initialized.");
        }
        Application.Init();

        var battleWindow = new BattleWindow(aPlayer,aEnemy);
        Application.Run(battleWindow);

        Application.Shutdown();
    }
    public void EndBattle()
    {
        Application.RequestStop();
    }
    private void UpdateBattleLog(string message)
    {
        if (battleLog != null)
        {
            battleLog.Text += message + "\n";
            battleLog.TopRow = Math.Max(0, battleLog.Lines - battleLog.Bounds.Height);
            battleLog.SetNeedsDisplay();
        }
    }
    private void AddAsciiPokemonArt()
    {
        var playerPokemonArt = new Label(GetAsciiArtForPokemon(playerPokemon))
        {
            X = 2,
            Y = Pos.Bottom(playerHPBar) +6,
            Width = 30,
            Height = 14
        };
        Add(playerPokemonArt);

        var enemyPokemonArt = new Label(GetAsciiArtForPokemon(enemyPokemon))
        {
            X = Pos.Right(enemyHPBar) - 14,
            Y = Pos.Top(enemyHPBar) - 8,
            Width = 30,
            Height = 14
        };
        Add(enemyPokemonArt);
    }
    private string GetAsciiArtForPokemon(Pokemon pokemon)
    {
        //Return ASCII 
        return @"
*%#=..........      .. .   ....  .      . ... 
.#@%-:-:.                        ..:-#@@@:    
..#@=::::=..  ..       .      ..--:::%@%.  ...
.  -*:::::-=..      ..      .:-:::::=@*....   
.   .=-:::::=:....:::::.....-:::::::=... .::-=
.     .--:::::=::::::::::::::::::--..:==:::::=
.       .:--:::::::::::::::::---.--=:::::::::-
.        ..=::::::::::::::::::+-::::::::::::-.
.       ..=::::::::::::::::::::-::::::::::::-.
.       ..-:-@*:+::::::::+:@*:--::::::::::::-.
.       ..+::*##:::::::::=##=:--:::::::+=...  
.       .-***-::::::::::::::-++*::=:..   ..   
.        -****=::::::::::::-****::::=..  ... .
.       ..:#**:::::::::::::-****::::::-..     
.          :==::::::::::::::-=-::-:::=-:.    .
.         .-::::-::::::::--:::=::-=...   ..   
.        .=::::::::::::::::::::+**=.          
.       .-::::::::::::--:::::::-+..      ..  .
.      .*::::::::::+::--:::::::::-.      ..   
.  ..--=:::-:::::::=::-:::::::=::-:......     
.  .:=-:-::-:::::::-::+::::::+::-=--=.        
.   ..===-::=::::::===+:::::-:::-===..        
.     .:+====+-::::*++=::::+=--===...         
.. .. . .-==-----=-...:-:--=+===..   .  .     ";
    }
}
public enum GameMode
{
    Exploration,
    Combat
}
