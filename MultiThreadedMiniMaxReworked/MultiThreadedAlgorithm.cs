using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using AlgorithmReworked;

namespace MultiThreadedMiniMaxReworked
{
    public class MultiThreadedAlgorithm
    {
        public byte signInRowToWin { get; set; }
        private MiniMax algorithm;
        public MultiThreadedAlgorithm(byte signInRowToWin)
        {
            this.signInRowToWin = signInRowToWin;
            algorithm = new MiniMax(signInRowToWin);
        }
        public byte[] GetOptimalStep(byte[,] playField, HashSet<byte[]> CellsToCheck, int depth)
        {
            Dictionary<byte[], int> iscores = new Dictionary<byte[], int>();
            foreach (byte[] cell in CellsToCheck)
            {
                byte[,] b = (byte[,])playField.Clone();
                b[cell[0], cell[1]] = 1;
                int s = algorithm.Evaluate(b, cell, 1);
                iscores[cell] = s;
            }
            var items = from i in iscores orderby i.Value descending select new {coordinates = i.Key, estimation = i.Value };
            var cellsToCheckList = items.Take(10).ToList();
            if (cellsToCheckList[0].estimation > 430000)
            {
                return cellsToCheckList[0].coordinates;
            }
            Dictionary<byte[], int> scores = new Dictionary<byte[], int>();

            Parallel.ForEach(cellsToCheckList, cell =>
            {

                int score = algorithm.EvaluateCell((byte[,])playField.Clone(),cell.coordinates, 1,(byte)depth);
                lock (scores)
                {
                    scores.Add(cell.coordinates, score);
                }             
                
            });
            var item = (from entry in scores orderby entry.Value descending select entry).FirstOrDefault();
            return item.Key;
        }
    }
}
