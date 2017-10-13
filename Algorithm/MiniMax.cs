using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class MiniMax
    {
        private byte signsInRowToWin;
        public PatternCollection ownPatterns;
        public PatternCollection opponentPatterns;

        public MiniMax(byte signsInRowToWin)
        {
            this.signsInRowToWin = signsInRowToWin;
            ownPatterns = new PatternCollection(signsInRowToWin, 1);
            opponentPatterns = new PatternCollection(signsInRowToWin, 2);
        }

        public int EvaluateCell (byte[,] board, byte[] move, byte sign = 1, byte depth = 2)
        {
            int result = Evaluate(board, move, sign);
            if (result >= 20000000 || depth == 0)
            {
                return result;
            }
            else
            {
                byte oppSign = sign == 1 ? (byte)2 : (byte)1;
                depth--;
                byte[,] newBoard = (byte[,])board.Clone();
                newBoard[move[0], move[1]] = sign;
                HashSet<byte[]> cells = Utils.FindMoves(newBoard);
                Dictionary<byte[], int> scores = new Dictionary<byte[], int>();
                foreach (byte[] cell in cells)
                {
                    byte[,] b = (byte[,])newBoard.Clone();
                    b[cell[0], cell[1]] = oppSign;
                    int s = Evaluate(b, cell, oppSign);
                    scores[cell] = s;
                }
                var items = from i in scores orderby i.Value descending select i.Key;
                List<byte[]> cellsToCheckList = items.Take(2).ToList();

                int score = int.MinValue;
                foreach (byte[] item in cellsToCheckList)
                {
                    int temp = EvaluateCell(newBoard, item, oppSign, depth);
                    if (temp > score)
                    {
                        score = temp;
                    }
                }
                return result - score;
            }        
             
        }



        public int Evaluate(byte[,] board, byte[] move, byte sign)
        {
            byte oppSign = sign == 1 ? (byte)2 : (byte)1;
            int result = 0;
            board[move[0], move[1]] = sign;
            List<string> StringsToCheck = Utils.FindStrings(board, move, signsInRowToWin);
            board[move[0], move[1]] = oppSign;
            StringsToCheck.AddRange(Utils.FindStrings(board, move, signsInRowToWin));
            board[move[0], move[1]] = 0;
            PatternCollection patterns = sign == 1 ? ownPatterns : opponentPatterns;
            foreach (string item in StringsToCheck)
            {
                foreach (Pattern pattern in patterns.PatternList)
                {
                    if (item.Contains(pattern.PatternString))
                    {
                        result += pattern.Weight;
                    }
                }
            }
            return result;
        }
    }
}
