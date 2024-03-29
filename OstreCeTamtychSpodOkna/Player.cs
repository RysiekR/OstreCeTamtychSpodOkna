﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCeTamtychSpodOkna
{
    internal class Player
    {
        string obstacle = "╬┼╦╩╣╠";
        string logicLetters = "P";
        public int row = 7;
        public int col = 15;
        List<string> jakaMapa = new List<string>();
        public void UpdatePos(List<string>mapaDoWczytania)
        {
            jakaMapa = mapaDoWczytania;
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
                if (obstacle.Contains(charToLogic))
                {
                    hitObstacle = true;
                }

                else { Console.WriteLine("Error Player.UpdatePos.UnpassableObstacle"); }
                //TODO albo zmienic tego switcha na ifa albo sie dowiedziec jak to zrobic inaczej czy w tym case moze byc funkcja ktora cos zwraca?
                /*switch (charToLogic)
                {
                    case '╬':
                        hitObstacle = true;
                        break;
                    case '╦':
                        hitObstacle = true;
                        break;
                    case '┼':
                        hitObstacle = true;
                        break;
                    case '╣':
                        hitObstacle = true;
                        break;
                    case '╩':
                        hitObstacle = true;
                        break;
                    case 'T':
                        hitObstacle = true;
                        break;
                    //
                }*/

            }
            
            void changeMapTo(char letterOfTheMap)
            {
                //TODO tu jest jeszcze do dopracowania
                List<string> newMap = new List<string>();
                Sprites.CreateFinallMap(newMap, Sprites.map2);

                switch(letterOfTheMap)
                {
                    case 'P': { jakaMapa = newMap; break; }
                }
                Console.Clear();
                //Display.Initialize(Display.baseMap, this);
                Sprites.Initialize(jakaMapa, this);
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
            //jak nie ma to jeszcze raz go zczytac
            //i wyswietlic "nie dotykaj klawiszy ktore nic nie robia łobuzie"

            // metoda ktora zczytuje i zwraca konkretny przycisk z klawiatury
            ConsoleKey inputFromPlayer()
            {
                consoleKeyPressed = Console.ReadKey(true).Key;
                return consoleKeyPressed;
            }

        }

        public void SetMap()
        {

        }
    }
}




