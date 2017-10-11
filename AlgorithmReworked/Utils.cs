using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmReworked
{
    public static class Utils
    {
        /*search for possible moves near cells which are already taken*/

        public static HashSet<byte[]> FindMoves (byte[,] board)
        {
            int searchRange = 2;
            int bLength = board.GetLength(0);
            HashSet<byte[]> pointsList = new HashSet<byte[]>();

            for (int i = 0; i < bLength; i++)
            {
                for (int j = 0; j < bLength; j++)
                {
                    if (board[i,j] == 0)
                    {
                        continue;
                    }
                    int x0 = Math.Max(0, i - searchRange), x1 = Math.Min(bLength - 1, i + searchRange);
                    int y0 = Math.Max(0, j - searchRange), y1 = Math.Min(bLength - 1, j + searchRange);
                    for (int k = x0; k <= x1; k++)
                    {
                        for (int l = y0; l <= y1; l++)
                        {
                            if (board[k,l] == 0)
                            {
                                pointsList.Add(new byte[] { (byte)k, (byte)l });
                            }
                        }
                    }
                }
            }
            return pointsList;
        }

        /*search for strings near move
         used for move estimation*/

        public static List<string> FindStrings(byte[,] board, byte[] move, byte signsInRowToWin)
        {
            List<string> resultList = new List<string>();

            StringBuilder horizontalLine = new StringBuilder();
            int startH = move[1] - (signsInRowToWin - 1) < 0 ? 0 : move[1] - (signsInRowToWin - 1);
            int finishH = move[1] + signsInRowToWin > board.GetLength(1) ? board.GetLength(1) : move[1] + signsInRowToWin;
            for (int i = startH; i < finishH; i++)
            {
                horizontalLine.Append(board[move[0], i].ToString());
            }
            resultList.Add(horizontalLine.ToString());

            StringBuilder verticalLine = new StringBuilder();
            int startV = move[0] - signsInRowToWin < 0 ? 0 : move[0] - signsInRowToWin;
            int finishV = move[0] + signsInRowToWin > board.GetLength(1) ? board.GetLength(1) : move[0] + signsInRowToWin;
            for (int i = startV; i < finishV; i++)
            {
                verticalLine.Append(board[i, move[1]].ToString());
            }
            resultList.Add(verticalLine.ToString());


            StringBuilder ForwardDiagonal = new StringBuilder();
            int startFwdRow = move[0] + (signsInRowToWin - 1);
            int startFwdColumn = move[1] - (signsInRowToWin - 1);
            while (startFwdRow > board.GetLength(1) - 1 || startFwdColumn < 0)
            {
                startFwdRow--;
                startFwdColumn++;
            }
            int finishFwdRow = move[0] - (signsInRowToWin - 1);
            int finishFwdColumn = move[1] + signsInRowToWin;
            while (finishFwdColumn > board.GetLength(1) || finishFwdRow < 0)
            {
                finishFwdRow++;
                finishFwdColumn--;
            }
            for (int i = startFwdColumn; i < finishFwdColumn; i++)
            {
                ForwardDiagonal.Append(board[startFwdRow, i].ToString());
                startFwdRow--;
            }
            if (ForwardDiagonal.Length >= signsInRowToWin)
            {
                resultList.Add(ForwardDiagonal.ToString());
            }


            StringBuilder BackwardDiagonal = new StringBuilder();
            int startBwdRow = move[0] - (signsInRowToWin - 1);
            int startBwdColumn = move[1] - (signsInRowToWin - 1);
            while (startBwdRow < 0 || startBwdColumn < 0)
            {
                startBwdRow++;
                startBwdColumn++;
            }
            int finisBwdRow = move[0] + signsInRowToWin;
            int finishBwdColumn = move[1] + signsInRowToWin;
            while (finishBwdColumn > board.GetLength(1) || finisBwdRow > board.GetLength(1))
            {
                finisBwdRow--;
                finishBwdColumn--;
            }
            for (int i = startBwdColumn; i < finishBwdColumn; i++)
            {
                BackwardDiagonal.Append(board[startBwdRow, i].ToString());
                startBwdRow++;
            }
            if (BackwardDiagonal.Length >= signsInRowToWin)
            {
                resultList.Add(BackwardDiagonal.ToString());
            }

            return resultList;
        }

        /*search for strings on the whole board
         used for board estimation*/

        public static List<string> FindStrings(byte[,] board, byte signsToWin)
        {
            int boardSize = board.GetLength(0);
            List<string> resultList = new List<string>();

            for (int i = 0; i < boardSize; i++)
            {
                StringBuilder horizontalLine = new StringBuilder();
                StringBuilder verticalLine = new StringBuilder();

                for (int j = 0; j < boardSize; j++)
                {
                    horizontalLine.Append(board[i, j].ToString());
                    verticalLine.Append(board[j, i].ToString());
                }
                resultList.Add(horizontalLine.ToString());
                resultList.Add(verticalLine.ToString());
            }

            int x = signsToWin - 1;
            int x1 = boardSize - signsToWin;
            int y = 0;
            while (y < boardSize - signsToWin + 1)
            {
                StringBuilder forwardDiagonal = new StringBuilder();
                StringBuilder backwardDiagonal = new StringBuilder();
                int tempX = x;
                int tempX1 = x1;
                int tempY = y;
                while (tempX > 0 && tempY > 0 && tempX < boardSize && tempY < boardSize)
                {
                    forwardDiagonal.Append(board[tempX, tempY].ToString());
                    backwardDiagonal.Append(board[tempX1, tempY].ToString());
                    tempX--;
                    tempX1++;
                    tempY++;
                }
                resultList.Add(forwardDiagonal.ToString());
                resultList.Add(backwardDiagonal.ToString());
                if (x == boardSize - 1)
                {
                    y++;
                }
                else
                {
                    x++;
                    x1--;
                }
                
            }
            return resultList;
        }
    }
}
