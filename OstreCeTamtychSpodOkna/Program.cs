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
        if(gameState is object)
        {
        Application.Run(new BattleWindow(gameState));
        }
        Application.Shutdown();
    }

    public class BattleWindow : Window
    {
        public BattleWindow(GameState gameState) : base("Battle")
        {
            {
                var attackButton = new Button()
                {
                    Text = "Attack!",
                    X = 10,
                    Y = 10,
                    IsDefault = true,
                };
                attackButton.Clicked += () =>
           {
               gameState.PlayerPokemon.Attack(gameState.EnemyPokemon);

               if (OperatingSystem.IsWindows())
               {
                   Console.Beep(1000, 1800);
               }
           };
                var skillsButton = new Button()
                {
                    Text = "Skills",
                    X = 20,
                    Y = 20,
                    IsDefault = true,
                };
                skillsButton.Clicked += () =>
                            {
                                var skillsDialog = new Dialog("Choose a skill", 60, 20)
                                {
                                    X = Pos.Center(),
                                    Y = Pos.Center()
                                };
                                int column1XOffset = -30;
                                int column2XOffset = 2;
                                int startY = 1;
                                int spacingY = 2;

                                bool useFirstColumn = true; //Zmienna do przełączania między kolumnami

                                foreach (Skill element in gameState.SkillList)
                                {
                                    var skillButton = new Button(element.Name)
                                    {
                                        X = Pos.Left(skillsDialog) + (useFirstColumn ? column1XOffset : column2XOffset),
                                        Y = Pos.Top(skillsDialog) + startY,
                                        IsDefault = false
                                    };
                                    skillButton.Clicked += () =>
                                    {
                                        //Logika przycisku skilli
                                    };
                                    skillsDialog.Add(skillButton);

                                    if (useFirstColumn)
                                    {
                                        useFirstColumn = false;
                                    }
                                    else
                                    {
                                        useFirstColumn = true;
                                        startY += spacingY;
                                    }
                                }
                                var differentButton = new Button("Different")
                                {
                                    X = 6,
                                    Y = 6,
                                    IsDefault = true,
                                };
                                skillsDialog.AddButton(differentButton);

                                differentButton.Clicked += () =>
                                {
                                    Application.RequestStop();
                                };
                                Application.Run(skillsDialog);
                            };
                Add(attackButton, skillsButton);
            }
        }
    }
}

public class Pokemon
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int AttackPower { get; set; }
    public int HealPower { get; set; }
    public int MaxHP { get; private set; }//

    public Pokemon(string name, int hp, int attackPower, int healPower)
    {
        Name = name;
        HP = hp;
        MaxHP = hp;
        AttackPower = attackPower;
        HealPower = healPower;
    }

    public void Attack(Pokemon other)
    {
        other.HP -= this.AttackPower;
        //Console.WriteLine($"{this.Name} zaatakował {other.Name} zadając {this.AttackPower} obrażeń.");
    }
    public void Heal()
    {
        this.HP += this.HealPower;
        Console.WriteLine($"{this.Name} użył leczenia, przywracając {this.HealPower} HP.");
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