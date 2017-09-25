using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleField
{
    public interface IPlayable
    {
        byte Identificator { get; set; }
        string Name { get; set; }
        byte[] NextMove(byte[,] currentState, byte[] OppMove);
    }
}
