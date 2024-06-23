public abstract class ItemFactory
{
    public static Item CreateSmallPotion()
    {
        return new Potion("SmallPotion", 10, 20, 100);
    }

    public static Item CreateMediumPotion()
    {
        return new Potion("MediumPotion", 20, 40, 200);
    }

    public static Item CreateHyperPotion()
    {
        return new HyperPotion();
    }

    public static Item CreateSmallShield()
    {
        return new Shield("SmallShield", 5, 10, 150);
    }

    public static Item CreateBigShield()
    {
        return new Shield("BigShield", 10, 20, 300);
    }

    public static Item CreateSmallBomb()
    {
        return new Bomb("SmallBomb", 5, 10, 200);
    }

    public static Item CreateBigBomb()
    {
        return new Bomb("BigBomb", 10, 20, 400);
    }
}
