using System;
using System.Numerics;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] values = ReadFile();

			string oxygenValue = CheckValue(values.ToList<string>(), true);
			string coValue = CheckValue(values.ToList<string>(), false);


			int oxygen = Convert.ToInt32(oxygenValue, 2);
			int co = Convert.ToInt32(coValue, 2);

			float answer = oxygen * co;
			Console.WriteLine(answer);
		}

		static private string[] ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\Advent of code\2021\03\Files\Final.txt"); //Runs the test code

			return lines;
		}

		static private string CheckValue(List<string> listToCheck, bool checkHighest)
		{

			bool done = false;

			for (int i = 0; i < listToCheck[0].Length; i++)
			{
				int oneCounter = 0;
				int zeroCounter = 0;

				for (int j = 0; j < listToCheck.Count; j++)
				{
					if (listToCheck[j][i] == '0')
					{
						zeroCounter++;
					}
					else
					{
						oneCounter++;
					}
				}

				if(checkHighest)
				{
					if (oneCounter >= zeroCounter)
					{
						for (int j = listToCheck.Count - 1; j >= 0; j--)
						{
							if (listToCheck[j][i] == '0')
							{
								listToCheck.RemoveAt(j);
								if (listToCheck.Count == 1)
								{
									done = true;
									break;
								}
							}
						}
					}
					else
					{
						for (int j = listToCheck.Count - 1; j >= 0; j--)
						{
							if (listToCheck[j][i] == '1')
							{
								listToCheck.RemoveAt(j);
								if (listToCheck.Count == 1)
								{
									done = true;
									break;
								}
							}
						}
					}
				}
				else
				{
					if (oneCounter < zeroCounter)
					{
						for (int j = listToCheck.Count - 1; j >= 0; j--)
						{
							if (listToCheck[j][i] == '0')
							{
								listToCheck.RemoveAt(j);
								if (listToCheck.Count == 1)
								{
									done = true;
									break;
								}
							}
						}
					}
					else
					{
						for (int j = listToCheck.Count - 1; j >= 0; j--)
						{
							if (listToCheck[j][i] == '1')
							{
								listToCheck.RemoveAt(j);
								if (listToCheck.Count == 1)
								{
									done = true;
									break;
								}
							}
						}
					}
				}

				if (done) break;
			}

			return listToCheck[0];
		}
	}
}
