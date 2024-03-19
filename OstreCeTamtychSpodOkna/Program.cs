class Program
{
    static void Main()
    {
        var playerPokemon = new Pokemon("Pikachu", 100, 30, 10);
        var enemyPokemon = new Pokemon("Charmander", 100, 20, 5);

        while (playerPokemon.HP > 0 && enemyPokemon.HP > 0)
        {
            DisplayBattleUI(playerPokemon, enemyPokemon);
            Console.WriteLine("Wybierz akcję: \n1. Atak \n2. Leczenie");
            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    playerPokemon.Attack(enemyPokemon);
                    break;
                case "2":
                    playerPokemon.Heal();
                    break;
                default:
                    Console.WriteLine("Nieznana akcja.");
                    continue;
            }

            if (enemyPokemon.HP > 0)
            {
                enemyPokemon.Attack(playerPokemon);
            }

            Console.WriteLine($"HP Pikachu: {playerPokemon.HP}");
            Console.WriteLine($"HP Charmander: {enemyPokemon.HP}");
        }

        if (playerPokemon.HP > 0)
            Console.WriteLine("Wygrałeś walkę!");
        else
            Console.WriteLine("Przegrałeś walkę...");

        static void DisplayBattleUI(Pokemon playerPokemon, Pokemon enemyPokemon)
        {
            Console.Clear(); 
            Console.WriteLine("==== Walka Pokemon ====");
            Console.WriteLine($"{"Gracz:",-15} {playerPokemon.Name,15} {"HP:",-4} {playerPokemon.HP,3} / {playerPokemon.MaxHP,3}");
            Console.WriteLine($"{"Przeciwnik:",-15} {enemyPokemon.Name,15} {"HP:",-4} {enemyPokemon.HP,3} / {enemyPokemon.MaxHP,3}");
            Console.WriteLine("=======================");
        }
        Console.ReadKey();
    }
}

class Pokemon
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int AttackPower { get; set; }
    public int HealPower { get; set; }
    public int MaxHP { get; private set; }

    public Pokemon(string name, int hp, int attackPower, int healPower)
    {
        Name = name;
        HP = hp;
        MaxHP = hp;
        AttackPower = attackPower;
        HealPower = healPower;
    }

    public void Attack(Pokemon other)
    {
        other.HP -= this.AttackPower;
        Console.WriteLine($"{this.Name} zaatakował {other.Name} zadając {this.AttackPower} obrażeń.");
    }

    public void Heal()
    {
        this.HP += this.HealPower;
        Console.WriteLine($"{this.Name} użył leczenia, przywracając {this.HealPower} HP.");
    }
}