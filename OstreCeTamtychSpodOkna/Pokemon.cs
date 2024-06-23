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
        stats = new Stats(5,5,this);
        /*
                allSkills.Add(new OffensiveSkill("Fireball",Type.Lava, this));
                allSkills.Add(new OffensiveSkill("Fireball2", Type.Lava, this));
                allSkills.Add(new OffensiveSkill("Fireball3",Type.Lava, this));
                allSkills.Add(new OffensiveSkill("Fireball4",Type.Lava, this));
                allSkills.Add(new OffensiveSkill("Not Fireball",Type.Lava, this));
        */
        GenerateSkills(5);
    }

    private void GenerateSkills(int howMany)
    {
        Random random = new Random();
        int offSkills = random.Next(1, howMany);
        int healSkills = howMany-offSkills;
        for (int i = 0; i < offSkills; i++)
        {
            allSkills.Add(new OffensiveSkill(this));
        }
        for (int i = 0;i < healSkills; i++)
        {
            allSkills.Add(new HealSkill(this));
        }
    }

    public bool IsAlive => stats.Hp > 0;
    public void ResetStats()
    {
        stats.Hp = stats.maxHp;
        stats.shield = stats.maxShield;
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
    /*    public void GainExperience(int experiencePoints)
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
    */
    public void LevelUpLogic()
    {
        //TOmfDO stats.levelup()
        Console.WriteLine("No Level up logic implemented !!!!!!!!!!!!!");
    }
    public void PokemonInfoPrint()
    {
        Console.WriteLine(Name + ": ");
        Console.WriteLine("Type" + type);
        Console.WriteLine("Lv: " + level.level +" Exp: "+level.exp+"/"+level.LevelUpFormula());
    }
    public void AllSkillsInfoPrint()
    {
        foreach (var skill in allSkills) { skill.SkillInfoPrint(); }
    }
}
