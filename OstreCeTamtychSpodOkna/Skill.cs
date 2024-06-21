public interface SkillCategory
{
    public int numberOfUses { get; }
    public int maxNumberOfUses { get; }
    public Category category { get; }
    public string name { get; }
    bool CanUse { get; }
    float Use(Pokemon target);
    public void ResetUses();
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
    Moist // albo Aqua
}

public class OffensiveSkill : SkillCategory
{
    public Category category { get; private set; } = Category.Offensive;
    public Type type { get; private set; }
    private int initialDamageValue;
    public string name { get; private set; }
    public float damage { get; private set; }
    public int accuracy { get; private set; }
    private Pokemon owner;
    public bool CanUse { get; private set; } = true;
    public int numberOfUses { get; private set; }
    public int maxNumberOfUses { get; private set; }

    public OffensiveSkill(Pokemon ownerA)
    {
        Random random = new Random();
        owner = ownerA;
        type = (Type)random.Next(0, Enum.GetNames(typeof(Type)).Length);
        name = SkillNameGenerator.GenerateName(type, Category.Offensive);
        initialDamageValue = random.Next(10, 20);

        accuracy = random.Next(0, 100);
        UpdateSkill();
        maxNumberOfUses = random.Next(1, 10);
        ResetUses();
    }
    public float Use(Pokemon target)
    {
        if (CanUse)
        {
            numberOfUses--;
            if (numberOfUses <= 0)
            {
                CanUse = false;
            }
            return DealDamage(target);
        }
        else return 0;
    }
    public void ResetUses()
    {
        numberOfUses = maxNumberOfUses;
        CanUse = true;
    }

    public void UpdateSkill()
    {
        damage = owner.stats.damageModifier * initialDamageValue * 1.0f;  //to do level 
    }
    public float DealDamage(Pokemon pokemonToHit)
    {
        var stats = pokemonToHit.stats;
        stats.Hp -= damage;
        if (stats.Hp < 0) stats.Hp = 0;
        pokemonToHit.stats = stats;
        Console.Beep();
        return damage;
    }
}
public class HealSkill : SkillCategory
{
    public Category category { get; private set; } = Category.Heal;
    public string name { get; private set; }
    public float HealValue { get; private set; }
    private Pokemon owner;
    public bool CanUse { get; private set; } = true;
    public int numberOfUses { get; private set; }
    public int maxNumberOfUses { get; private set; }
    public HealSkill(Pokemon ownerA)
    {
        owner = ownerA;
        name = SkillNameGenerator.GenerateName(ownerA.type, Category.Defensive);
        Random random = new Random();
        HealValue = random.Next(0, 40 * owner.level.level);
        maxNumberOfUses = random.Next(1, 3);
        ResetUses();
    }
    /// <summary>Zawsze zwraca 0.</summary>
    public float Use(Pokemon target)
    {
        if (CanUse)
        {
            Heal(target);
            numberOfUses--;
            if (numberOfUses <= 0)
            {

                CanUse = false;
            }
        }
        return 0;
    }
    public void ResetUses()
    {
        numberOfUses = maxNumberOfUses;
        CanUse = true;
    }
    public void Heal(Pokemon target)
    {
        if (target.IsAlive)
        {
            target.stats.Hp += HealValue;
            if (target.stats.Hp > target.stats.maxHp)
            {
                target.stats.Hp = target.stats.maxHp;
            }
        }
    }
}
