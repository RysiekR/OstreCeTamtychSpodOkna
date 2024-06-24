public abstract class Item
{
    public readonly static List<Item> availableItems = [
        ItemFactory.CreateSmallPotion(),
        ItemFactory.CreateMediumPotion(),
        ItemFactory.CreateHyperPotion(),
        ItemFactory.CreateSmallShield(),
        ItemFactory.CreateBigShield(),
        ItemFactory.CreateSmallBomb(),
        ItemFactory.CreateBigBomb()
      ];
   
    protected static Random random = new Random();

    public string Name { get; set; }
    public readonly int minPower;
    public readonly int maxPower;
    public readonly int Price;

    public Item(string name, int minPower, int maxPower, int price)
    {
        Name = name;
        this.minPower = minPower;
        this.maxPower = maxPower;
        Price = price;
    }

    public abstract string UseItem(Pokemon playerPokemon, Pokemon enemyPokemon);

}

class Potion(string name, int minPower, int maxPower, int price) : Item(name, minPower, maxPower, price)
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
    public HyperPotion() : base("HyperPotion", 0, 0, 500) { }

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

class Shield(string name, int minPower, int maxPower, int price) : Item(name, minPower, maxPower, price)
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

class Bomb(string name, int minPower, int maxPower, int price) : Item(name, minPower, maxPower, price)
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
