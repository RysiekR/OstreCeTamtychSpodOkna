  public class Pokemon
{

    public string Name { get; set; }
    public Type type { get; private set; }
    public int Level { get; private set; }
    public int HP { get; set; }
    public int MaxHP { get; private set; }
    public List<SkillCategory> allSkills = new List<SkillCategory>(); 
 
    public Pokemon(string name, int hp)
    {
        Random random = new Random();
        Name = name;
        type = (Type)random.Next(0, Enum.GetNames(typeof(Type)).Length);
        Level = 1;
        HP = hp;
        MaxHP = hp;
        allSkills.Add(new OffensiveSkill("Fireball",this));
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