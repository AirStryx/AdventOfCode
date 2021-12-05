using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day4
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] values = ReadFile();
			List<int> bingoValues = GetBingoValues(values[0]);
			List<BingoField> bingoFields = GetBingoFields(values, 5);
			bool done = false;
			int sumOfWinning = 0;
			int winningNumber = 0;

			for (int i = 0; i < bingoValues.Count; i++)
			{
				for (int j = 0; j < bingoFields.Count; j++)
				{
					bool bingo = bingoFields[j].EnterValue(bingoValues[i]);

					if(bingo)
					{
						if(bingoFields.Count == 1)
						{
							sumOfWinning = bingoFields[j].GetWinningSum();
							winningNumber = bingoValues[i];
							done = true;
						}
						else
						{
							bingoFields.RemoveAt(j);
							i--; //very hacky but works
						}
						break;
					}
				}

				if (done) break;
			}

			Console.WriteLine(sumOfWinning * winningNumber);
		}

		/// <summary>
		/// reads the file
		/// </summary>
		/// <returns></returns>
		static private string[] ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\Advent of code\2021\4\Files\Final.txt");

			return lines;
		}

		/// <summary>
		/// gets the values that will be rolled
		/// </summary>
		/// <param name="valueLine"></param>
		/// <returns></returns>
		static private List<int> GetBingoValues(string valueLine)
		{
			List<int> returnVal = valueLine.Split(',').Select(int.Parse).ToList();
			return returnVal;
		}

		/// <summary>
		/// generates the bingo fields
		/// </summary>
		/// <param name="values"></param>
		/// <param name="fieldSize"></param>
		/// <returns></returns>
		static private List<BingoField> GetBingoFields(string[] values, int fieldSize)
		{
			List<BingoField> returnVal = new List<BingoField>();

			for (int i = 2; i < values.Length; i+=fieldSize+1)
			{
				List<string> generateVal = new List<string>();
				for (int j = 0; j < fieldSize; j++)
				{
					generateVal.Add(values[i + j]);
				}
				returnVal.Add(new BingoField(generateVal, fieldSize));
			}

			return returnVal;
		}
	}

	class BingoField
	{
		public Dictionary<Vector2, int> FieldValues;
		public Dictionary<Vector2, bool> FieldFound;
		public int GridSize = 0;

		public BingoField(List<string> values, int size)
		{
			GridSize = size;
			FieldValues = new Dictionary<Vector2, int>();
			FieldFound = new Dictionary<Vector2, bool>();

			for (int i = 0; i < values.Count; i++)
			{
				List<int> returnVal = values[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

				for (int j = 0; j < returnVal.Count; j++)
				{
					FieldValues.Add(new Vector2(i, j), returnVal[j]);
					FieldFound.Add(new Vector2(i, j), false);
				}
			}
		}

		public bool EnterValue(int valueToEnter)
		{
			for (int i = 0; i < GridSize; i++)
			{
				for (int j = 0; j < GridSize; j++)
				{
					int value = 0;
					Vector2 fieldToCheck = new Vector2(i, j);
					bool lookForValue = FieldValues.TryGetValue(fieldToCheck, out value);

					if(lookForValue)
					{
						if(value == valueToEnter)
						{
							FieldFound[fieldToCheck] = true;

							return CheckIfBingo(fieldToCheck);
						}
					}
				}
			}

			return false;
		}

		private bool CheckIfBingo(Vector2 addedValuePosition)
		{
			int count = 0;

			//Check Horizontal
			for (int i = 0; i < GridSize; i++)
			{
				Vector2 posToCheck = new Vector2(i, addedValuePosition.Y);
				bool valueFound = false;
				bool lookForValue = FieldFound.TryGetValue(posToCheck, out valueFound);

				if(valueFound)
				{
					++count;
				}
			}

			if(count >= GridSize)
			{
				return true;
			}
			count = 0;

			//Check Vertical
			for (int i = 0; i < GridSize; i++)
			{
				Vector2 posToCheck = new Vector2(addedValuePosition.X, i);
				bool valueFound = false;
				bool lookForValue = FieldFound.TryGetValue(posToCheck, out valueFound);

				if (valueFound)
				{
					++count;
				}
			}

			if (count >= GridSize)
			{
				return true;
			}

			return false;
		}

		public int GetWinningSum()
		{
			int sum = 0;

			for (int i = 0; i < GridSize; i++)
			{
				for (int j = 0; j < GridSize; j++)
				{
					bool valueFound = true;
					Vector2 fieldToCheck = new Vector2(i, j);
					bool lookForValue = FieldFound.TryGetValue(fieldToCheck, out valueFound);
					if(lookForValue)
					{
						if(valueFound == false)
						{
							int valueInt = 0;
							lookForValue = FieldValues.TryGetValue(fieldToCheck, out valueInt);
							if(lookForValue)
							{
								sum += valueInt;
							}
						}
					}
				}
			}

			return sum;
		}
	}
}
