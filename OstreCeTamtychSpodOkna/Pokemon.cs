namespace OstreCeTamtychSpodOkna
{
    internal class Pokemon
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int AttackPower { get; set; }
        public int HealPower { get; set; }
        public int MaxHP { get; private set; }//

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
}
