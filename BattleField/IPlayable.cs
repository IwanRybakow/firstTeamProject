using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleField
{
    public interface IPlayable
    {
        int Identificator { get; set; }
        int[] NextMove(int[,] currentState);
    }
}
