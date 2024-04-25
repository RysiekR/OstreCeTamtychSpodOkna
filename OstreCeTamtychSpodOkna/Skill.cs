using System.Buffers;

public interface SkillCategory
{
    public Category category { get; }
}

public enum Category
{
    Offensive,
    Defensive,
    Heal,
    Buff,
    Debuff
}

public enum Type
{
    Lava,
    Mud,
    Ice//Placeholder
}

public class OffensiveSkill : SkillCategory
{
   public Category category { get; private set; } =  Category.Offensive;
   public Type type { get; private set; } 
   
   public string name { get; private set;} 
   public float damage { get; private set;}
   public int accuracy { get; private set;}
   private Pokemon owner;
   public OffensiveSkill(string skillName, Pokemon ownerA)
   {
    Random random = new Random();
    name = skillName;
    owner = ownerA;
    type = owner.type;
    damage = random.Next(0,20*owner.level.level);    //to do level 
    accuracy = random.Next(0,100); 
   }
    public class HealSkill : SkillCategory
    {
        public Category category { get; private set; } = Category.Heal;
        public string name { get; private set;}
        public float healValue;
        private Pokemon owner;
        public bool canUse = true;
        public HealSkill(string skillName, Pokemon ownerA)
        {
            name = skillName;
            owner = ownerA;
            Random random = new Random();
            healValue = random.Next(0,40*owner.level.level);
        }
    }
}