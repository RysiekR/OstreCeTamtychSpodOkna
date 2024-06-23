public abstract class Item
{
    protected static Random random = new Random();

    public string Name { get; set; }
    protected readonly int minPower;
    protected readonly int maxPower;
    public Item(string name, int minPower, int maxPower)
    {
        Name = name;
        this.minPower = minPower;
        this.maxPower = maxPower;
    }

    public abstract string UseItem(Pokemon playerPokemon, Pokemon enemyPokemon);

}

class Potion(string name, int minPower, int maxPower) : Item(name, minPower, maxPower)
{
    public string UseItemLocally(Pokemon pokemon)
    {
        var value = random.Next(minPower, maxPower);
        pokemon.stats.Heal(value);
        return ($"Pokemon {pokemon.Name} wyleczony o {value}");
    }
    public override string UseItem(Pokemon playerPokemon, Pokemon enemyPokemon)
    {
        return UseItemLocally(playerPokemon);
    }

}

class HyperPotion : Item
{
    public HyperPotion() : base("HyperPotion", 0, 0) { }

    public string UseItemLocally(Pokemon pokemon)
    {
        pokemon.stats.RestoreFullHP();
        return ($"Pokemon {pokemon.Name} wyleczony do max");
    }
    public override string UseItem(Pokemon playerPokemon, Pokemon enemyPokemon)
    {
        return UseItemLocally(playerPokemon);
    }

}

class Shield(string name, int minPower, int maxPower) : Item(name, minPower, maxPower)
{
    public string UseItemLocally(Pokemon pokemon)
    {
        var value = random.Next(minPower, maxPower);
        pokemon.stats.IncreaseShield(value);
        return ($"Pokemon {pokemon.Name} dostał punkty tarczy o {value}");
    }
    public override string UseItem(Pokemon playerPokemon, Pokemon enemyPokemon)
    {
        return UseItemLocally(playerPokemon);
    }

}

class Bomb(string name, int minPower, int maxPower) : Item(name, minPower, maxPower)
{
    public string UseItemLocally(Pokemon enemyPokemon)
    {
        var value = random.Next(minPower, maxPower);
        enemyPokemon.stats.HitWithItem(value);
        return ($"Pokemon przeciwnika {enemyPokemon.Name} dostał {value} punktow obrazen");
    }
    public override string UseItem(Pokemon playerPokemon, Pokemon enemyPokemon)
    {
        return UseItemLocally(enemyPokemon);
    }
}
