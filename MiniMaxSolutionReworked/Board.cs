using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxSolutionReworked
{
    class Board
    {
        public MapPoint[,] board;
        private int searchRange = 2;

        public Board(byte fieldsize)
        {
            board = new MapPoint[fieldsize, fieldsize];
            for (byte i = 0; i < fieldsize; i++)
            {
                for (byte j = 0; j < fieldsize; j++)
                {
                    board[i, j] = new MapPoint(i, j);
                }
            }
        }

        public MapPoint this[byte ind1, byte ind2]
        {
            get
            {
                return board[ind1, ind2];
            }
            set
            {
                board[ind1, ind2] = value;
            }
        }
        public byte[,] ByteBoard()
        {
            int size = board.GetLength(0);
            byte[,] result = new byte[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j].Value == CellValue.EmptyCell) result[i, j] = (byte)0;
                    if (board[i, j].Value == CellValue.OwnCell) result[i, j] = (byte)1;
                    if (board[i, j].Value == CellValue.OppCell) result[i, j] = (byte)2;

                }
            }
            return result;
        }

        public HashSet<byte[]> GetMoves()
        {
            int bLength = board.GetLength(0);
            HashSet<byte[]> pointsList = new HashSet<byte[]>();
            foreach (MapPoint point in board)
            {
                if (point.Value == CellValue.EmptyCell)
                {
                    continue;
                }
                int x0 = Math.Max(0, point.Row - searchRange), x1 = Math.Min(bLength - 1, point.Row + searchRange);
                int y0 = Math.Max(0, point.Column - searchRange), y1 = Math.Min(bLength - 1, point.Column + searchRange);
                for (int i = x0; i <= x1; i++)
                {
                    for (int j = y0; j <= y1; j++)
                    {
                        MapPoint p = board[i, j];
                        if (p.Value == CellValue.EmptyCell)
                        {
                            pointsList.Add(p.GetByteRepresentation());
                        }
                    }
                }

            }
            return pointsList;
        }
    }
}
