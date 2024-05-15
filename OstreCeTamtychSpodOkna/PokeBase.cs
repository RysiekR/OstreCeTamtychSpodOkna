using System;

public class PokeBase
{
	private List<Item> itemList;
	private List<Pokemon> pokemonList;
	public PokeBase()
	{
	}

	public void getItemFromBase(List<Item> playerInventory, Item item)
	{
		if(playerInventory.Count >= 20)
		{
			Console.WriteLine("Inventory is full");
			return;
		}

		if(playerInventory.Contains(item))
		{
			Item foundItem = playerInventory.First(inventoryItem => inventoryItem.Name == item.Name);

			if(foundItem != null)
			{
                foundItem.Quantity += 1;
                playerInventory.Add(foundItem);
			} else
			{
                playerInventory.Add(item);

            }
        }
	}
}
