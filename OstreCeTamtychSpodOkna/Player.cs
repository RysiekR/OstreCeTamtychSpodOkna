using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class Player
    {


        //OLD MAIN
        /*        using System.Threading.Tasks;
        Player player = new();

            Task playMusic = new Task(() => { while (true) { musicLibrary.Mario(); } });
            //playMusic.Start();

            //inicializacja wyswietlania mapy i wsadzenie gracza na mape
            Display.Initialize(Display.baseMapBig, player);

        // !!! ACHTUNG ! ACHTUNG !!! endless loop
        //oddanie kontroli nad pozycja gracza w rece gracza

        while (true) 
        {

            player.UpdatePos(Display.obstacleLetters);

        }*/

        string[] jakaMapa = Display.baseMapBig;

        public int row = 3;
        public int col = 5;
        public void UpdatePos(string obstacle)
        {

            int oldRow = row;
            int oldCol = col;
            bool hitObstacle = false;
            char charUp = jakaMapa[row - 1][col];
            char charDown = jakaMapa[row + 1][col];
            char charLeft = jakaMapa[row][col - 1];
            char charRight = jakaMapa[row][col + 1];


            /*      bool isShop = false;
                    bool isGrass = false;
            */
            switch (Console.ReadKey(true).Key)
            {
                /*  case ConsoleKey.W :
                      if(!(jakaMapa[row-1][col] == 'W')) { row--; }
                      else { hitObstacle = true; }
                      break;
                  case ConsoleKey.S:
                      if (!(jakaMapa[row + 1][col] == 'W')) { row++; }
                      else { hitObstacle = true; }     
                      break;
                  case ConsoleKey.A:
                      if (!(jakaMapa[row][col - 1] == 'W')) { col--; }
                      else { hitObstacle = true; }
                      break;
                  case ConsoleKey.D:
                      if (!(jakaMapa[row][col + 1] == 'W')) { col++; }
                      else { hitObstacle = true; }
                      break;*/
                case ConsoleKey.W:
                    if (obstacle.Contains(charUp))
                    {
                        unpassableObstacle(charUp);
                    }
                    else if (charUp == 'P')
                    {
                        changeMapTo(Display.baseMap);
                    }
                    else
                    {
                        row--;
                    }
                    break;
                case ConsoleKey.S:
                    if (obstacle.Contains(charDown))
                    {
                        unpassableObstacle(charDown);
                    }
                    else if (charDown == 'P')
                    {
                        changeMapTo(Display.baseMap);
                    }
                    else
                    {
                        row++;
                    }
                    break;
                case ConsoleKey.A:
                    if (obstacle.Contains(charLeft))
                    {
                        unpassableObstacle(charLeft);
                    }
                    else if (charLeft == 'P')
                    {
                        changeMapTo(Display.baseMap);
                    }
                    else
                    {
                        col--;
                    }
                    break;
                case ConsoleKey.D:
                    if (obstacle.Contains(charRight))
                    {
                        unpassableObstacle(charRight);
                    }
                    else if (charRight == 'P')
                    {
                        changeMapTo(Display.baseMap);
                    }
                    else
                    {
                        col++;
                    }
                    break;
            }

            Console.SetCursorPosition(this.col, this.row);
            Console.Write("#");

            //if wall was not hit remember to clear old position
            if (!hitObstacle)
            {
                Console.SetCursorPosition(oldCol, oldRow);
                Console.Write(" ");
                hitObstacle = false;
            }

            void unpassableObstacle(char charToLogic)
            {
                switch (charToLogic)
                {
                    case 'W':
                        hitObstacle = true;
                        break;
                    case 'T':
                        hitObstacle = true;
                        break;
                }

            }
            void changeMapTo(string[] map)
            {
                jakaMapa = map;
                Console.Clear();
                Display.Initialize(map, this);
            }
        }
    }
}




