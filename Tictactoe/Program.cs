using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField;
using RandomSolution;
using MiniMaxSolution;
using HumanPlayer;

namespace Tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            byte field = 15;
            byte win = 5;
            IPlayable p1;
            Console.WriteLine("Would you like to play 1 - yes:");
            int c;
            Int32.TryParse(Console.ReadLine(), out c);
            if (c == 1)
            {
                p1 = new HPlayer(field, win, (byte)1);
            }
            else
            {
                p1 = new MiniMaxPlayer(field, win, (byte)1);
            }

            IPlayable p2 = new MiniMaxPlayer(field, win, (byte)2);
            Game game = new Game(field, win, p1, p2);
            game.StartGame();
            Console.ReadKey();
        }
    }
}
