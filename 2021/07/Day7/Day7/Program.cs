using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
	class Program
	{
		static void Main(string[] args)
		{
			List<int> values = ReadFile();
			List<int> editableValues = ReadFile();
			editableValues.Sort();
			int efficientPoint = GetEfficientPoint(editableValues);
			int fuelcost = 0;

			for (int i = 0; i < values.Count; i++)
			{
				int intermediate = Math.Abs(values[i] - efficientPoint);
				fuelcost += (intermediate * (intermediate + 1)) / 2;
			}

			Console.WriteLine(fuelcost);
		}
		
		/// <summary>
		/// reads the file
		/// </summary>
		/// <returns></returns>
		static private List<int> ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\AdventOfCode\2021\07\Files\Final.txt");

			List<int> intValues = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

			return intValues;
		}

		static int GetEfficientPoint(List<int> listToFilter)
		{
			int final = 0;
			for (int i = 0; i < listToFilter.Count; i++)
			{
				final += listToFilter[i];
			}

			float endpoint = (float)final / listToFilter.Count;
			endpoint = MathF.Round(endpoint) - 1;

			return (int)endpoint;
		}
	}
}
