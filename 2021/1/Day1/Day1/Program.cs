using System;
using System.Collections;

namespace Day1
{
	class Program
	{
		static void Main(string[] args)
		{

			int[] values = ReadFile();

			int counter = 0;
			int previousVal = values[0] + values [1] + values [2];
			for (int i = 3; i < values.Length; i++)
			{
				int currentval = values[i] + values[i - 1] + values[i - 2];

				if(previousVal < currentval)
				{
					++counter;
				}
				previousVal = currentval;
			}
			

			Console.WriteLine(counter);
		}

		static private int[] ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\Advent of code\2021\1\Files\Test.txt"); //Runs the test code
			//string[] lines = System.IO.File.ReadAllLines(@"F:\Advent of code\2021\1\Files\Final.txt"); //runs the actual code

			int[] linesInt = Array.ConvertAll(lines, int.Parse);

			return linesInt;
		}
	}
}
