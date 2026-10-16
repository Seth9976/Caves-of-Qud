using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;

namespace Genkit
{
	// Token: 0x02000386 RID: 902
	public class RecursiveBacktrackerMaze
	{
		// Token: 0x060021F8 RID: 8696 RVA: 0x000A9B8C File Offset: 0x000A7D8C
		public static bool HasUnvisitedNeighbors(MazeCell C)
		{
			return (C.x > 0 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x - 1, C.y])) || (C.x < RecursiveBacktrackerMaze.M.Width - 1 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x + 1, C.y])) || (C.y > 0 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x, C.y - 1])) || (C.y < RecursiveBacktrackerMaze.M.Height - 1 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x, C.y + 1]));
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000A9C80 File Offset: 0x000A7E80
		public static MazeCell GetRandomUnvisitedNeighbor(MazeCell C, Random random)
		{
			List<MazeCell> list = new List<MazeCell>();
			if (C.x > 0 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x - 1, C.y]))
			{
				list.Add(RecursiveBacktrackerMaze.M.Cell[C.x - 1, C.y]);
			}
			if (C.x < RecursiveBacktrackerMaze.M.Width - 1 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x + 1, C.y]))
			{
				list.Add(RecursiveBacktrackerMaze.M.Cell[C.x + 1, C.y]);
			}
			if (C.y > 0 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x, C.y - 1]))
			{
				list.Add(RecursiveBacktrackerMaze.M.Cell[C.x, C.y - 1]);
			}
			if (C.y < RecursiveBacktrackerMaze.M.Height - 1 && !RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[C.x, C.y + 1]))
			{
				list.Add(RecursiveBacktrackerMaze.M.Cell[C.x, C.y + 1]);
			}
			return list[random.Next(0, list.Count)];
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000A9E10 File Offset: 0x000A8010
		public static void MarkOpeningBetweenCells(MazeCell C1, MazeCell C2)
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
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000A9EB4 File Offset: 0x000A80B4
		public static Maze GenerateWithHoles(int Width, int Height, bool bShow, int nHoles, bool bDefault, int seed)
		{
			Random random = new Random(seed);
			RecursiveBacktrackerMaze.CellStack.Clear();
			RecursiveBacktrackerMaze.VisitedCells.Clear();
			RecursiveBacktrackerMaze.M = new Maze(Width, Height, bDefault);
			for (int i = 0; i < nHoles; i++)
			{
				int num = random.Next(0, Width);
				int num2 = random.Next(0, Height);
				if (!RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[num, num2]))
				{
					RecursiveBacktrackerMaze.VisitedCells.Add(RecursiveBacktrackerMaze.M.Cell[num, num2], true);
				}
			}
			int num3 = random.Next(0, Width);
			int num4 = random.Next(0, Height);
			while (RecursiveBacktrackerMaze.VisitedCells.ContainsKey(RecursiveBacktrackerMaze.M.Cell[num3, num4]))
			{
				num3 = random.Next(0, Width);
				num4 = random.Next(0, Height);
			}
			RecursiveBacktrackerMaze.CellStack.Push(RecursiveBacktrackerMaze.M.Cell[num3, num4]);
			MazeCell mazeCell = RecursiveBacktrackerMaze.M.Cell[num3, num4];
			for (;;)
			{
				if (!RecursiveBacktrackerMaze.VisitedCells.ContainsKey(mazeCell))
				{
					RecursiveBacktrackerMaze.VisitedCells.Add(mazeCell, true);
				}
				if (RecursiveBacktrackerMaze.HasUnvisitedNeighbors(mazeCell))
				{
					RecursiveBacktrackerMaze.CellStack.Push(mazeCell);
					MazeCell randomUnvisitedNeighbor = RecursiveBacktrackerMaze.GetRandomUnvisitedNeighbor(mazeCell, random);
					RecursiveBacktrackerMaze.MarkOpeningBetweenCells(randomUnvisitedNeighbor, mazeCell);
					RecursiveBacktrackerMaze.CellStack.Push(mazeCell);
					if (bShow)
					{
						RecursiveBacktrackerMaze.M.Test(false);
					}
					mazeCell = randomUnvisitedNeighbor;
				}
				else
				{
					if (RecursiveBacktrackerMaze.CellStack.Count == 0)
					{
						break;
					}
					mazeCell = RecursiveBacktrackerMaze.CellStack.Pop();
				}
			}
			RecursiveBacktrackerMaze.CellStack.Clear();
			RecursiveBacktrackerMaze.VisitedCells.Clear();
			return RecursiveBacktrackerMaze.M;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000AA047 File Offset: 0x000A8247
		public static Maze Generate(int Width, int Height, bool bShow, string Seed)
		{
			return RecursiveBacktrackerMaze.Generate(Width, Height, bShow, XRLCore.Core.Game.GetWorldSeed(Seed));
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000AA064 File Offset: 0x000A8264
		public static Maze Generate(int Width, int Height, bool bShow, int Seed)
		{
			Random random = new Random(Seed);
			RecursiveBacktrackerMaze.M = new Maze(Width, Height, false);
			int num = Stat.Random(0, Width - 1);
			int num2 = Stat.Random(0, Height - 1);
			RecursiveBacktrackerMaze.CellStack.Clear();
			RecursiveBacktrackerMaze.VisitedCells.Clear();
			RecursiveBacktrackerMaze.CellStack.Push(RecursiveBacktrackerMaze.M.Cell[num, num2]);
			MazeCell mazeCell = RecursiveBacktrackerMaze.M.Cell[num, num2];
			Maze m;
			try
			{
				for (;;)
				{
					if (!RecursiveBacktrackerMaze.VisitedCells.ContainsKey(mazeCell))
					{
						RecursiveBacktrackerMaze.VisitedCells.Add(mazeCell, true);
					}
					if (RecursiveBacktrackerMaze.HasUnvisitedNeighbors(mazeCell))
					{
						RecursiveBacktrackerMaze.CellStack.Push(mazeCell);
						MazeCell randomUnvisitedNeighbor = RecursiveBacktrackerMaze.GetRandomUnvisitedNeighbor(mazeCell, random);
						RecursiveBacktrackerMaze.MarkOpeningBetweenCells(randomUnvisitedNeighbor, mazeCell);
						RecursiveBacktrackerMaze.CellStack.Push(mazeCell);
						if (bShow)
						{
							RecursiveBacktrackerMaze.M.Test(false);
						}
						mazeCell = randomUnvisitedNeighbor;
					}
					else
					{
						if (RecursiveBacktrackerMaze.CellStack.Count == 0)
						{
							break;
						}
						mazeCell = RecursiveBacktrackerMaze.CellStack.Pop();
					}
				}
				m = RecursiveBacktrackerMaze.M;
			}
			finally
			{
				RecursiveBacktrackerMaze.CellStack.Clear();
				RecursiveBacktrackerMaze.VisitedCells.Clear();
			}
			return m;
		}

		// Token: 0x04001101 RID: 4353
		public static Stack<MazeCell> CellStack = new Stack<MazeCell>();

		// Token: 0x04001102 RID: 4354
		public static Dictionary<MazeCell, bool> VisitedCells = new Dictionary<MazeCell, bool>();

		// Token: 0x04001103 RID: 4355
		public static Maze M;
	}
}
