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
        public RandSolution(byte fieldSize, byte maxInLine)
        {
            this.fieldSize = fieldSize;
            this.maxInRow = maxInLine;
        }
        public byte Identificator { get; set; }
        public string Name { get; set; }

        private byte fieldSize;
        private byte maxInRow;

        public byte[] NextMove(byte[,] currentState, byte[] OppMOve)
        {
            int value = 9;
            byte[] guess = new byte[2];
            while (value != 0)
            {
                guess = pickRandom();
                value = currentState[guess[0], guess[1]];
            }
            Console.WriteLine("Player {0} made turn {1} {2}", Identificator, guess[0], guess[1]);
            return guess;

        }

        private byte[] pickRandom()
        {
            System.Random rnd = new System.Random();
            return new byte[] { (byte)rnd.Next(0, fieldSize), (byte)rnd.Next(0, fieldSize) };
        }
    }
}
