public static class PokemonNameGenerator
{
    private static readonly Random random = new Random();
    private static readonly string[] lavaElements = { "Magma", "Volcano", "Eruption", "Coal", "Pyro", "Ember", "Flameboyant", "Lavabubble", "Sizzlestick", "Infernope" };
    private static readonly string[] moistElements = { "Dew", "Mist", "Puddle", "Spray", "Droplet", "Ripple", "Splashsquatch", "Moistache", "Dampoodle", "Fogzilla" };
    private static readonly string[] mudElements = { "Swamp", "Quagmire", "Silt", "Clay", "Peat", "Loam", "Mudbud", "Sludgehog", "Dirturtle", "Quicksandwich" };
    private static readonly string[] neutralElements = { "Mon", "Ster", "Ion", "Beast", "Critter", "Sprite", "Fuzzle", "Woozle", "Bloop", "Thingamajig" };

    /*    public enum PokemonType
        {
            Lava,
            Moist,
            Mud
        }
    */
    public static string GenerateName(Type type)
    {
        string[] typeElements = type switch
        {
            Type.Lava => lavaElements,
            Type.Moist => moistElements,
            Type.Mud => mudElements,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Not expected Pokémon type value: {type}")
        };

        return typeElements[random.Next(typeElements.Length)] + neutralElements[random.Next(neutralElements.Length)];
    }
}

public static class SkillNameGenerator
{
    private static readonly Random random = new Random();

    // Types:
    private static readonly string[] lavaElements = { "hot", "flaming", "sizzling", "blazing", "fiery", "scorching", "molten", "incendiary", "combustible", "inferno" };
    private static readonly string[] moistElements = { "wet", "moist", "damp", "soggy", "humid", "drippy", "saturated", "soaked", "drenched", "squishy" };
    private static readonly string[] mudElements = { "dirty", "nasty", "mucky", "grimy", "sludgy", "soiled", "filthy", "sullied", "tarnished", "stained" };

    // Categories:
    private static readonly string[] attackElements = { "noodle", "touch", "tickle", "slap", "poke", "jab", "whack", "bop", "smack", "thump" };
    private static readonly string[] defenseElements = { "thought", "ponder", "contemplate", "reflect", "meditate", "muse", "brood", "cogitate", "ruminate", "speculate" };
    private static readonly string[] specialElements = { "something, something...", "thinking", "pondering", "wondering", "questioning", "contemplating", "musing", "daydreaming", "fantasizing", "imagining" };

    public static string GenerateName(Type type, Category category)
    {
        string element = "";
        string name = "";

        switch (type)
        {
            case Type.Lava:
                element = lavaElements[random.Next(lavaElements.Length)];
                break;
            case Type.Moist:
                element = moistElements[random.Next(moistElements.Length)];
                break;
            case Type.Mud:
                element = mudElements[random.Next(mudElements.Length)];
                break;
        }

        switch (category)
        {
            case Category.Offensive:
                name = attackElements[random.Next(attackElements.Length)];
                break;
            case Category.Defensive:
                name = defenseElements[random.Next(defenseElements.Length)];
                break;
            default:
                name = specialElements[random.Next(specialElements.Length)];
                break;
        }

        return $"{element} {name}";
    }
}
