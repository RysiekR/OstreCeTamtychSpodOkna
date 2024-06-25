using System.Data;
using Terminal.Gui;

public class BattleProgram
{
    public static bool battleOverride;
    public static void BattleWindowHolder(Player aPlayer, Enemy enemy)
    {
        bool battle = false;
        do
        {
            battleOverride = true;
            battle = false;
            bool playerCon = false;
            bool enemyCon = false;
            Application.Init();
            var battleWindow = new BattleWindow(aPlayer, enemy);
            Application.Run(battleWindow);
            Application.Shutdown();
            foreach (Pokemon p in enemy.pokemonList)
            {
                if (p.stats.IsAlive)
                {
                    enemyCon = true;
                }
            }
            foreach (Pokemon p in aPlayer.pokemonList)
            {
                if (p.stats.IsAlive)
                {
                    playerCon = true;
                }
            }
            if (playerCon && enemyCon) battle = true;

            battle = battleOverride && battle;
            /*
                        if (battle)
                        {
                            Application.Init();
                            {
                                var messageDialog = new Dialog("Brak Pokemonów", 60, 7);

                                var surrenderButton = new Button("RUN / Don't Attack")
                                {
                                    X = Pos.Percent(20),
                                    Y = Pos.Center(),
                                };
                                surrenderButton.Clicked += () =>
                                {
                                    Application.RequestStop(messageDialog);

                                    battle = false;
                                    //Environment.Exit(0); //Zakończenie aplikacji
                                };

                                var tryAgainButton = new Button("Fight Continues / Spróbuj ponownie")
                                {
                                    X = Pos.Percent(60),
                                    Y = Pos.Center(),
                                };
                                tryAgainButton.Clicked += () =>
                                {
                                    Application.RequestStop(); //Zamknięcie tylko tego okna dialogowego
                                };
                                messageDialog.AddButton(surrenderButton);
                                messageDialog.AddButton(tryAgainButton);
                                Application.Run(messageDialog);
                                Application.Shutdown();
                            }
                        }*/
        } while (battle);

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
                RemoveOldPokemonAscii();
                AddAsciiPokemonArt();
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
        ColorScheme = new ColorScheme() {Normal = new Terminal.Gui.Attribute(new TrueColor (255, 255, 0).ToConsoleColor()  , new TrueColor(82, 140, 41).ToConsoleColor()) };
        this.player = player;
        this.enemy = enemy;
        ChoosePokemon(enemy);
        ChoosePokemonDirectly(player);
        battleState = new BattleState(playerPokemon, enemyPokemon);
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
        RemoveOldPokemonAscii();
        AddAsciiPokemonArt();
        InitializeButtons();
        InitializeUI();
        UpdateHPBars();
    }
    private void EndEnemyTurn()
    {
        if (!enemyPokemon.IsAlive)
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
        else if (!enemyPokemon.IsAlive)
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
        EnableButtons();
    }
    private void EnemyTurn()
    {
        var enemySkill = enemyPokemon.ChooseOffensiveSkill();
        if (enemySkill is OffensiveSkill offensiveSkill)
        {
            float damageDealt = offensiveSkill.Use(playerPokemon);
            UpdateHPBars();
            if (damageDealt <= 0)
            {
                UpdateBattleLog($"{enemyPokemon.Name} używa {enemySkill.name} i nie trafia!");
            }
            else
            {
                UpdateBattleLog($"{enemyPokemon.Name} używa {enemySkill.name}, zadając {(int)damageDealt} obrażenia!");
            }
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
    public void InitializeButtons()
    {
        var buttonsTable = new FrameView("Akcje")
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(85),
            Width = Dim.Percent(30),
            Height = 6
        };

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

        buttonsTable.Add(skillsButton, changeButton, itemsButton, fleeButton);
        ConfigureButtonEvents(changeButton, skillsButton, itemsButton, fleeButton);
        Add(buttonsTable);
    }

    private void InitializeUI()
    {
        var playerFrame = new FrameView($"Twój pokemon: {playerPokemon.Name}")
        {
            X = Pos.Percent(5),
            Y = Pos.Percent(5),
            Width = Dim.Percent(40),
            Height = 8
        };
        playerHPBar = new ProgressBar()
        {
            X = Pos.Percent(12),
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

        playerHPLabel = new Label($"HP: {(int)playerPokemon.stats.Hp}/{(int)playerPokemon.stats.maxHp}")
        {
            X = Pos.Left(playerHPBar),
            Y = Pos.Top(playerHPBar) - 1,
        };

        playerShieldLabel = new Label($"Shield: {(int)playerPokemon.stats.shield}/{(int)playerPokemon.stats.maxShield}")
        {
            X = Pos.Left(playerShieldBar),
            Y = Pos.Top(playerShieldBar) - 1,
        };

        var playerLevelLabel = new Label($"Level:{playerPokemon.level.level}")
        {
            X = Pos.Percent(1),
            Y = Pos.Top(playerShieldBar),
        };
        var playerTypeLabel = new Label($"Type:{playerPokemon.type}")
        {
            X = Pos.Percent(1),
            Y = Pos.Top(playerHPBar),
        };

        Add(playerFrame);
        playerFrame.Add(playerHPBar, playerShieldBar, playerHPLabel, playerShieldLabel, playerLevelLabel, playerTypeLabel);

        var enemyFrame = new FrameView($"Pokemon przeciwnika: {enemyPokemon.Name}")
        {
            X = Pos.Percent(55),
            Y = Pos.Percent(5),
            Width = Dim.Percent(40),
            Height = 8
        };

        enemyHPBar = new ProgressBar()
        {
            X = Pos.Percent(12),
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

        enemyHPLabel = new Label($"HP: {(int)enemyPokemon.stats.Hp}/{(int)enemyPokemon.stats.maxHp}")
        {
            X = Pos.Left(enemyHPBar),
            Y = Pos.Top(enemyHPBar) - 1,
        };

        enemyShieldLabel = new Label($"Shield: {(int)enemyPokemon.stats.shield}/{(int)enemyPokemon.stats.maxShield}")
        {
            X = Pos.Left(enemyShieldBar),
            Y = Pos.Top(enemyShieldBar) - 1,
        };
        var enemyLevelLabel = new Label($"Level:{enemyPokemon.level.level}")
        {
            X = Pos.Percent(1),
            Y = Pos.Top(enemyShieldBar),
        };
        var enemyTypeLabel = new Label($"Type:{enemyPokemon.type}")
        {
            X = Pos.Percent(1),
            Y = Pos.Top(enemyHPBar),
        };

        Add(enemyFrame);
        enemyFrame.Add(enemyHPBar, enemyShieldBar, enemyHPLabel, enemyShieldLabel, enemyLevelLabel, enemyTypeLabel);

        var battleLogFrame = new FrameView("Battle Log")
        {
            X = Pos.Percent(58),
            Y = Pos.Percent(85),
            Width = 70,
            Height = 8,
            CanFocus = false,
        };

        battleLog = new TextView()
        {
            X = Pos.Percent(2),
            Y = Pos.Percent(1),
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

        itemsButton.Clicked += () =>
        {
            var itemsDialog = new Dialog("Choose an item", 60, 20)
            {
                X = Pos.Center(),
                Y = Pos.Center()
            };

            CreateItemsGrid(itemsDialog);

            Application.Run(itemsDialog);
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

        playerHPLabel.Text = $"HP: {(int)playerPokemon.stats.Hp}/{(int)playerPokemon.stats.maxHp}";
        playerShieldLabel.Text = $"Shield: {(int)playerPokemon.stats.shield}/{(int)playerPokemon.stats.maxShield}";

        enemyHPLabel.Text = $"HP: {(int)enemyPokemon.stats.Hp}/{(int)enemyPokemon.stats.maxHp}";
        enemyShieldLabel.Text = $"Shield: {(int)enemyPokemon.stats.shield}/{(int)enemyPokemon.stats.maxShield}";


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
    private void CreateItemsGrid(Dialog itemsDialog)
    {
        int rows = (player.itemsList.Count + 1) / 2;
        int itemIndex = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemIndex < player.itemsList.Count)
                {
                    Item item = player.itemsList[itemIndex];
                    var itemButton = new Button(item.Name, true)
                    {
                        X = j * 20,
                        Y = i * 2,
                    };
                    itemButton.Clicked += () =>
                    {
                        player.itemsList.Remove(item);
                        UpdateBattleLog(item.UseItem(playerPokemon, enemyPokemon));
                        UpdateHPBars();
                        if (!enemyPokemon.stats.IsAlive)
                        {
                            enemyPokemon.ExpAfterWin();
                            int expGained = enemyPokemon.ExpAfterWin();
                            playerPokemon.level.exp = expGained;
                            Application.RequestStop();
                            Application.Top.Running = false;
                        }
                        else
                        {
                            DisableButtons();
                            Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(0.5f), (_) =>
                            {
                                EnemyTurn();
                                return false;
                            });
                        }
                        itemsDialog.Running = false;
                    };
                    itemsDialog.Add(itemButton);
                    itemIndex++;
                }
            }
        }
        var returnButton = new Button("Return")
        {
            X = Pos.Center(),
            Y = Pos.Percent(90),
        };
        returnButton.Clicked += () => { itemsDialog.Running = false; };
        itemsDialog.Add(returnButton);
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
                    string skillButtonLabel = skill.name + " " + skill.numberOfUses + "/" + skill.maxNumberOfUses;
                    var skillButton = new Button(skillButtonLabel, true)
                    {
                        X = j * 25,
                        Y = i * 2,
                    };
                    if (skill is OffensiveSkill offensiveSkill)
                    {
                        skillButton.ColorScheme = GetColorSchemeForSkillType(offensiveSkill.type.ToString());
                    }
                    else 
                    {
                        var normalAttribute = Terminal.Gui.Attribute.Make(Color.Black, Color.White);
                        var focusAttribute = Terminal.Gui.Attribute.Make(Color.Black, Color.DarkGray);
                        skillButton.ColorScheme = new ColorScheme() { Normal = normalAttribute, Focus = focusAttribute };
                    }
                
                    skillButton.Clicked += () =>
                    {
                        if (skill is OffensiveSkill offensiveSkill)
                        {
                            if (offensiveSkill.numberOfUses > 0)
                            {
                                float damageDealt = offensiveSkill.Use(enemyPokemon);
                                UpdateHPBars();

                                if (damageDealt <= 0)
                                {
                                    UpdateBattleLog($"{playerPokemon.Name} używa {offensiveSkill.name} i nie trafia!");
                                }
                                else
                                {
                                    UpdateBattleLog($"{playerPokemon.Name} używa {offensiveSkill.name}, zadając {(int)damageDealt} obrażeń!");
                                }

                                if (!enemyPokemon.stats.IsAlive)
                                {
                                    enemyPokemon.ExpAfterWin();
                                    int expGained = enemyPokemon.ExpAfterWin();
                                    playerPokemon.level.exp = expGained;
                                    Application.RequestStop();
                                    Application.Top.Running = false;
                                }
                                else
                                {
                                    skillsDialog.Running = false;
                                    DisableButtons();
                                    Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(0.5f), (_) =>
                                    {
                                        EnemyTurn();
                                        return false;
                                    });
                                }
                            }
                            else
                            {
                                SkillUnavailableError();
                            }
                        }
                        else if (skill is HealSkill healSkill)
                        {
                            if (healSkill.numberOfUses > 0)
                            {
                                healSkill.Use(playerPokemon);
                                UpdateHPBars();
                                if (playerPokemon.stats.Hp == playerPokemon.stats.maxHp)
                                {
                                    UpdateBattleLog($"{playerPokemon.Name} używa {healSkill.name} ale ma już pełne życie!");
                                }
                                else
                                {
                                    UpdateBattleLog($"{playerPokemon.Name} używa {healSkill.name}, lecząc {(int)healSkill.HealValue} zdrowia!");
                                }
                                skillsDialog.Running = false;
                                DisableButtons();
                                Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(0.5f), (_) =>
                                {
                                    EnemyTurn();
                                    return false;
                                });
                            }
                            else
                            {
                                SkillUnavailableError();
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
    private void SkillUnavailableError()
    {
        var messageDialog = new Dialog("Ta umiejętność jest obecnie niedostępna.", 60, 7);
        messageDialog.ColorScheme = Colors.Error;
        var label = new Label("Wykorzystałeś już wszystkie jej użycia. \n" + "Odwiedź szpital, żeby odświeżyć użycia.")
        {
            X = Pos.Center(),
            Y = Pos.Percent(30),
        };
        messageDialog.Add(label);
        var tryAgainButton = new Button("Wybierz inną umiejętność!")
        {
            X = Pos.Percent(60),
            Y = Pos.Center(),
        };
        tryAgainButton.Clicked += () =>
        {
            Application.RequestStop(); //Zamknięcie tylko tego okna dialogowego
        };

        messageDialog.AddButton(tryAgainButton);
        Application.Run(messageDialog);
    }
    private ColorScheme GetColorSchemeForSkillType(string type)
    {
        var normalAttribute = Terminal.Gui.Attribute.Make(Color.White, Color.Black);
        var focusAttribute = Terminal.Gui.Attribute.Make(Color.White, Color.Black);

        switch (type)
        {
            case "Mud":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightYellow, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.BrightYellow, Color.DarkGray);
                break;
            case "Lava":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.DarkGray);
                break;
            case "Moist":
                normalAttribute = Terminal.Gui.Attribute.Make(Color.BrightBlue, Color.Black);
                focusAttribute = Terminal.Gui.Attribute.Make(Color.BrightBlue, Color.DarkGray);
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
        BattleProgram.battleOverride = false;
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
        var playerPokemonArt = new Label(PokemonAscii.GetPokemonAscii())
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(23),
            Width = 30,
            Height = 14
        };
        Add(playerPokemonArt);
        var enemyPokemonArt = new Label(PokemonAscii.GetPokemonAscii())
        {
            X = Pos.Percent(60),
            Y = Pos.Percent(25),
            Width = 30,
            Height = 14
        };  
        Add(enemyPokemonArt);
    }
    private void RemoveOldPokemonAscii()
    {
        var playerPokemonRemoveArt = new Label(@"
                                                    
                                                        
                                                    
                                                        
                                                        
                                                        
                                                            
                                                            
                                                            
                                                            
                                                        
                                                            
                                                        
                                                        
                                                        
                                                        
                                                        
                                                        
                                                            
                                                        
                                                            
                                                        
                                                    
                                                      
                                                    ")
        {
            X = Pos.Percent(10),
            Y = Pos.Percent(23),
            Width = 30,
            Height = 14
        };
        Add(playerPokemonRemoveArt);
        var enemyPokemonRemoveArt = new Label(@"
                                                    
                                                        
                                                    
                                                        
                                                        
                                                        
                                                            
                                                            
                                                            
                                                            
                                                        
                                                            
                                                        
                                                        
                                                        
                                                        
                                                        
                                                        
                                                            
                                                        
                                                            
                                                        
                                                    
                                                      
                                                    ")
        {
            X = Pos.Percent(60),
            Y = Pos.Percent(25),
            Width = 30,
            Height = 14
        };
        Add(enemyPokemonRemoveArt);
    }
}
public enum GameMode
{
    Exploration,
    Combat
}
