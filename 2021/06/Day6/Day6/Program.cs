using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
	class Program
	{

		static void Main(string[] args)
		{
			List<long> values = ReadFile();
			int timerAmount = 256;

			long day0 = 0;
			long day1 = 0;
			long day2 = 0;
			long day3 = 0;
			long day4 = 0;
			long day5 = 0;
			long day6 = 0;
			long day7 = 0;
			long day8 = 0;

			foreach (var item in values)
			{
				switch (item)
				{
					case 0:
						day0++;
						break;
					case 1:
						day1++;
						break;
					case 2:
						day2++;
						break;
					case 3:
						day3++;
						break;
					case 4:
						day4++;
						break;
					case 5:
						day5++;
						break;
					case 6:
						day6++;
						break;
					case 7:
						day7++;
						break;
					case 8:
						day8++;
						break;
					default:
						break;
				}
			}

			for (int i = 0; i < timerAmount; i++)
			{
				long temp = day8;
				long temp0 = day0;
				day8 = day0;
				day0 = day1;
				day1 = day2;
				day2 = day3;
				day3 = day4;
				day4 = day5;
				day5 = day6;
				day6 = day7 + temp0;
				day7 = temp;
			}

			Console.WriteLine(day0 + day1 + day2 + day3 + day4 + day5 + day6 + day7 + day8);
		}

		/// <summary>
		/// reads the file
		/// </summary>
		/// <returns></returns>
		static private List<long> ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\AdventOfCode\2021\06\Files\Final.txt");

			List<long> returnVal = lines[0].Split(',').Select(long.Parse).ToList();
			return returnVal;
		}
	}
}
