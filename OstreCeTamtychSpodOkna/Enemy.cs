﻿namespace OstreCeTamtychSpodOkna
{
    //enemy class with movement logic, in the future will be holding pokemons 
    internal class Enemy
    {
        private int row;
        private int col;

        public Enemy(int rowSpawn, int colSpawn)
        {
            this.row = rowSpawn;
            this.col = colSpawn;
        }
        private int oldCol;
        private int oldRow;
        public List<string> jakaMapa;
        Random rnd = new Random();
        private bool hitObstacle = false;
        string enemyAvatar = "5";
        int randomMove;

        public void UpdatePos(Map currentMap)
        {
            oldCol = this.col;
            oldRow = this.row;

            //TODO jak bedzie uzyte z GSRogue to juz chyba nie trzeba tu przypisywac
            jakaMapa = currentMap.mapAsList;
            char charUp = jakaMapa[row - 1][col];
            char charDown = jakaMapa[row + 1][col];
            char charLeft = jakaMapa[row][col - 1];
            char charRight = jakaMapa[row][col + 1];
            randomMove = rnd.Next(1, 5);
            switch (randomMove)
            {
                case 1:
                    Movement(charUp);
                    break;
                case 2:
                    Movement(charDown);
                    break;
                case 3:
                    Movement(charLeft);
                    break;
                case 4:
                    Movement(charRight);
                    break;
            }


            //if wall was not hit remember to clear old position
            if (!hitObstacle)
            {
            jakaMapa[row] = jakaMapa[row].Insert(col, enemyAvatar);
            jakaMapa[row] = jakaMapa[row].Remove(col + 1, 1);
                jakaMapa[oldRow] = jakaMapa[oldRow].Insert(oldCol, " ");
                jakaMapa[oldRow] = jakaMapa[oldRow].Remove(oldCol + 1, 1);
            }
            else
            {
                jakaMapa[row] = jakaMapa[row].Insert(col, enemyAvatar);
                jakaMapa[row] = jakaMapa[row].Remove(col + 1, 1);

            }
            hitObstacle = false;

            //zapisanie zmodyfikowanej mapy
            currentMap.mapAsList = this.jakaMapa;
        }

        // i sprawdza co tam jest i robi co trzeba(mam nadzieje)
        void Movement(char charInThisDirection)
        {
            //sprawdzenie czy char gdzie idziemy jest charem ze string z przeszkodami nie do przejscia
            if (Sprites.obstacle.Contains(charInThisDirection) || charInThisDirection == 'P' || charInThisDirection == '#')
            {
                unpassableObstacle(charInThisDirection);
            }
            // tutaj jak moze normalnie chodzic to zmienia pozycje row / col
            else
            {
                switch (randomMove)
                {
                    case 1:
                        { row--; }
                        break;
                    case 2:
                        { row++; }
                        break;
                    case 3:
                        { col--; }
                        break;
                    case 4:
                        { col++; }
                        break;
                }
            }
        }

        void unpassableObstacle(char charToLogic)
        {
            if (Sprites.obstacle.Contains(charToLogic))
            {
                hitObstacle = true;
            }

            else { Console.WriteLine("Error ! ! ! Enemy.UpdatePos.UnpassableObstacle"); }

        }



    }
}
