using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class Player
    {
        string[] jakaMapa = Display.baseMapBig;
        string obstacle = Display.obstacleLetters;
        string logicLetters = Display.obstacleLettersWithLogic;
        //position (chcialem w vector2 ale on oddaje floaty a nam trzeba inty)
        public int row = 3;
        public int col = 5;

        public void UpdatePos()
        {

            int oldRow = row;
            int oldCol = col;
            bool hitObstacle = false;
            char charUp = jakaMapa[row - 1][col];
            char charDown = jakaMapa[row + 1][col];
            char charLeft = jakaMapa[row][col - 1];
            char charRight = jakaMapa[row][col + 1];
            ConsoleKey consoleKeyPressed = ConsoleKey.None;

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
                    //
                    //
                    //
                    // mozna dodac case i od razu na nim wykonac cos
                    // np jak bedzie npc do odpalic rozmowe walke
                }

            }
            /*void changeMapTo(string[] map)
            {
                jakaMapa = map;
                Console.Clear();
                Display.Initialize(map, this);


            }*/
            void changeMapTo(char letterOfTheMap)
            {
                switch(letterOfTheMap)
                {
                    case 'P': { jakaMapa = Display.baseMap; break; }
                }
                Console.Clear();
                Display.Initialize(jakaMapa, this);

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
                    changeMapTo(charInThisDirection);
                }
                // tutaj jak moze normalnie chodzic to zmienia pozycje row / col
                else
                {
                    switch(consoleKeyPressed)
                    {
                        case ConsoleKey.W: { row--; }
                        break;
                        case ConsoleKey.S:{ row++;}
                        break;
                        case ConsoleKey.D: { col++; }
                        break;
                        case ConsoleKey.A: { col--; }
                        break;
                    }
                        
                   
                }
            }
            //TODO zrobic check czy ten klawisz ma zastosowanie
            //jak nie to jeszcze raz go zczytac
            //i wyswietlic "nie dotykaj klawiszy ktore nic nie robia łobuzie"

            // metoda ktora zczytuje i zwraca konkretny przycisk z klawiatury
            ConsoleKey inputFromPlayer()
            {
                consoleKeyPressed = Console.ReadKey(true).Key;
                return consoleKeyPressed;
            }

        }
    }
}




