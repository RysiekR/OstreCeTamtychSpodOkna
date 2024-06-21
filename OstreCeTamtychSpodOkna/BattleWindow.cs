using System.Data;
using Terminal.Gui;

public class BattleProgram
{
    public static void BattleWindowHolder(Player aPlayer, Enemy enemy)
    {

        Application.Init();
        var battleWindow = new BattleWindow(aPlayer, enemy);
        Application.Run(battleWindow);
        Application.Shutdown();
    }
}
public class BattleWindow : Window
{
    private BattleState battleState;
    private EnemyAI enemyAI;
    private Pokemon? playerPokemon;
    private Pokemon? enemyPokemon;
    private Player player;
    private Enemy enemy;
    private ProgressBar? playerHPBar;
    private ProgressBar? playerShieldBar;
    private ProgressBar? enemyHPBar;
    private ProgressBar? enemyShieldBar;
    private Label? playerHPLabel;
    private Label? playerShieldLabel;
    private Label? enemyHPLabel;
    private Label? enemyShieldLabel;
    private TextView battleLog;
    private bool playerTurn = true;
    private Button changeButton;
    private Button skillsButton;
    private Button itemsButton;
    private Button fleeButton;

    public void ChoosePokemon(HasPokemonList entity)
    {
        Random random = new Random();
        var alivePokemons = entity.pokemonList.Where(p => p.stats.IsAlive).ToList();

        if (alivePokemons.Any())
        {
            int index = random.Next(alivePokemons.Count);
            if (entity is Enemy)
            {
                enemyPokemon = alivePokemons[index];
            }
        }
        else
        {
            Console.WriteLine("Error: Wszystkie Pokemony są niezdolne do walki.");
            Application.RequestStop(); //This will close the application if no Pokemon can fight
        }
    }
    private void ChoosePokemonDirectly(Player player)
    {
        var pokemons = player.pokemonList.Where(p => p.stats.IsAlive).ToList();
        var names = pokemons.Select(p => p.Name).ToArray();

        var dialog = new Dialog("Wybierz pokemona", 60, 20);
        var listView = new ListView(new Rect(0, 0, 50, 14), names);
        listView.OpenSelectedItem += (args) =>
        {
            int index = args.Item;
            if (index >= 0 && index < pokemons.Count)
            {
                playerPokemon = pokemons[index];
                InitializeUI();
                UpdateHPBars();
                Application.RequestStop(dialog);
                playerTurn = true;
            }
        };
        dialog.Add(listView);
        Application.Run(dialog);
    }

    public BattleWindow(Player player, Enemy enemy) : base("Battle")
    {
        this.player = player;
        this.enemy = enemy;
        ChoosePokemon(enemy);
        ChoosePokemonDirectly(player);
        battleState = new BattleState(player.Pokemon, enemy.Pokemon);
        enemyAI = new EnemyAI(battleState);

        battleState.OnEnemyTurnStart += () =>
        {
            DisableButtons();
            Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (_) =>
            {
                enemyAI.TakeTurn();
                EndEnemyTurn();
                battleState.NextTurn();
                return false;
            });
        };
        battleState.StartBattle();
        AddAsciiPokemonArt();
        InitializeButtons();
        InitializeUI();
        UpdateHPBars();
    }
    private void EndEnemyTurn()
    {
        if (!enemy.Pokemon.IsAlive)
        {
            EndBattle();
        }
        else
        {
            EnableButtons();
        }
    }

    private void NextTurn()
    {
        if (playerPokemon != null && playerPokemon.stats.Hp == 0)
        {
            if (player.pokemonList.Any(p => p.stats.IsAlive))
            {
                ChoosePokemonDirectly(player);
                EnableButtons();
            }
            else
            {
                battleLog.Text = "Przegrałeś walkę!";
                Application.RequestStop();
            }
        }
        else if (!enemy.Pokemon.IsAlive)
        {
            EndBattle();
        }
        else
        {
            if (playerTurn)
            {
                PlayerTurn();
            }
            else
            {
                Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (_) =>
                {
                    EnemyTurn();
                    return false;
                });
            }
        }
    }
    private void PlayerTurn()
    {
        InitializeButtons();
        EnableButtons();
    }
    private void EnemyTurn()
    {
        var enemySkill = enemyPokemon.ChooseOffensiveSkill();
        if (enemySkill is OffensiveSkill offensiveSkill)
        {
            float damageDealt = offensiveSkill.DealDamage(playerPokemon);
            UpdateHPBars();
            UpdateBattleLog($"{enemyPokemon.Name} używa {enemySkill.name}, zadając {damageDealt} obrażenia!");
            if (!playerPokemon.stats.IsAlive)
            {
                playerTurn = false;
                Application.MainLoop.Invoke(NextTurn);
            }
            else
            {
                EnableButtons();
                playerTurn = true;
                Application.MainLoop.Invoke(NextTurn);
            }
        }
    }
    private void InitializeButtons()
    {
        skillsButton = new Button("Umiejętności")
        {
            X = Pos.Percent(5),
            Y = Pos.Percent(20)
        };

        changeButton = new Button("Zmień Pokemona")
        {
            X = Pos.Percent(5),
            Y = Pos.Percent(90)
        };

        itemsButton = new Button("Przedmioty")
        {
            X = Pos.Percent(60),
            Y = Pos.Percent(20)
        };

        fleeButton = new Button("Ucieczka")
        {
            X = Pos.Percent(60),
            Y = Pos.Percent(90)
        };

        var buttonsTable = new FrameView("Akcje")
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(80),
            Width = Dim.Percent(20),
            Height = 6
        };
        buttonsTable.Add(skillsButton, changeButton, itemsButton, fleeButton);
        ConfigureButtonEvents(changeButton, skillsButton, itemsButton, fleeButton);
        Add(buttonsTable);
    }

    private void InitializeUI()
    {
        var playerFrame = new FrameView("Gracz")
        {
            X = Pos.Percent(5),
            Y = Pos.Percent(5),
            Width = Dim.Percent(40),
            Height = 8
        };
        playerHPBar = new ProgressBar()
        {
            X = Pos.Percent(15),
            Y = Pos.Percent(30),
            Width = Dim.Percent(80),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Green, Color.Black) }
        };

        playerShieldBar = new ProgressBar()
        {
            X = Pos.Left(playerHPBar),
            Y = Pos.Bottom(playerHPBar) + 2,
            Width = Dim.Percent(80),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.BrightCyan, Color.Black) }
        };

        playerHPLabel = new Label($"HP: {playerPokemon.stats.Hp}/{playerPokemon.stats.maxHp}")
        {
            X = Pos.Left(playerHPBar),
            Y = Pos.Top(playerHPBar) - 1,
        };

        playerShieldLabel = new Label($"Shield: {playerPokemon.stats.shield}/{playerPokemon.stats.maxShield}")
        {
            X = Pos.Left(playerShieldBar),
            Y = Pos.Top(playerShieldBar) - 1,
        };

        var playerPokemonNameLabel = new Label($"{playerPokemon.Name}")
        {
            X = Pos.Percent(1),
            Y = Pos.Top(playerHPBar),
        };
        Add(playerFrame);
        playerFrame.Add(playerHPBar, playerShieldBar, playerHPLabel, playerShieldLabel, playerPokemonNameLabel);

        var enemyFrame = new FrameView("Przeciwnik")
        {
            X = Pos.Percent(55),
            Y = Pos.Percent(5),
            Width = Dim.Percent(40),
            Height = 8
        };

        enemyHPBar = new ProgressBar()
        {
            X = Pos.Percent(15),
            Y = Pos.Percent(30),
            Width = Dim.Percent(80),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Green, Color.Black) }
        };

        enemyShieldBar = new ProgressBar()
        {
            X = Pos.Left(enemyHPBar),
            Y = Pos.Bottom(enemyHPBar) + 2,
            Width = Dim.Percent(80),
            Height = 1,
            ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.BrightCyan, Color.Black) }
        };

        enemyHPLabel = new Label($"HP: {enemyPokemon.stats.Hp}/{enemyPokemon.stats.maxHp}")
        {
            X = Pos.Left(enemyHPBar),
            Y = Pos.Top(enemyHPBar) - 1,
        };

        enemyShieldLabel = new Label($"Shield: {enemyPokemon.stats.shield}/{enemyPokemon.stats.maxShield}")
        {
            X = Pos.Left(enemyShieldBar),
            Y = Pos.Top(enemyShieldBar) - 1,
        };

        var enemyPokemonNameLabel = new Label($"{enemyPokemon.Name}")
        {
            X = Pos.Percent(1),
            Y = Pos.Top(enemyHPBar),
        };
        Add(enemyFrame);
        enemyFrame.Add(enemyHPBar, enemyShieldBar, enemyHPLabel, enemyShieldLabel, enemyPokemonNameLabel);

        var battleLogFrame = new FrameView("Battle Log")
        {
            X = Pos.Percent(80),
            Y = Pos.Percent(60),
            Width = 52,
            Height = 20,
            CanFocus = false,

        };

        battleLog = new TextView()
        {
            X = Pos.Percent(5),
            Y = Pos.Percent(10),
            Width = Dim.Fill() - 1,
            Height = Dim.Fill() - 1,
            ReadOnly = true,
            Text = "Battle started!\n",
            CanFocus = false,
            ColorScheme = new ColorScheme()
            {
                Normal = Terminal.Gui.Attribute.Make(Color.White, Color.DarkGray)
            }
        };
        Add(battleLogFrame);
        battleLogFrame.Add(battleLog);
    }

    private void ConfigureButtonEvents(Button changeButton, Button skillsButton, Button itemsButton, Button fleeButton)
    {
        fleeButton.Clicked += () =>
        {
            EndBattle();
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

        changeButton.Clicked += () =>
        {
            ChoosePokemonDirectly(player);

            if (playerPokemon != null)
            {
                UpdateHPBars();
                UpdateBattleLog($"{playerPokemon.Name} jest teraz gotowy do walki!");
            }
        };
    }
    private void DisableButtons()
    {
        changeButton.Enabled = false;
        skillsButton.Enabled = false;
        itemsButton.Enabled = false;
        fleeButton.Enabled = false;
    }
    private void EnableButtons()
    {
        changeButton.Enabled = true;
        skillsButton.Enabled = true;
        itemsButton.Enabled = true;
        fleeButton.Enabled = true;
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
            playerShieldBar.ColorScheme = new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.BrightCyan, Color.Black) };
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
                {
                    SkillCategory skill = playerPokemon.allSkills[skillIndex];
                    var skillButton = new Button(skill.name)
                    {
                        X = j * 20,
                        Y = i * 2,
                    };
                    if (skill is OffensiveSkill offensiveSkill)
                    {
                        skillButton.ColorScheme = GetColorSchemeForSkillType(offensiveSkill.type.ToString());
                    }
                    skillButton.Clicked += () =>
                    {
                        if (skill is OffensiveSkill offensiveSkill)
                        {
                            float damageDealt = offensiveSkill.Use(enemyPokemon);
                            UpdateHPBars();
                            UpdateBattleLog($"{playerPokemon.Name} używa {offensiveSkill.name}, zadając {damageDealt} obrażeń!");
                            if (!enemyPokemon.stats.IsAlive)
                            {
                                Application.RequestStop();
                                Application.Top.Running = false;
                            }
                            else
                            {
                                DisableButtons();
                                Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (_) =>
                                {
                                    EnemyTurn();
                                    return false;
                                });
                            }
                        }

                        skillsDialog.Running = false;
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
            case "Mud":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightYellow, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.White, Color.Black);
                break;
            case "Lava":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.Red, Color.Black);
                break;
            case "Moist":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightBlue, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.Blue, Color.Black);
                break;
        }

        return new ColorScheme()
        {
            Normal = normalAttribute,
            Focus = focusAttribute
        };

    }
    public void EndBattle()
    {
        UpdateBattleLog("Walka zakończona!");
        Application.RequestStop();
    }
    void UpdateBattleLog(string message)
    {
        Application.MainLoop.Invoke(() =>
        {
            if (battleLog != null)
            {
                battleLog.Text += message + "\n";
                battleLog.ScrollTo(battleLog.Lines - 3);
                battleLog.SetNeedsDisplay();
            }
        });
    }
    private void AddAsciiPokemonArt()
    {
        var playerPokemonArt = new Label(GetAsciiArtForPokemon(playerPokemon))
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(25),
            Width = 30,
            Height = 14
        };
        Add(playerPokemonArt);

        var enemyPokemonArt = new Label(GetAsciiArtForPokemon(enemyPokemon))
        {
            X = Pos.Percent(60),
            Y = Pos.Percent(25),
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
