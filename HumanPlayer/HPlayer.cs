using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField;

namespace HumanPlayer
{
    public class HPlayer : IPlayable
    {
        public byte Identificator { get; set; }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public HPlayer(byte fieldSize, byte maxInLine, byte Id)
        {
            Identificator = Id;
        }
        public byte[] NextMove(byte[,] currentState, byte maxInRow)
        {
            Console.WriteLine("Please enter coordinates of your next move ");
            string move = Console.ReadLine();
            string[] coordinates = move.Split(' ');

            byte r = Convert.ToByte(coordinates[0]);
            byte c = Convert.ToByte(coordinates[1]);
            return new byte[] { --r, --c };
        }
    }
}
