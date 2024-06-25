using System;

public struct Stats
{
    Pokemon owner;
    private int vitality;
    private float hp;
    private float strength;
    public float damageModifier { get; private set; }
    public float maxHp { get; private set; }
    private bool isAlive = true;

    private int defense;
    public float shield { get; set; }
    public float maxShield { get; private set; }
    private bool isShielded = true;

    public float armorFromDefense { get; private set; }


    //Add vittality stat and regen method zamienic strenght na vitality i zrobic cos nowego ze strenght

    public Stats(Pokemon ownerPokemon) //trzeba dodac strength i wywogle wszystko zrobic na random
    {
        Random random = new Random();
        int skillPoll = 15;
        
        owner = ownerPokemon;
        vitality = random.Next(1,skillPoll/3*2);
        defense = random.Next(1,skillPoll-vitality-1);
        strength = skillPoll - vitality - defense;
        UpdateStats();
        hp = random.Next((int)(0.5f * maxHp), (int)maxHp);
    }
    public bool IsAlive
    {
        get
        {
            if (hp <= 0) { return false; }
            else { return true; }
        }
    }

    public float Hp
    {
        get => hp;
        set
        {
            if (value < 0) // Jeśli wartość jest ujemna, zadaj obrażenia.
            {
                if (!isShielded)
                {

                    TakeHpDamage(value);//hp += value; // Zadaj obrażenia.
                    if (hp <= 0)
                    {
                        hp = 0;
                        isAlive = false;
                    }
                }
                else
                {
                    HitShield(value);
                }
            }
            else // W przeciwnym razie lecz postać.
            {
                hp += value;
                if (hp > maxHp)
                {
                    hp = maxHp;
                }
            }
        }
    }

    float armorScalar = 100f;
    private void TakeHpDamage(float value)
    {
        float damage = -value;
        float damageAfterArmor;
        damageAfterArmor = damage / (1.0f + (armorFromDefense / armorScalar));
/*        Console.WriteLine("damage b4 armor :");
        Console.WriteLine(damage);
        Console.WriteLine("damage after armor :");
        Console.WriteLine(damageAfterArmor);
*/
        if (damageAfterArmor < 0)
        {
            damageAfterArmor = 0;
        }
        hp -= damageAfterArmor;
    }
    public float GetValueAfterArmors(float value)
    {
        return value / (1.0f + (armorFromDefense / armorScalar));
    }

    public void HitShield(float damage)
    {
        if (damage < 0) // damage jest na minusie => deal dmg
        {
            shield += damage;
            if (shield <= 0)
            {
                shield = 0;
                isShielded = false;
            }
        }
    }
    public void UpdateStats()
    {
        float tempMaxHp = maxHp;
        maxHp = vitality * 10.0f * owner.level.level;
        hp = (hp * maxHp) / tempMaxHp;

        maxShield = (defense * 1.5f + vitality * 1.5f) * 1.2f * owner.level.level;
        shield = maxShield;
        isShielded = true;
        if (shield < 0)
        {
            shield = 0;
        }

        damageModifier = 1 + (strength * 0.1f * owner.level.level);

        armorFromDefense = (defense) * owner.level.level * 0.5f;
    }
    public void LevelUp()
    {
        Random random = new Random();
        int skillPoll = 9;
        int vitalityAdd = random.Next(1, skillPoll / 3 * 2);
        int defenseAdd = random.Next(1, skillPoll - vitalityAdd - 1);
        int strengthAdd = skillPoll - vitalityAdd - defenseAdd;
        vitality += vitalityAdd;
        defense += defenseAdd;
        strength += strengthAdd;
        UpdateStats();
    }
    public void PrintInfo()
    {
        Console.WriteLine($"Vit: {vitality}");
        Console.WriteLine($"Armor: {armorFromDefense} from Def: {defense} which is {100-GetValueAfterArmors(100f)}% dmg reduction");
        Console.WriteLine($"Dmg mod: {damageModifier} from Str: {strength}");
    }
    private void SetShield()
    {
        if (shield > 0)
        {
            isShielded = true;
        }
        else
        {
            isShielded = false;
        }
    }
    private void SetAlive()
    {
        if (hp > 0) { isAlive = true; }
        else { isAlive = false; }
    }
    public void SetShieldAndAlive()
    {
        SetShield();
        SetAlive();
    }

    public void Heal(int value)
    {
        var tempCurrentHp = hp + value;
        if(tempCurrentHp > maxHp)
        {
            hp = maxHp;
        } else
        {
            hp = tempCurrentHp;
        }
    }

    public void RestoreFullHP()
    {
        hp = maxHp;
    }

    public void IncreaseShield(int value)
    {
        var tempCurrentShield = shield + value;

        if (tempCurrentShield > maxShield) {
            shield = maxShield;
        } 
        else {
            shield = tempCurrentShield;
        }
        isShielded = true;
    }

    public void HitWithItem(int value)
    {
        var leftHPHitValue = shield - value; //bijemy w obecną taczę jak dodatnia to całość na tarcze, jak ujemna to jeszcze w HP

        if (leftHPHitValue >= 0)
        {
            shield = leftHPHitValue;
        }
        else
        {
            shield = 0;
            hp += leftHPHitValue; //dodajemy do hp ujemną liczbę czyli zadajemy obrażenia 
        }
    }

}

