using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField;
using MultiThreadedMiniMaxReworked;

namespace MiniMaxSolutionReworked
{
    public class MiniMaxPlayer : IPlayable
    {
        Board board;
        private byte fieldSize;
        private byte maxInRow;
        private MultiThreadedAlgorithm Solver;
        private int depth;
        private bool isGameStarted;
        public byte Identificator { get; set; }


        public MiniMaxPlayer(int depth)
        {
            this.depth = depth;
        }

        public byte[] NextMove(byte[,] currentState, byte maxInRow)
        {
            
            if (!isGameStarted) //initialization if first move
            {
                bool isFirstMove = true;
                this.fieldSize = (byte)currentState.GetLength(0);
                this.maxInRow = maxInRow;
                board = new Board(fieldSize);
                for (byte i = 0; i < fieldSize; i++)
                {
                    for (byte j = 0; j < fieldSize; j++)
                    {
                        if (currentState[i,j]!=0)
                        {
                            board[i, j].Value = CellValue.OppCell;
                            isFirstMove = false;
                        }
                    }
                }
                Solver = new MultiThreadedAlgorithm(maxInRow);
                isGameStarted = true;
                if (isFirstMove) //if first move - return cell in the middle of the board
                {
                    board[(byte)(fieldSize / 2), (byte)(fieldSize / 2)].Value = CellValue.OwnCell;
                    return new byte[] { (byte)(fieldSize / 2), (byte)(fieldSize / 2) };
                }
            }
            for (byte i = 0; i < fieldSize; i++)
            {
                for (byte j = 0; j < fieldSize; j++)
                {
                    if (currentState[i, j] != 0 && board[i, j].Value == CellValue.EmptyCell)
                    {
                        board[i, j].Value = CellValue.OppCell;
                    }
                }
            }
            HashSet<byte[]> moves = board.GetMoves();
            byte[] result = Solver.GetOptimalStep(board.ByteBoard(), moves, depth);
            Console.WriteLine("Cell {0} {1}", result[0], result[1]);
            board[result[0], result[1]].Value = CellValue.OwnCell;
            return result;            
        }
    }
}

