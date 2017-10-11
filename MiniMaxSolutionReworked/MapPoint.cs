using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxSolutionReworked
{
    public class MapPoint
    {
        public byte Row { get; set; }
        public byte Column { get; set; }
        public int Score { get; set; }
        public CellValue Value { get; set; }

        public MapPoint(byte x, byte y)
        {
            Row = x;
            Column = y;
            Score = 0;
            Value = CellValue.EmptyCell;
        }
        public byte[] GetByteRepresentation()
        {
            return new byte[] { this.Row, this.Column };
        }

    }
    public enum CellValue : byte { OwnCell, OppCell, EmptyCell }
}
