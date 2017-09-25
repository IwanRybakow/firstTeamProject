using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField;
using MultiThreadedMiniMax;

namespace MiniMaxSolution
{
    public class MiniMaxPlayer : IPlayable
    {
        Board board;
        public byte Identificator { get; set; }
        private byte fieldSize;
        private byte maxInRow;
        public string Name { get; set; }
        private MultiThreadedAlgorithm Solver;

        public MiniMaxPlayer(byte fieldSize, byte maxInLine, byte Id)
        {
            this.fieldSize = fieldSize;
            this.maxInRow = maxInLine;
            Identificator = Id;
            board = new Board(fieldSize);
            Solver = new MultiThreadedAlgorithm(maxInLine);
        }

        public byte[] NextMove(byte[,] currentState, byte[] OppMove)
        {
            if (OppMove[0] == 255) //if first move - return cell in the middle of the board
            {
                board[(byte)(fieldSize / 2), (byte)(fieldSize / 2)].Value = CellValue.OwnCell;
                return new byte[] { (byte)(fieldSize / 2), (byte)(fieldSize / 2) };
            }
            else
            {
                board[OppMove[0], OppMove[1]].Value = CellValue.OppCell;
                byte[] result = Solver.GetOptimalStep(board.ByteBoard());
                board[result[0], result[1]].Value = CellValue.OwnCell;
                return result;
            }
        }
    }
}

