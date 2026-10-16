using System;
using System.Collections.Generic;

namespace Genkit
{
	// Token: 0x02000385 RID: 901
	public class RecursiveBacktrackerMaze3D
	{
		// Token: 0x060021F2 RID: 8690 RVA: 0x000A9314 File Offset: 0x000A7514
		public static bool HasUnvisitedNeighbors(MazeCell3D C)
		{
			return (C.x > 0 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)(C.x - 1), (int)C.y, (int)C.z])) || ((int)C.x < RecursiveBacktrackerMaze3D.M.Width - 1 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)(C.x + 1), (int)C.y, (int)C.z])) || (C.y > 0 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)(C.y - 1), (int)C.z])) || ((int)C.y < RecursiveBacktrackerMaze3D.M.Height - 1 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)(C.y + 1), (int)C.z])) || (C.z > 0 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)C.y, (int)(C.z - 1)])) || ((int)C.z < RecursiveBacktrackerMaze3D.M.Depth - 1 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)C.y, (int)(C.z + 1)]));
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000A94A0 File Offset: 0x000A76A0
		public static MazeCell3D GetRandomUnvisitedNeighbor(MazeCell3D C)
		{
			List<MazeCell3D> list = new List<MazeCell3D>();
			if (C.x > 0 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)(C.x - 1), (int)C.y, (int)C.z]))
			{
				list.Add(RecursiveBacktrackerMaze3D.M.Cell[(int)(C.x - 1), (int)C.y, (int)C.z]);
			}
			if ((int)C.x < RecursiveBacktrackerMaze3D.M.Width - 1 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)(C.x + 1), (int)C.y, (int)C.z]))
			{
				list.Add(RecursiveBacktrackerMaze3D.M.Cell[(int)(C.x + 1), (int)C.y, (int)C.z]);
			}
			if (C.y > 0 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)(C.y - 1), (int)C.z]))
			{
				list.Add(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)(C.y - 1), (int)C.z]);
			}
			if ((int)C.y < RecursiveBacktrackerMaze3D.M.Height - 1 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)(C.y + 1), (int)C.z]))
			{
				list.Add(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)(C.y + 1), (int)C.z]);
			}
			if (C.z > 0 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)C.y, (int)(C.z - 1)]))
			{
				list.Add(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)C.y, (int)(C.z - 1)]);
			}
			if ((int)C.z < RecursiveBacktrackerMaze3D.M.Depth - 1 && !RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)C.y, (int)(C.z + 1)]))
			{
				list.Add(RecursiveBacktrackerMaze3D.M.Cell[(int)C.x, (int)C.y, (int)(C.z + 1)]);
			}
			return list[RecursiveBacktrackerMaze3D.R.Next(0, list.Count - 1)];
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000A9734 File Offset: 0x000A7934
		public static void MarkOpeningBetweenCells(MazeCell3D C1, MazeCell3D C2)
		{
			if (C1.z == C2.z)
			{
				if (C1.x == C2.x)
				{
					if (C1.y + 1 == C2.y)
					{
						C1.S = true;
						C2.N = true;
						return;
					}
					if (C1.y - 1 == C2.y)
					{
						C1.N = true;
						C2.S = true;
						return;
					}
				}
				else if (C1.y == C2.y)
				{
					if (C1.x + 1 == C2.x)
					{
						C1.E = true;
						C2.W = true;
						return;
					}
					if (C1.x - 1 == C2.x)
					{
						C1.W = true;
						C2.E = true;
					}
				}
				return;
			}
			if (C1.z > C2.z)
			{
				C1.U = true;
				C2.D = true;
				return;
			}
			C1.D = true;
			C2.U = true;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000A9814 File Offset: 0x000A7A14
		public static Maze3D Generate(int Seed, int Width, int Height, int Depth, bool bShow, int AddUpChance = 0, int AddDownChance = 0, int AddNChance = 0, int AddSChance = 0, int AddEChance = 0, int AddWChance = 0)
		{
			RecursiveBacktrackerMaze3D.R = new Random(Seed);
			RecursiveBacktrackerMaze3D.M = new Maze3D(Width, Height, Depth, false);
			int num = RecursiveBacktrackerMaze3D.R.Next(0, Width - 1);
			int num2 = RecursiveBacktrackerMaze3D.R.Next(0, Height - 1);
			int num3 = RecursiveBacktrackerMaze3D.R.Next(0, Depth - 1);
			RecursiveBacktrackerMaze3D.CellStack.Clear();
			RecursiveBacktrackerMaze3D.VisitedCells.Clear();
			RecursiveBacktrackerMaze3D.CellStack.Push(RecursiveBacktrackerMaze3D.M.Cell[num, num2, num3]);
			MazeCell3D mazeCell3D = RecursiveBacktrackerMaze3D.M.Cell[num, num2, num3];
			for (;;)
			{
				if (!RecursiveBacktrackerMaze3D.VisitedCells.ContainsKey(mazeCell3D))
				{
					RecursiveBacktrackerMaze3D.VisitedCells.Add(mazeCell3D, true);
				}
				if (RecursiveBacktrackerMaze3D.HasUnvisitedNeighbors(mazeCell3D))
				{
					RecursiveBacktrackerMaze3D.CellStack.Push(mazeCell3D);
					MazeCell3D randomUnvisitedNeighbor = RecursiveBacktrackerMaze3D.GetRandomUnvisitedNeighbor(mazeCell3D);
					RecursiveBacktrackerMaze3D.MarkOpeningBetweenCells(randomUnvisitedNeighbor, mazeCell3D);
					RecursiveBacktrackerMaze3D.CellStack.Push(mazeCell3D);
					mazeCell3D = randomUnvisitedNeighbor;
				}
				else
				{
					if (RecursiveBacktrackerMaze3D.CellStack.Count == 0)
					{
						break;
					}
					mazeCell3D = RecursiveBacktrackerMaze3D.CellStack.Pop();
				}
			}
			if (bShow)
			{
				RecursiveBacktrackerMaze3D.M.Test(true);
			}
			RecursiveBacktrackerMaze3D.VisitedCells.Clear();
			RecursiveBacktrackerMaze3D.CellStack.Clear();
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					for (int k = 0; k < Depth; k++)
					{
						if (RecursiveBacktrackerMaze3D.R.Next(1, 100) <= AddUpChance)
						{
							RecursiveBacktrackerMaze3D.M.Cell[i, j, k].U = true;
							if (k > 0)
							{
								RecursiveBacktrackerMaze3D.M.Cell[i, j, k - 1].D = true;
							}
						}
						if (RecursiveBacktrackerMaze3D.R.Next(1, 100) <= AddDownChance)
						{
							RecursiveBacktrackerMaze3D.M.Cell[i, j, k].D = true;
							if (k < Depth - 1)
							{
								RecursiveBacktrackerMaze3D.M.Cell[i, j, k + 1].U = true;
							}
						}
						if (RecursiveBacktrackerMaze3D.R.Next(1, 100) <= AddNChance)
						{
							RecursiveBacktrackerMaze3D.M.Cell[i, j, k].N = true;
							if (j > 0)
							{
								RecursiveBacktrackerMaze3D.M.Cell[i, j - 1, k].S = true;
							}
						}
						if (RecursiveBacktrackerMaze3D.R.Next(1, 100) <= AddSChance)
						{
							RecursiveBacktrackerMaze3D.M.Cell[i, j, k].S = true;
							if (j < Height - 1)
							{
								RecursiveBacktrackerMaze3D.M.Cell[i, j + 1, k].N = true;
							}
						}
						if (RecursiveBacktrackerMaze3D.R.Next(1, 100) <= AddEChance)
						{
							RecursiveBacktrackerMaze3D.M.Cell[i, j, k].E = true;
							if (i < Width - 1)
							{
								RecursiveBacktrackerMaze3D.M.Cell[i + 1, j, k].W = true;
							}
						}
						if (RecursiveBacktrackerMaze3D.R.Next(1, 100) <= AddWChance)
						{
							RecursiveBacktrackerMaze3D.M.Cell[i, j, k].W = true;
							if (i > 0)
							{
								RecursiveBacktrackerMaze3D.M.Cell[i - 1, j, k].E = true;
							}
						}
					}
				}
			}
			RecursiveBacktrackerMaze3D.CellStack.Clear();
			RecursiveBacktrackerMaze3D.VisitedCells.Clear();
			return RecursiveBacktrackerMaze3D.M;
		}

		// Token: 0x040010FD RID: 4349
		public static Stack<MazeCell3D> CellStack = new Stack<MazeCell3D>();

		// Token: 0x040010FE RID: 4350
		public static Dictionary<MazeCell3D, bool> VisitedCells = new Dictionary<MazeCell3D, bool>();

		// Token: 0x040010FF RID: 4351
		public static Maze3D M;

		// Token: 0x04001100 RID: 4352
		private static Random R = null;
	}
}
