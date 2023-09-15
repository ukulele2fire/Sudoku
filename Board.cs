using System;
using System.Collections.Generic;
using System.Linq;

public class Board
{
	public int[,] myMatrix { get; set; }
	public int solutionCount { get; set;}

	public Board()
	{
		solutionCount = 0;
		myMatrix = new int[9,9];
	}


	public Board(int[,] matrix)
    {
		solutionCount = 0;
		myMatrix = matrix;
    }

	//prints the board
	public void printBoard()
    {
		Console.WriteLine();
		for (int r = 0; r <= 8; r++)
        {
			if (r != 0 && r % 3 == 0)
				Console.WriteLine("---------------------------------");
			for (int c = 0; c <= 8; c++)
            {
				if (c % 3 == 0 && c != 0)
					Console.Write(" | ");
				string num = myMatrix[r, c] + "";
				if (myMatrix[r, c] == 0)
					num = " ";
				Console.Write("[" + num + "]");
            }
			Console.WriteLine();
        }
		Console.WriteLine("Number of Solutions: " + solutionCount);
    }

	//check if board is correct
	public bool checkBoard()
    {
		for (int i = 0; i <= 8; i++)
        {
			if (!checkRow(i) || !checkCol(i))
				return false;
        }
		if (!checkBoxes())
			return false;
		else
			return true;
    }

	public bool checkChange(int r, int c)
    {
		if (checkRow(r) && checkCol(c))
        {
			if (r >= 6)
				r = 7;
			else if (r >= 3)
				r = 4;
			else
				r = 1;

			if (c >= 6)
				c = 7;
			else if (c >= 3)
				c = 4;
			else
				c = 1;

			return checkBox(r, c);
		}
		return false;
    }

	//check if row is correct
	bool checkRow(int r)
    {
		HashSet<int> rowNums = new HashSet<int>();
		for (int c = 0; c <= 8; c++)
        {
			int num = myMatrix[r,c];
			bool duplicate = !rowNums.Add(num);
			if (num != 0 && duplicate)
				return false;
        }
		return true;
    }

	//check if column is correct
	bool checkCol(int c)
    {
		HashSet<int> colNums = new HashSet<int>();
		for (int r = 0; r <= 8; r++)
		{
			int num = myMatrix[r,c];
			bool duplicate = !colNums.Add(num);
			if (num != 0 && duplicate)
				return false;
		}
		return true;
	}

	//check all boxes are correct
	bool checkBoxes()
    {
		//check all squares in a box

		for (int r = 1; r < 8; r += 3)
        {
			for (int c = 1; c < 8; c += 3)
            {
				if (!checkBox(r, c)) //box is incorrect
					return false;
            }
        }

		return true;
	}

	//check select box for correct
	bool checkBox(int r, int c)
    {

		HashSet<int> boxNums = new HashSet<int>();
		for (int rp = -1; rp <= 1; rp++)
		{
			for (int cp = -1; cp <= 1; cp++)
			{
				int num = myMatrix[r + rp,c + cp];
				bool duplicate = !boxNums.Add(num);
				if (num != 0 && duplicate)
					return false;
			}
		}
		return true;
	}

	//solve the sudoku board
	//true if solvable
	public bool solve()
    {
		solutionCount = 0;
		return solveHelper(0);
    }

	//recursive helper
	bool solveHelper(int i)
    {
		printBoard();
		if (i == 81) //got through all spaces successfully
		{
			solutionCount++;
			return true;
		}
		else  //still working
		{
			int r = i / 9;
			int c = i % 9;

			if (myMatrix[r, c] != 0)  //space is already filled
				return solveHelper(i + 1);
			else
			{
				for (int n = 1; n <= 9; n++)  //loop through each number
				{
					myMatrix[r, c] = n;  //try number
					
					if (checkChange(r,c) && solveHelper(i + 1)) //if board is correct and works for next iterations
						return true;
				}
				myMatrix[r, c] = 0;  //undo change
				return false; 
			}
		}
    }

	//generate unique uncompleted board
	public void generateBoard()
    {

    }

	//fill board randomly with a unique solution
	public void fillBoard()
    {
		List<int> indicies = new List<int>();
		List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		for (int i = 0; i <= 80; i++)
			indicies.Add(i);

		indicies = (List<int>)Shuffle(indicies);

		for (int j = 0; j <= 17; j++)
		{
			Console.WriteLine(j);
			printBoard();
			int index = indicies.ElementAt(j);
			int row = index / 9;
			int col = index % 9;

			nums = (List<int>)Shuffle(nums);


			for (int k = 0; k <= 8; k++)
			{
				int num = nums.ElementAt(k);

				myMatrix[row, col] = num;
				Console.WriteLine(num + " placed at " + row + ", " + col);
				int[,] unsolved = myMatrix.Clone() as int[,];

					
				bool solvable = false;
				if (j > 0 && checkChange(row,col))
					solvable = solve();

				myMatrix = unsolved;

				if (j == 0 || solvable)
                   k = 9;

            }
		}
	}

	//randomly remove numbers ensuring unique solution
	void removeBoxes()
    {

    }

	public static IList<T> Shuffle<T>(IList<T> values)
	{
		Random rand = new Random();
		for (int i = values.Count - 1; i > 0; i--)
		{
			int k = rand.Next(i + 1);
			T value = values[k];
			values[k] = values[i];
			values[i] = value;
		}
		return values;
	}



}
