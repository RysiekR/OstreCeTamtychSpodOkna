using System;

namespace Equipment
{
    public abstract class Item
    {
        private string Name { get; set; }
        protected readonly int minPower;
        protected readonly int maxPower;

        protected Item(string name, int minPower, int maxPower)
        {
            Name = name;
            this.minPower = minPower;
            this.maxPower = maxPower;
        }

        public abstract void UseItem(Pokemon pokemon);
 
    }

    class SmallPotion : Item
    {
        public SmallPotion() : base("SmallPotion",10, 20) { }

        public override void UseItem(Pokemon pokemon)
        {
            Random r = new Random();
            Console.WriteLine("Pokemon {0} wyleczony o {1}", pokemon.Name, r.Next(minPower, maxPower));
        }

    }

    class MediumPotion : Item
    {
        public MediumPotion() : base("MediumPotion", 20, 40) { }

        public override void UseItem(Pokemon pokemon)
        {
            Random r = new Random();
            Console.WriteLine("Pokemon {0} wyleczony o {1}", pokemon.Name, r.Next(minPower, maxPower));
        }

    }

    class HyperPotion : Item
    {
        public HyperPotion() : base("HyperPotion", 0, 0) { }

        public override void UseItem(Pokemon pokemon)
        {
            Random r = new Random();
            Console.WriteLine("Pokemon {0} wyleczony do maxa", pokemon.Name);
        }

    }

    class SmallShield : Item
    {
        public SmallShield() : base("SmallShield", 5, 10) { }

        public override void UseItem(Pokemon pokemon)
        {
            Random r = new Random();
            Console.WriteLine("Pokemon {0} dostał punkty tarczy o {1}", pokemon.Name, r.Next(minPower, maxPower));
        }

    }

    class BigShield : Item
    {
        public BigShield() : base("BigShield", 10, 20) { }

        public override void UseItem(Pokemon pokemon)
        {
            Random r = new Random();
            Console.WriteLine("Pokemon {0} dostał punkty tarczy o {1}", pokemon.Name, r.Next(minPower, maxPower));
        }

    }

    class SmallBomb : Item
    {
        public SmallBomb() : base("SmallBomb", 5, 10) { }

        public override void UseItem(Pokemon enemyPokemon)
        {
            Random r = new Random();
            Console.WriteLine("Pokemon przeciwnika {0} dostał {1} punktow obrazen", enemyPokemon.Name, r.Next(minPower, maxPower));
        }

    }

    class BigBomb : Item
    {
        public BigBomb() : base("BigBomb", 10, 20) { }

        public override void UseItem(Pokemon enemyPokemon)
        {
            Random r = new Random();
            Console.WriteLine("Pokemon przeciwnika {0} dostał {1} punktow obrazen", enemyPokemon.Name, r.Next(minPower, maxPower));
        }

    }

}


