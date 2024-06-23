using Terminal.Gui;

namespace OstreCeTamtychSpodOkna.Equipment
{
    public class ShopWindow : Window
    {
        private Player player;

        public ShopWindow(Player player) {
            this.player = player;
        }


        public void CreateShopGrid(Dialog shopDialog)
        {
            var infoLabel = new Label()
            {
                X = Pos.Center(),
                Y = Pos.Percent(80),
                Height = 1,
                Visible = false,
            };

            var label = new Label("Cash: " + player.money)
            {
                X = Pos.Center(),
                Y = Pos.Center(),
                Height = 1

            };

            shopDialog.Add(label);
            shopDialog.Add(infoLabel);

            int rows = (Item.availableItems.Count + 1) / 2;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int itemIndex = i * 2 + j;
                    if (itemIndex < Item.availableItems.Count)
                    {
                        Item item = Item.availableItems[itemIndex];
                        var itemButton = new Button(item.Name +": " + item.Price.ToString())
                        {
                            X = j * 20,
                            Y = i * 2,
                        };
                        itemButton.Clicked += () =>
                        {

                            if (player.money >= item.Price) {
                                player.itemsList.Add(item);
                                player.money -= item.Price;
                                label.Text = "Cash: " + player.money;
                                infoLabel.Text = "Kupiłeś: " + item.Name;
                                infoLabel.Visible = true;
                            } else
                            {
                                infoLabel.Text = "Nie stac cie";
                            }

                        };
                        shopDialog.Add(itemButton);
                    }
                }
            }
            var returnButton = new Button("Return")
            {
                X = Pos.Center(),
                Y = Pos.Percent(90),
            };
            returnButton.Clicked += () => { shopDialog.Running = false; };
            shopDialog.Add(returnButton);
        }
    }
}
