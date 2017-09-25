using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField;

namespace WeightedSolution
{
    class wPlayer : IPlayable
    {
        MapPoint[,] board;
        public byte Identificator { get; set; }
        private byte fieldSize;
        private byte maxInRow;
        public string Name { get; set; }

        public wPlayer(byte fieldSize, byte maxInLine)
        {
            this.fieldSize = fieldSize;
            this.maxInRow = maxInLine;
            board = new MapPoint[fieldSize, fieldSize];
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    board[i,j] = new MapPoint(i, j);
                }
            }
            foreach (MapPoint item in board)
            {
                PopulateNeighbours(item);
            }
        }
        public void PopulateNeighbours(MapPoint p)
        {
            if (p.Row != 0 && p.Column != 0)
            {
                p.NeighbourPoints.Add(board[p.Row - 1, p.Column - 1]);
            }
            if (p.Row != fieldSize - 1 && p.Column != fieldSize - 1)
            {
                p.NeighbourPoints.Add(board[p.Row + 1, p.Column + 1]);
            }
            if (p.Row != 0 && p.Column != fieldSize-1)
            {
                p.NeighbourPoints.Add(board[p.Row - 1, p.Column + 1]);
            }
            if (p.Row != fieldSize-1 && p.Column != 0)
            {
                p.NeighbourPoints.Add(board[p.Row + 1, p.Column - 1]);
            }
            if (p.Row != 0)
            {
                p.NeighbourPoints.Add(board[p.Row - 1, p.Column]);
            }
            if (p.Row != fieldSize-1)
            {
                p.NeighbourPoints.Add(board[p.Row + 1, p.Column]);
            }
            if (p.Column != 0)
            {
                p.NeighbourPoints.Add(board[p.Row, p.Column - 1]);
            }
            if (p.Column != fieldSize - 1)
            {
                p.NeighbourPoints.Add(board[p.Row, p.Column - 1]);
            }

        }

        public byte[] NextMove(byte[,] currentState, byte[] OppMove)
        {
            byte[] response = new byte[2];
            if (OppMove[0] == 255) //if first move - return cell in the middle of the board
            {
                response[0] = response[1] = (byte)(fieldSize / 2);
            }
            else
            {
                board[OppMove[0], OppMove[1]].Value = CellValue.OppCell;
            }
            return response;
        }
    }
}
