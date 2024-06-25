public interface SkillCategory
{
    public int numberOfUses { get; }
    public int maxNumberOfUses { get; }
    public Category category { get; }
    public string name { get; }
    bool CanUse { get; }
    float Use(Pokemon target);
    public void ResetUses();
    public void SkillInfoPrint();
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
    Moist, // albo Aqua
    Mud,
    Lava,
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
    Random random = new Random();

    public OffensiveSkill(Pokemon ownerA)
    {
        owner = ownerA;
        type = (Type)random.Next(0, Enum.GetNames(typeof(Type)).Length);
        name = SkillNameGenerator.GenerateName(type, Category.Offensive);
        initialDamageValue = random.Next(10, 20);

        accuracy = random.Next(75, 101);
        UpdateSkill();
        maxNumberOfUses = random.Next(1, 10);
        ResetUses();
    }
    /// <summary>
    /// when canUse: returns damage if hit succesful if not then returns 0
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public float Use(Pokemon target)
    {
        if (CanUse)
        {
            numberOfUses--;
            if (random.Next(0, 101) < accuracy)
            {
                if (numberOfUses <= 0)
                {
                    CanUse = false;
                }
                return DealDamage(target);
            }
            else return 0;
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

        float damageMultiplayer = Calculations.GiveMeMutiplayer(pokemonToHit,type);

        var stats = pokemonToHit.stats;
        stats.Hp = -damage * damageMultiplayer;
        if (stats.Hp < 0) stats.Hp = 0;
        pokemonToHit.stats = stats;
        return pokemonToHit.stats.GetValueAfterArmors(damage);
    }

    public void SkillInfoPrint()
    {
        Console.WriteLine(category + " skill: " + name + ":");
        Console.WriteLine("Type: " + type);
        Console.WriteLine("Damage: " + damage);
        Console.WriteLine("Accuracy: " + accuracy);
        Console.WriteLine("Num of uses: " + numberOfUses + "/" + maxNumberOfUses);
        Console.WriteLine();
    }
}
public class HealSkill : SkillCategory
{
    public Category category { get; private set; } = Category.Heal;
    public Type type { get; private set; }
    public string name { get; private set; }
    public float HealValue { get; private set; }
    public float baseHealValue {  get; private set; }
    private Pokemon owner;
    public bool CanUse { get; private set; } = true;
    public int numberOfUses { get; private set; }
    public int maxNumberOfUses { get; private set; }
    Random random = new Random();
    public HealSkill(Pokemon ownerA)
    {
        owner = ownerA;
        type = (Type)random.Next(0, Enum.GetNames(typeof(Type)).Length);
        name = SkillNameGenerator.GenerateName(ownerA.type, Category.Defensive);
        baseHealValue = random.Next(0, 40);
        HealValue = baseHealValue * owner.level.level;
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
        if (target.type == this.type)
        {
            HealValue = baseHealValue * owner.level.level * 2;
        }
        else HealValue = baseHealValue * owner.level.level;

        if (target.IsAlive)
        {
            target.stats.Hp += HealValue;
            if (target.stats.Hp > target.stats.maxHp)
            {
                target.stats.Hp = target.stats.maxHp;
            }
        }
    }
    public void SkillInfoPrint()
    {
        Console.WriteLine(category + " skill: " + name + ":");
        Console.WriteLine("Heal ammount: " + HealValue);
        Console.WriteLine("Num of uses: " + numberOfUses + "/" + maxNumberOfUses);
        Console.WriteLine();
    }

}

public static class Calculations
{
    /// <summary>
    /// Is First type wining with second
    /// </summary>
    /// <param name="firstType"></param>
    /// <param name="secondType"></param>
    /// <returns></returns>
    private static bool IsWinning(Type firstType, Type secondType)
    {
        Type[] types = (Type[])Enum.GetValues(typeof(Type));
        int secondIndex = Array.IndexOf(types, secondType);
        int winningIndex = (secondIndex + 1) % types.Length; // index that wins with second type
        Type winningType = types[winningIndex];

        return firstType == winningType;
    }
    /// <summary>
    /// returns 2, 1, or 0.5 depending if spellType is wining with target pokemon type
    /// </summary>
    /// <param name="target"></param>
    /// <param name="spellType"></param>
    /// <returns></returns>
    public static float GiveMeMutiplayer(Pokemon target, Type spellType)
    {
        Type targetPokemonType = target.type;

        if (IsWinning(spellType, targetPokemonType))
        {
            //Wygrana
            return 2f;
        }
        else if (spellType == targetPokemonType)
        {
            //Remis
            return 1f;
        }
        else
        {
            //Przegrana
            return 0.5f;
        }
    }
}