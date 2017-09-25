using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class Utils
    {
        public static HashSet<Tuple<byte, byte>> FindMoves (byte[,] board)
        {
            byte sizeOfBoard = (byte)board.GetLength(1);
            HashSet<Tuple<byte, byte>> res = new HashSet<Tuple<byte, byte>>();
            for (byte i = 0; i < sizeOfBoard; i++)
            {
                for (byte j = 0; j < sizeOfBoard; j++)
                {
                    if (board[i, j] == 0 && hasNonZeroNeighbours(i, j))
                    {
                        res.Add(new Tuple<byte, byte>(i, j));
                    }
                }
            }
            return res;

            bool hasNonZeroNeighbours(byte i, byte j)
            {
                if (i > 0)
                {
                    if (board[i - 1, j] != 0) return true;
                    if (j > 0 && board[i - 1, j - 1] != 0) return true;
                    if (j < sizeOfBoard - 1 && board[i - 1, j + 1] != 0) return true;
                }
                if (i < sizeOfBoard - 1)
                {
                    if (board[i + 1, j] != 0) return true;
                    if (j > 0 && board[i + 1, j - 1] != 0) return true;
                    if (j < sizeOfBoard - 1 && board[i + 1, j + 1] != 0) return true;
                }
                if (j < sizeOfBoard - 1)
                {
                    if ((board[i, j + 1] != 0)) return true;
                }
                if (j > 0)
                {
                    if ((board[i, j - 1] != 0)) return true;
                }

                return false;
            }
        }

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
    }
}
