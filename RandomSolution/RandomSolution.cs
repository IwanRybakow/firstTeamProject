using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField;

namespace RandomSolution
{
    public class RandSolution : IPlayable
    {
        public RandSolution(int fieldSize, int maxInLine)
        {
            this.fieldSize = fieldSize;
            this.maxInRow = maxInLine;
        }
        public int Identificator { get; set; }
        private int fieldSize;
        private int maxInRow;

        public int[] NextMove(int[,] currentState)
        {
            int value = 9;
            int[] guess = new int[2];
            while (value != 0)
            {
                guess = pickRandom();
                value = currentState[guess[0], guess[1]];
            }
            Console.WriteLine("Player {0} made turn {1} {2}", Identificator, guess[0], guess[1]);
            return guess;

        }

        private int[] pickRandom()
        {
            System.Random rnd = new System.Random();
            return new int[] { rnd.Next(0, fieldSize), rnd.Next(0, fieldSize) };
        }
    }
}
