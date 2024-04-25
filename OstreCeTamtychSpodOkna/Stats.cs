public struct Stats
{
    Pokemon owner;
    private int strenght;
    private float hp;
    public float maxHp { get; private set; }
    private bool isAlive = true;

    private int defense;
    public float shield { get; private set; }
    public float maxShield { get; private set; }
    private bool isShielded = true;

    public float armorFromDefense { get; private set; }


    //Add vittality stat and regen method zamienic strenght na vitality i zrobic cos nowego ze strenght

    public Stats(int str, int def, Pokemon ownerPokemon)
    {
        owner = ownerPokemon;
        strenght = str;
        defense = def;
        UpdateStats();
        hp = 0.5f * maxHp;
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

    private void TakeHpDamage(float value)
    {
        float damage = -value;
        float damageAfterArmor;
        damageAfterArmor = damage / (1.0f + (armorFromDefense / 10.0f));
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

    public void HitShield(float damage)
    {
        if (damage < 0) // damage jest na minusie => deal dmg
        {
            shield += damage;
            if (shield <= 0)
            {
                isShielded = false;
            }
        }
    }
    public void UpdateStats()
    {
        float tempMaxHp = maxHp;
        maxHp = strenght * 2.0f * owner.level.level;
        hp = (hp * maxHp) / tempMaxHp;

        maxShield = (defense * 1.5f + strenght * 1.5f) * 1.2f * owner.level.level;
        shield = maxShield;
        isShielded = true;

        armorFromDefense = (defense) * owner.level.level * 0.5f;
    }
    public void LevelUp()
    {
        Console.WriteLine("LevelUp");
        strenght += 5;
        defense += 5;
    }

}

