using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleField
{
    public class Game
    {
        private int fieldSize;
        private byte[,] board;
        private byte goal;
        private IPlayable[] PlayersArray;
        private int movesCount = 0;

        public Game(int size, byte max, IPlayable first, IPlayable second)
        {
            fieldSize = size;
            goal = max;
            PlayersArray = new IPlayable[] { first, second };
            PlayersArray[0].Identificator = 1;
            PlayersArray[1].Identificator = 2;
            board = new byte[size, size];
        }

        public void StartGame()
        {
            int currentPlayer = 0;
            byte[] move = new byte[] { (byte)255, (byte)255 };
            while (true)
            {
                byte id = PlayersArray[currentPlayer].Identificator;
                try
                {
                    move = AddTurn(PlayersArray[currentPlayer], move);
                    //Console.WriteLine("Player {0} made turn {1} {2}", id, move[0], move[1]);
                    PrintState();
                }
                catch (CellNotEmptyException)
                {
                    Console.WriteLine("This cell is already taken. Player {0} lost.", id);
                    return;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Turn is out of range of the field. Player {0} lost.", id);
                    return;
                }
                if (checkResult(id, move))
                {
                    Console.WriteLine("Game is over. Player {0} won.", id);
                    return;
                }
                else if (movesCount == fieldSize*fieldSize) //if number of moves equals to the number of cells - there is no more empty cells - it's a tie
                {
                    Console.WriteLine("It's a tie!");
                    return;
                }
                else
                {
                    currentPlayer = (currentPlayer == 0) ? 1 : 0; // change player
                }

            }
        }

        private bool checkResult(byte id, byte[] move)
        {
            if (checkBackwardDiagonal(id, move) || checkForwardDiagonal(id, move) || checkHorizntal(id, move[0]) || checkVertical(id, move[1]))
            {
                return true;
            }
            return false;
        }

        private bool checkHorizntal(byte id, byte row)
        {
            //check only row of last turn
            int sameInRowCount = 0;
            for (int i = 0; i < fieldSize - (goal - 1); i++)
            {
                while (board[row, i] == id)
                {
                    i++;
                    sameInRowCount++;
                    if (sameInRowCount == goal)
                    {
                        return true;
                    }
                }
                sameInRowCount = 0;
            }
            return false;
        }
        private bool checkVertical(byte id, byte column)
        {
            //check only column of last turn
            int sameInRowCount = 0;
            for (int i = 0; i < fieldSize - (goal - 1); i++)
            {
                while (board[i, column] == id)
                {
                    i++;
                    sameInRowCount++;
                    if (sameInRowCount == goal)
                    {
                        return true;
                    }
                }
                sameInRowCount = 0;
            }
            return false;
        }
        private bool checkForwardDiagonal(byte id, byte[] move)
        {
            //check only diagonal of last turn, looking top left to bottom right
            int min = move.Min();
            //define starting poit
            int row = move[0] - min;
            int column = move[1] - min;
            int numOfCells = fieldSize - column - row;
            if (numOfCells < goal) // check if diagonal is long enough
            {
                return false;
            }
            int sameInRowCount = 0;
            for (int i = 0; i < numOfCells - (goal-1); i++)
            {
                while (board[row, column] == id)
                {
                    i++;
                    row++;
                    column++;
                    sameInRowCount++;
                    if (sameInRowCount == goal)
                    {
                        return true;
                    }
                }
                row++;
                column++;
                sameInRowCount = 0;                
            }
            return false;
        }
        private bool checkBackwardDiagonal(byte id, byte[] move)
        {
            //check only diagonal of last turn, looking bottom left to top right
            //define starting poit
            int row = move[0];
            int column = move[1];
            while (row < fieldSize -1 && column > 0)
            {
                row++;
                column--;
            }
            int numOfCells = row - column + 1;
            if (numOfCells < goal) // check if diagonal is long enough
            {
                return false;
            }
            int sameInRowCount = 0;
            for (int i = 0; i < numOfCells - (goal - 1); i++)
            {
                while (board[row, column] == id)
                {
                    i++;
                    row--;
                    column++;
                    sameInRowCount++;
                    if (sameInRowCount == goal)
                    {
                        return true;
                    }
                }
                row--;
                column++;
                sameInRowCount = 0;
                
            }
            return false;
        }

        public byte[] AddTurn(IPlayable player, byte[] oppMove)
        {
            byte[] newTurn = player.NextMove((byte[,])board.Clone(), goal);
            byte currentValueOfCell = board[newTurn[0], newTurn[1]];

            if (currentValueOfCell != 0)
            {
                Console.WriteLine("Cell {0}{1} is taken", newTurn[0], newTurn[1]);
                throw new CellNotEmptyException();
            }
            else
            {
                board[newTurn[0], newTurn[1]] = player.Identificator;
                movesCount++;
                return newTurn;
            }
        }

        public void PrintState()
        {
            StringBuilder border = new StringBuilder();
            border.Append('-', fieldSize);
            Console.WriteLine("");
            //Console.Clear();
            Console.Write("  |");
            for (int i = 0; i < fieldSize; i++)
            {
                Console.Write("{0, 2}|",i+1);
                
            }
            Console.WriteLine("");
            for (int i = 0; i < fieldSize; i++)
            {
                Console.Write("{0, 2}", i + 1);
                Console.Write("|");
                for (int j = 0; j < fieldSize; j++)
                {
                    if (board[i, j] == 0)
                    {
                        Console.Write("{0, 3}", "|");
                    }
                    else if (board[i, j] == 1)
                    {
                        Console.Write("{0, 3}", "X|");
                    }
                    else if (board[i, j] == 2)
                    {
                        Console.Write("{0, 3}", "0|");
                    }
                }
                Console.WriteLine("");
            }
        }

        public void Wipe()
        {
            this.board = new byte[fieldSize, fieldSize];
        }
    }
}
