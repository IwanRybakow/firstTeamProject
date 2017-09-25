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
        private PatternUtils ownPatterns;
        private PatternUtils opponentPatterns;
        public MiniMax(byte signsInRowToWin)
        {
            this.signsInRowToWin = signsInRowToWin;
            ownPatterns = new PatternUtils(signsInRowToWin, 1);
            opponentPatterns = new PatternUtils(signsInRowToWin, 2);
        }

        public int EvaluateCell (byte[,] board, byte[] move, byte sign = 1, byte depth = 0)
        {
            byte oppSign = sign == 1 ? (byte)2 : (byte)1;
            int result = 0;
            board[move[0], move[1]] = sign;
            List<string> StringsToCheck = Utils.FindStrings(board,move,signsInRowToWin);
            board[move[0], move[1]] = oppSign;
            StringsToCheck.AddRange(Utils.FindStrings(board, move, signsInRowToWin));
            board[move[0], move[1]] = 0;
            PatternUtils patterns = sign == 1 ? ownPatterns : opponentPatterns;
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

            if (result >= 20000000 || depth == 0)
            {
                return result;
            }
            else
            {
                depth--;
                byte[,] newBoard = (byte[,])board.Clone();
                newBoard[move[0], move[1]] = sign;
                HashSet<Tuple<byte, byte>> cellsToCheckList = Utils.FindMoves(newBoard);
                Dictionary<Tuple<byte, byte>, int> scores = new Dictionary<Tuple<byte, byte>, int>();
                foreach (Tuple<byte, byte> item in cellsToCheckList)
                {
                    scores[item] = EvaluateCell(newBoard, new byte[] { item.Item1, item.Item2}, oppSign, depth);
                }
                var i = (from entry in scores orderby entry.Value descending select entry).FirstOrDefault();
                return result - i.Value;
            }        
             
        }
    }
}
