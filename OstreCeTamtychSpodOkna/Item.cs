using System;

public class Item
{
	private string name { get; set; }
	private ItemType type { get; set; }
    private int quantity { get; set; }
    public Item()
	{
	}

	public void use()
	{
		quantity--;
	}

    public void buy()
    {
        quantity++;
    }
}

public enum ItemType
{
	Pokeball, //normal, great, ultra itp
	PokemonUsage, //potion, berry
	PlayerUsage, //Do użytku przez gracza typu repel, escape robe
	Story //Kluczowy, fabularny
}
