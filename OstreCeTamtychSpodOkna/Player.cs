﻿namespace OstreCeTamtychSpodOkna
{
    internal class Player
    {
        public int row = 7;
        public int col = 15;
        string obstacle = Sprites.obstacle;
        string logicLetters = "P123456789";

        //TODO uzyc mapy z GSRogue
        public List<string> jakaMapa = new List<string>();
        private bool hitObstacle = false;
        public int oldRow;
        public int oldCol;
        ConsoleKey consoleKeyPressed;// = ConsoleKey.None;
        public void UpdatePos(Map currentMap)
        {
            oldCol = col;
            oldRow = row;

            //TODO jak bedzie uzyte z GSRogue to juz chyba nie trzeba tu przypisywac
            jakaMapa.Clear();
            jakaMapa.AddRange(currentMap.mapAsList);
            char charUp = jakaMapa[row - 1][col];
            char charDown = jakaMapa[row + 1][col];
            char charLeft = jakaMapa[row][col - 1];
            char charRight = jakaMapa[row][col + 1];

            switch (inputFromPlayer())
            {
                case ConsoleKey.W:
                    Movement(charUp);
                    break;
                case ConsoleKey.S:
                    Movement(charDown);
                    break;
                case ConsoleKey.A:
                    Movement(charLeft);
                    break;
                case ConsoleKey.D:
                    Movement(charRight);
                    break;
            }


            //if wall was not hit remember to clear old position
            if (!hitObstacle)
            {
                jakaMapa[row] = jakaMapa[row].Insert(col, "#");
                jakaMapa[row] = jakaMapa[row].Remove(col + 1, 1);
                jakaMapa[oldRow] = jakaMapa[oldRow].Insert(oldCol, " ");
                jakaMapa[oldRow] = jakaMapa[oldRow].Remove(oldCol + 1, 1);
            }
            hitObstacle = false;

            //zapisanie zmodyfikowanej mapy
            currentMap.mapAsList.Clear();
            currentMap.mapAsList.AddRange(jakaMapa);


        }

        // bierze i sprawdza pole w kierunku w ktorym chcemy sie poruszyc
        // i sprawdza co tam jest i robi co trzeba(mam nadzieje)
        void Movement(char charInThisDirection)
        {
            //sprawdzenie czy char gdzie idziemy jest charem ze string z przeszkodami nie do przejscia
            if (obstacle.Contains(charInThisDirection))
            {
                unpassableObstacle(charInThisDirection);
            }
            //tu jak jest cos do zrobienia a nie tylko "E!!E!! nie ma przejscia
            //TODO wymyslic zrobic zeby bylo jak na gorze tylko ze stringiem liter na ktorych wykonujemy logike
            else if (logicLetters.Contains(charInThisDirection))
            {
                if (charInThisDirection == 'P')
                {
                    changeMapTo(charInThisDirection);
                }
                else if (charInThisDirection == '5')
                {
                    Console.Beep(500, 400);
                }
            }
            // tutaj jak moze normalnie chodzic to zmienia pozycje row / col
            else
            {
                switch (consoleKeyPressed)
                {
                    case ConsoleKey.W:
                        { row--; }
                        break;
                    case ConsoleKey.S:
                        { row++; }
                        break;
                    case ConsoleKey.D:
                        { col++; }
                        break;
                    case ConsoleKey.A:
                        { col--; }
                        break;
                }


            }
        }

        void unpassableObstacle(char charToLogic)
        {
            if (obstacle.Contains(charToLogic))
            {
                hitObstacle = true;
            }

            else { Console.WriteLine("Error Player.UpdatePos.UnpassableObstacle"); }

        }

        void changeMapTo(char letterOfTheMap)
        {
            //TODO tu jest jeszcze do dopracowania , trzeba uzywac gamestatu

            Map tempMap = new Map(Sprites.map2);
            switch (letterOfTheMap)
            {
                case 'P':
                    {
                        jakaMapa.Clear();
                        jakaMapa.AddRange(tempMap.mapAsList);
                        break;
                    }
            }
        }



        //TODO zrobic check czy ten klawisz ma zastosowanie
        //jak nie ma to jeszcze raz go zczytac
        //i wyswietlic "nie dotykaj klawiszy ktore nic nie robia łobuzie"
        // metoda ktora zczytuje i zwraca konkretny przycisk z klawiatury
        ConsoleKey inputFromPlayer()
        {
            consoleKeyPressed = Console.ReadKey(true).Key;
            return consoleKeyPressed;
        }


        //TODO zapisywanie mapy do GSRogue
        public void SetMap()
        {

        }
    }
}




