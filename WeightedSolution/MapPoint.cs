using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedSolution
{
    public class MapPoint
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool ToCheck { get; set; } // indicates if this cell should be checked during next turn
        public int Score { get; set; }
        private CellValue _value;
        public CellValue Value {
            get {
                return _value;
                }
            set {
                if (value != CellValue.EmptyCell)
                {
                    foreach (MapPoint item in NeighbourPoints)
                    {
                        if (item.Value == CellValue.EmptyCell)
                        {
                            item.ToCheck = true;
                        }
                        
                    }
                    _value = value;
                }
                }
        }
        public List<MapPoint> NeighbourPoints; //list of all neighbour cells

        public MapPoint(int x, int y)
        {
            Row = x;
            Column = y;
            ToCheck = false;
            Score = 0;
            Value = CellValue.EmptyCell;
        }

    }


    public enum CellValue: byte { OwnCell, OppCell, EmptyCell}
}
