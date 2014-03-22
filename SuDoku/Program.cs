using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuDoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] board1 = new int[][]{ 
                                        new int[] {1, 2, 0, 3}, 
                                        new int[] {0, 3, 0, 2}, 
                                        new int[] {3, 0, 2, 0}, 
                                        new int[] {0, 4, 0, 1}, 
                                       };

            if (SolveBoard(board1))
            {
                Console.WriteLine("Solved board");

                for (int i = 0; i < board1.Length; i++)
                {
                    for (int j = 0; j < board1.Length; j++)
                    {
                        Console.Write(board1[i][j] + " ");
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Error: board not solved");
            }
        }

        static bool SolveBoard(int[][] board)
        {
            int row = 0;
            int column = 0;
            int regionSize = 2;

            if (!GetFirstEmptyCell(board, ref row, ref column))
            {
                // If the board is full then, check 
                // whether it has been solved correctly
                return IsValid(board, regionSize);
            }

            for (int value = 1; value <= 4; value++)
            {
                if (IsValid(board, row, column, value))
                {
                    board[row][column] = value;

                    if (SolveBoard(board))
                    {
                        return true;
                    }

                    // Since the board was not 
                    // solved, remove the value
                    board[row][column] = 0;
                }
            }

            return false;
        }

        // For given row and column, 
        // check if any other cell in row contains value
        // check if any other cell in column contains value
        // check if the bounding region contains value
        private static bool IsValid(int[][] board, int row, int column, int value)
        {
            // check row
            for (int i = 0; i < board.Length; i++)
            {
                if (board[row][i] == value)
                {
                    return false;
                }
            }

            // check column
            for (int j = 0; j < board.Length; j++)
            {
                if (board[j][column] == value)
                {
                    return false;
                }
            }

            // check region
            // Now check the 3 x 3 square surrounding this cell
            int blockRowStart = 0;
            int blockColumnStart = 0;
            int regionSize = 2;
            GetRegion(row, column, ref blockRowStart, ref blockColumnStart, regionSize);

            for (int i = blockRowStart; i < (blockRowStart + regionSize); i++)
            {
                for (int j = blockColumnStart; j < (blockColumnStart + regionSize); j++)
                {
                    if (board[i][j] == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static bool GetFirstEmptyCell(int[][] board, ref int row, ref int column)
        {
            int n = board.Length;

            for (row = 0; row < n; row++)
            {
                for (column = 0; column < n; column++)
                {
                    if (board[row][column] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static void GetRegion(int currentRow, int currentCol, ref int blockRowStart, ref int blockColumnStart, int regionSize)
        {
            int blockRow = currentRow / regionSize;

            int blockColumn = currentCol / regionSize;

            blockRowStart = blockRow * regionSize;
            blockColumnStart = blockColumn * regionSize;
        }


        // For a filled up board, check 
        // if the board is valid
        static bool IsValid(int[][] board, int regionSize)
        {
            int n = board.Length;

           // Check all rows
           for(int i = 0; i < n; i++)
           {
               HashSet<int> rowValues = new HashSet<int>();

               for(int j = 0; j < n; j++)
               {
                  if (rowValues.Add(board[i][j]) == false)
                  {
                     return false;
                  }
               }
           }

           // Check all columns
           for (int j = 0; j < n; j++)
           {
               HashSet<int> columnValues = new HashSet<int>();

               for(int i = 0; i < n; i++)
               {
                  if (columnValues.Add(board[i][j]) == false)
                  {
                     return false; 
                  }
               }
           }

           // Check all blocks
           for (int i = 0; i < n; i += regionSize)
           {
               for (int j = 0; j < n; j += regionSize)
              {
                   HashSet<int> blockValues = new HashSet<int>();

                   for (int k = i; k < i + regionSize; k++)
                   {
                       for (int l = j; l < j + regionSize; l++)
                       {
                           if (blockValues.Add(board[k][l]) == false)
                           {
                               return false; 
                           }
                       }
                   }
              }
           }

           return true;
       }
    }
}
