using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day5
{
	class Program
	{

		private static VentField Field;
		static void Main(string[] args)
		{
			Field = new VentField();
			List<Tuple<Vector2, Vector2>> values = ReadFile();
			

			Console.WriteLine(Field.GetIntersections(2));
		}

		/// <summary>
		/// reads the file
		/// </summary>
		/// <returns></returns>
		static private List<Tuple<Vector2, Vector2>> ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\AdventOfCode\2021\05\Files\Final.txt");
			List<Tuple<Vector2, Vector2>> returnVal = new List<Tuple<Vector2, Vector2>>();
			for (int i = 0; i < lines.Length; i++)
			{
				string firstValue = lines[i].Substring(0, lines[i].IndexOf('-'));
				string secondValue = lines[i].Substring(lines[i].IndexOf('>') + 1);
				List<int> intValues = firstValue.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
				Vector2 startPoint = new Vector2(intValues[0], intValues[1]);
				intValues = secondValue.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
				Vector2 endPoint = new Vector2(intValues[0], intValues[1]);

				if(startPoint.X == endPoint.X || startPoint.Y == endPoint.Y)
				{
					returnVal.Add(new Tuple<Vector2, Vector2>(startPoint, endPoint));
					Field.EnterValue(startPoint, endPoint);
				}
				else if(IsDiagonal(startPoint, endPoint))
				{
					returnVal.Add(new Tuple<Vector2, Vector2>(startPoint, endPoint));
					Field.EnterValue(startPoint, endPoint, true);
				}
			}

			return returnVal;
		}

		static private bool IsDiagonal(Vector2 beginPoint, Vector2 endPoint)
		{
			int xDifference = Math.Abs((int)(beginPoint.X - endPoint.X));
			int yDifference = Math.Abs((int)(beginPoint.Y - endPoint.Y));

			return xDifference == yDifference;
		}
	}

	class VentField
	{
		public Dictionary<Vector2, int> FieldValues;
		public int MaxXValue = int.MinValue;
		public int MaxYValue = int.MinValue;
		public int MinXValue = int.MaxValue;
		public int MinYValue = int.MaxValue;

		public VentField()
		{
			FieldValues = new Dictionary<Vector2, int>();
		}

		public void EnterValue(Vector2 startPoint, Vector2 endPoint, bool diagonal = false)
		{
			if(diagonal == false)
			{
				if (startPoint.X == endPoint.X)
				{
					Vector2 beginVal = startPoint.Y < endPoint.Y ? startPoint : endPoint;
					Vector2 endVal = startPoint.Y < endPoint.Y ? endPoint : startPoint;

					//move over Y
					for (int i = (int)beginVal.Y; i < (int)endVal.Y + 1; i++)
					{
						int value = 0;
						Vector2 fieldToCheck = new Vector2(beginVal.X, i);

						//update min-max values first
						if (fieldToCheck.X < MinXValue) MinXValue = (int)fieldToCheck.X;
						if (fieldToCheck.X > MaxXValue) MaxXValue = (int)fieldToCheck.X;
						if (fieldToCheck.Y < MinYValue) MinYValue = (int)fieldToCheck.Y;
						if (fieldToCheck.Y > MaxYValue) MaxYValue = (int)fieldToCheck.Y;

						//actually adding it
						bool lookForValue = FieldValues.TryGetValue(fieldToCheck, out value);

						if (lookForValue)
						{
							//value already exists, increment
							FieldValues[fieldToCheck]++;
						}
						else
						{
							//not seen yet, add with value 1
							FieldValues.Add(fieldToCheck, 1);
						}
					}
				}
				else
				{
					Vector2 beginVal = startPoint.X < endPoint.X ? startPoint : endPoint;
					Vector2 endVal = startPoint.X < endPoint.X ? endPoint : startPoint;
					//move over X
					for (int i = (int)beginVal.X; i < (int)endVal.X + 1; i++)
					{
						int value = 0;
						Vector2 fieldToCheck = new Vector2(i, beginVal.Y);

						//update min-max values first
						if (fieldToCheck.X < MinXValue) MinXValue = (int)fieldToCheck.X;
						if (fieldToCheck.X > MaxXValue) MaxXValue = (int)fieldToCheck.X;
						if (fieldToCheck.Y < MinYValue) MinYValue = (int)fieldToCheck.Y;
						if (fieldToCheck.Y > MaxYValue) MaxYValue = (int)fieldToCheck.Y;

						//actually adding it
						bool lookForValue = FieldValues.TryGetValue(fieldToCheck, out value);

						if (lookForValue)
						{
							//value already exists, increment
							FieldValues[fieldToCheck]++;
						}
						else
						{
							//not seen yet, add with value 1
							FieldValues.Add(fieldToCheck, 1);
						}
					}
				}
			}
			else
			{
				int length = Math.Abs((int)(endPoint.X - startPoint.X));
				bool incrementX = startPoint.X < endPoint.X ? true : false;
				bool incrementY = startPoint.Y < endPoint.Y ? true : false;

				//move over Y
				for (int i = 0; i < length + 1; i++)
				{
					int value = 0;
					Vector2 fieldToCheck = new Vector2();
					if (incrementX)
						fieldToCheck.X = startPoint.X + i;
					else
						fieldToCheck.X = startPoint.X - i;
					if (incrementY)
						fieldToCheck.Y = startPoint.Y + i;
					else
						fieldToCheck.Y = startPoint.Y - i;

					//update min-max values first
					if (fieldToCheck.X < MinXValue) MinXValue = (int)fieldToCheck.X;
					if (fieldToCheck.X > MaxXValue) MaxXValue = (int)fieldToCheck.X;
					if (fieldToCheck.Y < MinYValue) MinYValue = (int)fieldToCheck.Y;
					if (fieldToCheck.Y > MaxYValue) MaxYValue = (int)fieldToCheck.Y;

					//actually adding it
					bool lookForValue = FieldValues.TryGetValue(fieldToCheck, out value);

					if (lookForValue)
					{
						//value already exists, increment
						FieldValues[fieldToCheck]++;
					}
					else
					{
						//not seen yet, add with value 1
						FieldValues.Add(fieldToCheck, 1);
					}
				}
			}
			
		}

		public int GetIntersections(int minimumAmount)
		{
			int count = 0;

			for (int i = MinXValue; i < MaxXValue + 1; i++)
			{
				for (int j = MinYValue; j < MaxYValue + 1; j++)
				{
					int value = 0;
					Vector2 fieldToCheck = new Vector2(i, j);
					bool lookForValue = FieldValues.TryGetValue(fieldToCheck, out value); 
	 
					if (lookForValue)
					{
						if(value >= minimumAmount)
						{
							count++;
						}
					}
				}
			}


			return count;
		}

		
	}
}
