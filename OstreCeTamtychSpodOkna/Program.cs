class Program
{
    static void Main()
    {
        var playerPokemon = new Pokemon("Pikachu", 100, 30, 10);
        var enemyPokemon = new Pokemon("Charmander", 100, 20, 5);

        BattleUI.DisplayBattleUI(playerPokemon, enemyPokemon);

        Console.WriteLine("Wybierz akcję: \n1. Atak \n2. Leczenie");

        while (playerPokemon.HP > 0 && enemyPokemon.HP > 0)
        {
            string? userInput = Console.ReadLine();
            BattleUI.ClearLastLine();

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
            BattleUI.UpdateHPDisplay(playerPokemon, enemyPokemon);
        }

        if (playerPokemon.HP > 0)
            Console.WriteLine("Wygrałeś walkę!");
        else
            Console.WriteLine("Przegrałeś walkę...");


        Console.ReadKey();
    }
}
class BattleUI
{
    public static void DisplayBattleUI(Pokemon playerPokemon, Pokemon enemyPokemon)
    {
        Console.WriteLine("==== Walka Pokemon ====");
        Console.WriteLine($"{"Gracz:",-15} {playerPokemon.Name,15} {"HP:",-4} {playerPokemon.HP,3} / {playerPokemon.MaxHP,3}");
        Console.WriteLine($"{"Przeciwnik:",-15} {enemyPokemon.Name,15} {"HP:",-4} {enemyPokemon.HP,3} / {enemyPokemon.MaxHP,3}");
        Console.WriteLine("=======================");
    }
    public static void UpdateHPDisplay(Pokemon playerPokemon, Pokemon enemyPokemon)
    {
        int cursorTop = Console.CursorTop;
        int cursorLeft = Console.CursorLeft;
        // stawienie kursora na pozycji HP gracza
        Console.SetCursorPosition(37, 1); //Zaktualizować jak coś się zmieni
        Console.Write("                  ");
        Console.SetCursorPosition(37, 1);
        Console.Write($"{playerPokemon.HP} / {playerPokemon.MaxHP}");

        //Ustawienie kursora na pozycji HP przeciwnika
        Console.SetCursorPosition(37, 2); //Zaktualizować jak coś się zmieni
        Console.Write("                   ");
        Console.SetCursorPosition(37, 2);
        Console.Write($"{enemyPokemon.HP} / {enemyPokemon.MaxHP}");
        Console.WriteLine();
        Console.WriteLine();
        
        Console.SetCursorPosition(cursorLeft, cursorTop);
        // Console.Write(new string(' ', Console.WindowWidth)); 
    }
    public static void ClearLastLine()
{
    //Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.BufferWidth));
    Console.SetCursorPosition(0, Console.CursorTop - 1);
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
        //Console.WriteLine($"{this.Name} zaatakował {other.Name} zadając {this.AttackPower} obrażeń.");
    }
    public void Heal()
    {
        this.HP += this.HealPower;
        Console.WriteLine($"{this.Name} użył leczenia, przywracając {this.HealPower} HP.");
    }
}