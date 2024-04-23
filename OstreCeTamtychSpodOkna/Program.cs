using Microsoft.VisualBasic;
using Terminal.Gui;
using System.Data;
using OstreCeTamtychSpodOkna;
using System.Media;

public class Program
{
    public static void Main()
    {
        Application.Init();

        Pokemon playerPokemon = new Pokemon("Pikachu", "Electric", 100, 30, 10);
        Pokemon enemyPokemon = new Pokemon("Charmander", "Fire", 100, 20, 5);
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

        var attackButton = new Button ("Attack!")
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
        attackButton.Clicked += () =>
        {
            gameState.PlayerPokemon.Attack(gameState.EnemyPokemon);
            UpdateHPBars(); //Aktualizacja HP po ataku
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
                    var skillButton = new Button (skill.Name);
                    skillButton.X = j * 20; // Odstęp między kolumnami
                    skillButton.Y = i * 2; // Odstęp między wierszami
                    skillsDialog.Add(skillButton);
                }
            }
        }
    }
}
public class Pokemon
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Level { get; private set; }
    public int HP { get; set; }
    public int AttackPower { get; set; }
    public int HealPower { get; set; }
    public int MaxHP { get; private set; }

    public Pokemon(string name, string type, int hp, int attackPower, int healPower)
    {
        Name = name;
        Type = type;
        Level = 1;
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
    public void GainExperience(int experiencePoints)
    {

        int experienceThreshold = 100 * this.Level; //Wartość potrzebna do level up'a

        // double experienceMultiplier = 1.6 / (1 + Math.Exp(opponent.Level - this.Level));
        // int adjustedExperiencePoints = (int)(experiencePoints * experienceMultiplier) + 1;

        if (experiencePoints >= experienceThreshold)
        {
            this.Level++;
            Console.WriteLine($"{this.Name} zdobył {experiencePoints} punktów doświadczenia i awansował na poziom {this.Level}!");

            //Zwiększanie statystyk po level up'ie
            this.MaxHP += 10;
            this.AttackPower += 2;
            this.HealPower += 1;

            //Warunek ewolucji
            if (this.Level >= 10)
            {
                this.Name += "*";
                Console.WriteLine($"{this.Name} ewoluował!");
            }
        }
        else
        {
            Console.WriteLine($"{this.Name} zdobył {experiencePoints} punktów doświadczenia.");
        }
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