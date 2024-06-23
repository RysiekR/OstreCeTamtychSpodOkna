public class PokemonLevel
{ 
    private int _level;
    private int _exp;

    private Pokemon owner;

    public PokemonLevel(Pokemon ownerOfLevel)
    {
        owner = ownerOfLevel;
        _level = 1;
        _exp = 0;
    }

    public PokemonLevel(Pokemon ownerOfLevel, int level)
    {
        Random random = new Random();
        owner = ownerOfLevel;
        _level = level;
        _exp = random.Next(0,LevelUpFormula());
    }

    public int level { get => _level; }
   
    public int exp
    {
        get => _exp;
        set
        {
            if(value > 0)
            {
                if (_exp + value < LevelUpFormula()) //sprawdzenie czy prog do wbicia lvl nie zostanie przekroczony
                {
                    _exp += value;
                }
                else // tutaj zostal
                {
                    _exp = LevelUpFormula() - (_exp + value); // rozwiazanie przypadku marnowania zdobytego expa ( z 99/100 expa + 20 => 19/nowyProgLvl a nie 0/nowyProg)
                    _level++;
                    owner.LevelUpLogic();
                }
            }
            else { Console.WriteLine("Error PokemonLevel.exp gain < 0 !!!!!!"); }; // error jezeli ktos by chcial odjac expa
        }
    }
    public int LevelUpFormula() // intowy prog expa na level
    {
        return _level * 5; // tutaj bardzo prosty do zmiany
    }
}