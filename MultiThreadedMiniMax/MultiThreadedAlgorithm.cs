using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Algorithm;

namespace MultiThreadedMiniMax
{
    public class MultiThreadedAlgorithm
    {
        public byte signInRowToWin { get; set; }
        public MultiThreadedAlgorithm(byte signInRowToWin)
        {
            this.signInRowToWin = signInRowToWin;
        }
        public byte[] GetOptimalStep(byte[,] playField)
        {
            MiniMax algorithm = new MiniMax(signInRowToWin);
            Dictionary<Tuple<byte, byte>, int> scores = new Dictionary<Tuple<byte, byte>, int>();

            int length = playField.GetLength(0);

            for (byte i = 0; i < length; i++)
            {
                for (byte j = 0; j < length; j++)
                {
                    if (playField[i, j] == 0)
                    {
                        Tuple<byte, byte> coordinate = new Tuple<byte, byte>((byte)i, (byte)j);
                        Task<int> task = Task<int>.Factory.StartNew(() => algorithm.EvaluateCell((byte[,])playField.Clone(), new byte[] { coordinate.Item1, coordinate.Item2 }));
                        scores.Add(coordinate, task.Result);
                        task.Dispose();
                    }
                }
            }
            var item = (from entry in scores orderby entry.Value descending select entry).FirstOrDefault();

            return new byte[] { item.Key.Item1, item.Key.Item2};
        }
    }
}
