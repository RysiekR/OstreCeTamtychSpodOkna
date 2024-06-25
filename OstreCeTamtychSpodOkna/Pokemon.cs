
public class Pokemon
{

    public PokemonLevel level;
    public string Name { get; set; }
    public Type type { get; private set; }
    public Stats stats; //{ get; set; }
    public List<SkillCategory> allSkills = new List<SkillCategory>();
    public Pokemon()
    {
        level = new PokemonLevel(this);
        Random random = new Random();
        type = (Type)random.Next(0, Enum.GetNames(typeof(Type)).Length);
        Name = PokemonNameGenerator.GenerateName(type);
        stats = new Stats(this);
        /*for(int i = 0; i < 10; i++)
        {
            level.exp = 5 * level.level + 1;
        }*/
        FillSkillsUpToPokemonLevel();
    }
    /// <summary>
    /// Takes int and randoms pokemon level base on intbaseLevelToRandomize +- diference in player pokemon lvls
    /// </summary>
    /// <param name="baseLevelToRandomizeFrom"></param>
    public Pokemon(int baseLevelToRandomizeFrom)
    {
        Random random = new Random();
        int difference = PrawieSingleton.GetLevelDifferenceInPlayerPokemons();
        int levelToCreate = Math.Max(1, baseLevelToRandomizeFrom + random.Next(-difference,difference+1));
        level = new(this, levelToCreate);

        type = (Type)random.Next(0, Enum.GetNames(typeof(Type)).Length);
        Name = PokemonNameGenerator.GenerateName(type);
        stats = new Stats(this);

        FillSkillsUpToPokemonLevel();
        for(int i = 0; i < levelToCreate-1; i++)
        {
            LevelUpLogic();
        }
    }
    /// <summary>
    /// Generuje skille. losowo jezeli howMany = 1, co najmniej 1 offensywny jezeli howMany > 1
    /// </summary>
    /// <param name="howMany"></param>
    private void GenerateSkills(int howMany)
    {
        Random random = new Random();
        if (howMany > 1)
        {
            int offSkills = random.Next(1, howMany);
            int healSkills = howMany - offSkills;
            for (int i = 0; i < offSkills; i++)
            {
                allSkills.Add(new OffensiveSkill(this));
            }
            for (int i = 0; i < healSkills; i++)
            {
                allSkills.Add(new HealSkill(this));
            }
        }
        else
        {
            if (random.Next(0, 2) == 0)
            {
                allSkills.Add(new OffensiveSkill(this));
            }
            else
            {
                allSkills.Add(new HealSkill(this));
            }
        }
    }
    public void FillSkillsUpToPokemonLevel()
    {
        int minNumOfSkills = 2;
        int numberOfSkillsToHave = minNumOfSkills + level.level;
        if (numberOfSkillsToHave > allSkills.Count && numberOfSkillsToHave > 0)
        {
            GenerateSkills(numberOfSkillsToHave-allSkills.Count);
        }
    }

    public bool IsAlive => stats.Hp > 0;
    public void ResetStats()
    {
        stats.Hp = stats.maxHp;
        stats.shield = stats.maxShield;
        stats.SetShieldAndAlive();

        foreach (var skill in allSkills)
        {
            skill.ResetUses();
        }
    }
    public OffensiveSkill ChooseOffensiveSkill()
    {
        var offensiveSkills = allSkills.OfType<OffensiveSkill>().ToList();
        return offensiveSkills.Any() ? offensiveSkills[new Random().Next(offensiveSkills.Count)] : null;
    }
    public HealSkill ChooseHealSkill()
    {
        var healSkills = allSkills.OfType<HealSkill>().Where(s => s.CanUse).ToList();
        return healSkills.Any() ? healSkills[new Random().Next(healSkills.Count)] : null;
    }
    public void LevelUpLogic()
    {
        stats.LevelUp();
        FillSkillsUpToPokemonLevel();
        foreach (var skill in allSkills)
        {
            if (skill is OffensiveSkill offensiveSkill)
            {
                offensiveSkill.UpdateSkill();
            }
        }
    }
    public void PokemonInfoPrint()
    {
        Console.WriteLine(Name + ": ");
        Console.WriteLine("Type " + type);
        Console.WriteLine("Hp: " + stats.Hp + "/" + stats.maxHp);
        Console.WriteLine("Shield: " + stats.shield + "/" + stats.maxShield);
        stats.PrintInfo();
        Console.WriteLine("Lv: " + level.level + " Exp: " + level.exp + "/" + level.LevelUpFormula());
    }
    public void AllSkillsInfoPrint()
    {
        int number = 0;
        foreach (var skill in allSkills) 
        {
            number++;
            Console.WriteLine(number);
            skill.SkillInfoPrint(); 
        }
    }
    public int ExpAfterWin()
    {
        if (level.level > 1)
        {
            return ((int)Math.Sqrt(level.level - 1) * 5) / 2;
        }
        else { return 1; }
    }
}
