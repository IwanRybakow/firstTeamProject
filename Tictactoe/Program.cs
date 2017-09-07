using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField;
using RandomSolution;

namespace Tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            int field = 5;
            int win = 3;
            IPlayable p1 = new RandSolution(field, win);
            IPlayable p2 = new RandSolution(field, win);
            Game game = new Game(field, win, p1, p2);
            game.StartGame();
            Console.ReadKey();
        }
    }
}
