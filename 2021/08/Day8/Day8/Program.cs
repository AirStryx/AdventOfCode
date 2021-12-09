using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Tuple<List<string>, List<string>>> OutputEntries = ReadFile();
			int output = 0;

			for (int i = 0; i < OutputEntries.Count; i++)
			{
				List<string> key = GenerateKey(OutputEntries[i].Item1);
				output += GenerateOutput(key, OutputEntries[i].Item2);
			}

			Console.WriteLine(output);
		}

		/// <summary>
		/// reads the file
		/// </summary>
		/// <returns></returns>
		static private List<Tuple<List<string>, List<string>>> ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\AdventOfCode\2021\08\Files\Final.txt");
			List<Tuple<List<string>, List<string>>> end = new List<Tuple<List<string>, List<string>>>();
			for (int i = 0; i < lines.Length; i++)
			{
				string firstLine = lines[i].Substring(0, lines[i].IndexOf('|'));
				List<string> firstPart = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

				string finalLine = lines[i].Substring(lines[i].IndexOf('|') + 1);
				List<string> lastPart = finalLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

				end.Add(new Tuple<List<string>, List<string>>(firstPart, lastPart));
			}

			return end;
		}

		static private int GenerateOutput(List<string> key, List<string> output)
		{
			string outputValue = "";

			for (int i = 0; i < output.Count; i++)
			{
				outputValue += GetNumberByKey(key, output[i]);
			}


			return int.Parse(outputValue);
		}

		static private string GetNumberByKey(List<string> key, string output)
		{
			List<string> filters = new List<string>() { "012456", "25", "02346", "02356", "1235", "01356", "013456", "025", "0123456", "012356" };
			string KeyToEntries = "";
			for (int i = 0; i < key.Count; i++)
			{
				if(output.Contains(key[i]))
				{
					KeyToEntries += i.ToString();
				}
			}

			String.Concat(KeyToEntries.OrderBy(c => c));

			for (int i = 0; i < filters.Count; i++)
			{
				if (KeyToEntries == filters[i]) return i.ToString();
			}

			return "FOUT";
		}

		static private List<string> GenerateKey(List<string> decodeValues)
		{
			List<string> key = new List<string>(7) { "", "", "", "", "", "", "" };
			//Fill in the base
			//FIND 1, fill in those values in slots 2 and 5
			string numberOne = FindBySize(decodeValues, 2);
			key[2] = numberOne;
			key[5] = numberOne;

			//FIND 7, Fill in those vals in slot 0 and remove values present in 2 and 5
			string numberSeven = FindBySize(decodeValues, 3);
			key[0] = CleanString(numberSeven, numberOne);

			//FIND 3 AND FIND VALUE FOR 3 AND 6
			string numberThree = FindBySize(decodeValues, 5, numberSeven);
			key[3] = CleanString(numberThree, numberSeven);
			key[6] = CleanString(numberThree, numberSeven);

			//USE 4 TO CLEAN UP 3 6 AND FILL 1
			string numberFour = FindBySize(decodeValues, 4);
			numberFour = CleanString(numberFour, numberOne);
			key[3] = FindValueInString(key[3], numberFour);
			key[6] = CleanString(key[6], key[3]);
			key[1] = CleanString(numberFour, key[3]);

			//FIND 5 TO CLEAR UP DIFFERENCE BETWEEN 2 AND 5
			string numberFive = FindBySize(decodeValues, 5, key[0] + key[1] + key[3]);
			key[2] = CleanString(numberOne, numberFive);
			key[5] = CleanString(numberOne, key[2]);

			//finally get 4
			key[4] = CleanString(FindBySize(decodeValues, 7), key[0] + key[1] + key[2] + key[3] + key[5] + key[6]);

			return key;
		}

		static string CleanString(string stringToClean, string valuesToRemove)
		{
			for (int i = 0; i < valuesToRemove.Length; i++)
			{
				stringToClean = stringToClean.Replace(valuesToRemove[i].ToString(), "");
			}

			return stringToClean;
		}

		static string FindValueInString(string stringToClean, string valuesToFind)
		{
			for (int i = 0; i < stringToClean.Length; i++)
			{
				for (int j = 0; j < valuesToFind.Length; j++)
				{
					if (stringToClean[i] == valuesToFind[j])
					{
						return valuesToFind[j].ToString();
					}
				}
			}

			return "";
		}

		static private string FindBySize(List<string> decodeValues, int size, string filter = "")
		{
			if(filter == string.Empty)
			{
				for (int i = 0; i < decodeValues.Count; i++)
				{
					if (decodeValues[i].Length == size) return decodeValues[i];
				}
			}
			else
			{
				for (int i = 0; i < decodeValues.Count; i++)
				{
					if (decodeValues[i].Length == size)
					{
						int count = 0;

						for (int j = 0; j < filter.Length; j++)
						{
							if (decodeValues[i].Contains(filter[j]))
							{
								count++;
							}
						}

						if(count >= filter.Length)
							return decodeValues[i];
					}
				}
			}

			return string.Empty;
		}
	}
}
