public static class PrawieSingleton
{
    public static Player player;
    public static int GetAverageLevelOfPlayerPokemons()
    {
        int averageLevel = 0;
        foreach (Pokemon p in player.pokemonList)
        {
            averageLevel += p.level.level;
        }
        averageLevel /= player.pokemonList.Count + 1;

        return averageLevel;
    }
    public static int GetLevelDifferenceInPlayerPokemons()
    {
        int lowestLevel = 0;
        int highestlevel = 0;
        if (player.pokemonList.Count != 0)
        {
            int[] levelsArray = new int[player.pokemonList.Count];

            for (int i = 0; i < player.pokemonList.Count; i++)
            {
                levelsArray[i] = player.pokemonList[i].level.level;
            }

            lowestLevel = levelsArray.Min();
            highestlevel = levelsArray.Max();
        }
        return Math.Max(0, (highestlevel - lowestLevel));
    }
}
