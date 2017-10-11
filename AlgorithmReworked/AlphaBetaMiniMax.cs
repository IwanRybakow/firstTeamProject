using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmReworked
{
    public class ABMiniMax
    {
        private byte signsInRowToWin;
        private PatternUtils Patterns;
        private PatternUtils opponentPatterns;
        private Dictionary<String, int> ownCache = new Dictionary<string, int>();
        public Dictionary<string, int> cache = new Dictionary<string, int>(); 
        public ABMiniMax(byte signsInRowToWin)
        {
            this.signsInRowToWin = signsInRowToWin;
            Patterns = new PatternUtils(signsInRowToWin, 1);
        }

        public int AlphaBeta(byte[,] node, byte[] move, byte depth, int alpha, int beta, bool isMaximizing)
        {
            byte sign = isMaximizing ? (byte)1 : (byte)2;
            byte[,] newNode = (byte[,])node.Clone();
            newNode[move[0], move[1]] = sign;
            HashSet<byte[]> cellsToCheckList = Utils.FindMoves(newNode);

            // base case
            if (depth == 0 || cellsToCheckList.Count == 0)
            {
                return EvaluateNode(newNode);
            }

            if (isMaximizing)
            {
                foreach (byte[] cell in cellsToCheckList)
                {
                    alpha = Math.Max(alpha, AlphaBeta(newNode, cell, --depth, alpha, beta, !isMaximizing));
                    if (alpha >= beta)
                    {
                        break;
                    }
                    
                }
                return alpha;
            }
            else
            {
                foreach (byte[] cell in cellsToCheckList)
                {
                    alpha = Math.Min(alpha, AlphaBeta(newNode, cell, --depth, alpha, beta, !isMaximizing));
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return beta;
            }
        }

        public int EvaluateNode (byte[,] board)
        {
            string boardString = board.ToString();
            if (ownCache.ContainsKey(boardString))
            {
                return ownCache[boardString];
            }
            int result = 0;
            List<string> StringsToCheck = Utils.FindStrings(board, signsInRowToWin);
            foreach (string item in StringsToCheck)
            {
                foreach (Pattern pattern in Patterns.PatternList)
                {
                    if (item.Contains(pattern.PatternString))
                    {
                        result += pattern.Weight;
                    }
                }
            }
            ownCache[boardString] = result;
            return result;
        }
    }
}
