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
        private int[,] board;
        private int goal;
        private IPlayable[] PlayersArray;

        public Game(int size, int max, IPlayable first, IPlayable second)
        {
            fieldSize = size;
            goal = max;
            PlayersArray = new IPlayable[] { first, second };
            PlayersArray[0].Identificator = 1;
            PlayersArray[1].Identificator = 2;
            board = new int[size, size];
        }

        public void StartGame()
        {
            int currentPlayer = 0;

            while (true)
            {
                int id = PlayersArray[currentPlayer].Identificator;
                try
                {
                    AddTurn(PlayersArray[currentPlayer]);
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
                if (checkResult(id))
                {
                    Console.WriteLine("Game is over. Player {0} won.", id);
                    return;
                }
                else if (checkTie())
                {
                    Console.WriteLine("It's a tie!");
                    return;
                }
                else
                {
                    currentPlayer = (currentPlayer == 0) ? 1 : 0;
                }

            }
        }

        private bool checkResult(int id)
        {
            if (checkHorizntal(id) || checkVertical(id) || checkForwardDiagonal(id) || checkBackwardDiagonal(id))
            {
                return true;
            }
            return false;
        }

        private bool checkHorizntal(int id)
        {
            int sameInRowCount = 0;
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize - (goal - 1); j++)
                {
                    while (board[i, j] == id)
                    {
                        j++;
                        sameInRowCount++;
                        if (sameInRowCount == goal)
                        {
                            return true;
                        }
                    }
                    sameInRowCount = 0;
                }
            }
            return false;
        }
        private bool checkVertical(int id)
        {
            int sameInRowCount = 0;
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize - (goal - 1); j++)
                {
                    while (board[j, i] == id)
                    {
                        j++;
                        sameInRowCount++;
                        if (sameInRowCount == goal)
                        {
                            return true;
                        }
                    }
                    sameInRowCount = 0;
                }
            }
            return false;
        }
        private bool checkForwardDiagonal(int id)
        {
            int sameInRowCount = 0;
            for (int i = 0; i < fieldSize - (goal - 1); i++)
            {
                for (int j = 0; j < fieldSize - (goal - 1); j++)
                {
                    while (board[j, i] == id)
                    {
                        i++;
                        j++;
                        sameInRowCount++;
                        if (sameInRowCount == goal)
                        {
                            return true;
                        }
                    }
                    sameInRowCount = 0;
                }
            }
            return false;
        }
        private bool checkBackwardDiagonal(int id)
        {
            int sameInRowCount = 0;
            for (int i = fieldSize - 1; i > goal - 2; i--)
            {
                for (int j = 0; j < fieldSize - (goal - 1); j++)
                {
                    while (board[j, i] == id)
                    {
                        i--;
                        j++;
                        sameInRowCount++;
                        if (sameInRowCount == goal)
                        {
                            return true;
                        }
                    }
                    sameInRowCount = 0;
                }
            }
            return false;
        }

        private bool checkTie()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void AddTurn(IPlayable player)
        {
            int[] newTurn = player.NextMove(board);
            int currentValueOfCell = board[newTurn[0], newTurn[1]];

            if (currentValueOfCell != 0)
            {
                throw new CellNotEmptyException();
            }
            else
            {
                board[newTurn[0], newTurn[1]] = player.Identificator;
            }
        }

        public void PrintState()
        {
            Console.WriteLine("***********************");
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    Console.Write("{0} ", board[i, j]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("***********************");
        }

        public void Wipe()
        {
            this.board = new int[fieldSize, fieldSize];
        }
    }
}
