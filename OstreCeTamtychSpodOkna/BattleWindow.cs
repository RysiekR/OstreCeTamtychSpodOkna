using Terminal.Gui;
public partial class Program
{
    public class BattleWindow : Window
    {
        public BattleWindow(GameState gameState) : base("Battle")
        {
            {
                var attackButton = new Button()
                {
                    Text = "Attack!",
                    X = 10,
                    Y = 10,
                    IsDefault = true,
                };
                attackButton.Clicked += () =>
           {
               gameState.PlayerPokemon.Attack(gameState.EnemyPokemon);

               if (OperatingSystem.IsWindows())
               {
                   Console.Beep(1000, 1800);
               }
           };
                var skillsButton = new Button()
                {
                    Text = "Skills",
                    X = 20,
                    Y = 20,
                    IsDefault = true,
                };
                skillsButton.Clicked += () =>
                            {
                                var skillsDialog = new Dialog("Choose a skill", 60, 20)
                                {
                                    X = Pos.Center(),
                                    Y = Pos.Center()
                                };
                                int column1XOffset = -30;
                                int column2XOffset = 2;
                                int startY = 1;
                                int spacingY = 2;

                                bool useFirstColumn = true; //Zmienna do przełączania między kolumnami

                                foreach (Skill element in gameState.SkillList)
                                {
                                    var skillButton = new Button(element.Name)
                                    {
                                        X = Pos.Left(skillsDialog) + (useFirstColumn ? column1XOffset : column2XOffset),
                                        Y = Pos.Top(skillsDialog) + startY,
                                        IsDefault = false
                                    };
                                    skillButton.Clicked += () =>
                                    {
                                        //Logika przycisku skilli
                                    };
                                    skillsDialog.Add(skillButton);

                                    if (useFirstColumn)
                                    {
                                        useFirstColumn = false;
                                    }
                                    else
                                    {
                                        useFirstColumn = true;
                                        startY += spacingY;
                                    }
                                }
                                var differentButton = new Button("Different")
                                {
                                    X = 6,
                                    Y = 6,
                                    IsDefault = true,
                                };
                                skillsDialog.AddButton(differentButton);

                                differentButton.Clicked += () =>
                                {
                                    Application.RequestStop();
                                };
                                Application.Run(skillsDialog);
                            };
                Add(attackButton, skillsButton);
            }
        }
    }
}
