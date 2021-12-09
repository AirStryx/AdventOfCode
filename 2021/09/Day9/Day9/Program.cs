using System;

namespace Day9
{
	class Program
	{
		static void Main(string[] args)
		{
			int[,] grid = ReadFile();
			int finalCount = 0;
			int clampX = grid.GetLength(0);
			int clampY = grid.GetLength(1);

			for (int i = 0; i < clampX; i++)
			{
				for (int j = 0; j < clampY; j++)
				{
					int checkCount = 0;
					if (grid[i, j] < grid[Math.Clamp(i - 1, 0, clampX - 1), j])
					{
						checkCount++;
					}
					else if(i - 1 < 0)
					{
						checkCount++;
					}

					if (grid[i, j] < grid[Math.Clamp(i + 1, 0, clampX - 1), j])
					{
						checkCount++;
					}
					else if(i + 1 > clampX - 1)
					{
						checkCount++;
					}

					if (grid[i, j] < grid[i, Math.Clamp(j - 1, 0, clampY - 1)])
					{
						checkCount++;
					}
					else if(j - 1 < 0)
					{
						checkCount++;
					}

					if (grid[i, j] < grid[i, Math.Clamp(j + 1, 0, clampY - 1)])
					{
						checkCount++;
					}
					else if(j + 1 > clampY - 1)
					{
						checkCount++;
					}

					if(checkCount >= 4)
					{
						finalCount += 1+ grid[i, j];
					}
				}
			}
			Console.WriteLine(finalCount);
		}
		
		/// <summary>
		/// reads the file
		/// </summary>
		/// <returns></returns>
		static private int[,] ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\AdventOfCode\2021\09\Files\Final.txt");
			int[,] answers = new int[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					answers[i, j] = int.Parse(lines[i][j].ToString());
				}
			}

			return answers;
		}
	}
	
	
}
