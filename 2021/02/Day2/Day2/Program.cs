using System;
using System.Numerics;
using System.Windows;

namespace Day2
{
	class Program
	{
		static void Main(string[] args)
		{

			Vector2[] values = ReadFile();
			Vector3 finalVector = new Vector3(0, 0, 0);

			for (int i = 0; i < values.Length; i++)
			{

				if(values[i].X != 0) // X = HORIZONTAL, Y = AIM, Z = DEPTH
				{
					float val = finalVector.Z + (values[i].X * finalVector.Y);
					finalVector.Z = val;
				}

				finalVector.X += values[i].X;
				finalVector.Y += values[i].Y;
			}

			float product = finalVector.X * finalVector.Z;

			Console.WriteLine(product);
		}

		static private Vector2[] ReadFile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"F:\Advent of code\2021\2\Files\Final.txt"); //Runs the test code
			Vector2[] linesVector = new Vector2[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Contains("forward"))
				{
					linesVector[i] = new Vector2(int.Parse(lines[i].Substring(lines[i].Length - 1, 1)), 0f);
				}
				else if(lines[i].Contains("down"))
				{
					linesVector[i] = new Vector2(0, int.Parse(lines[i].Substring(lines[i].Length - 1, 1)));
				}
				else if(lines[i].Contains("up"))
				{
					linesVector[i] = new Vector2(0f, -int.Parse(lines[i].Substring(lines[i].Length - 1, 1)));
				}
			}

			return linesVector;
		}
	}
}
