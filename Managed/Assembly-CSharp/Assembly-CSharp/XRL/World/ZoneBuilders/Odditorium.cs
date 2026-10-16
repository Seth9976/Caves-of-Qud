using System;
using Genkit;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A4A RID: 2634
	public class Odditorium
	{
		// Token: 0x06006DE1 RID: 28129 RVA: 0x00253B3C File Offset: 0x00251D3C
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.GenerateWithHoles(20, 6, false, 30, true, XRLCore.Core.Game.GetWorldSeed("OdditorimMaze" + Z.ZoneID));
			for (int i = 0; i < 15; i++)
			{
				int num = Stat.Random(1, 9);
				int num2 = Stat.Random(0, 2);
				if (Stat.Random(0, 100) <= 25)
				{
					maze.Cell[num, num2].N = true;
				}
				if (Stat.Random(0, 100) <= 25)
				{
					maze.Cell[num, num2].S = true;
				}
				if (Stat.Random(0, 100) <= 25)
				{
					maze.Cell[num, num2].E = true;
				}
				if (Stat.Random(0, 100) <= 25)
				{
					maze.Cell[num, num2].W = true;
				}
			}
			string text = "Marble";
			Z.Fill(text);
			bool[,] array = new bool[20, 6];
			for (int j = 0; j < 20; j++)
			{
				for (int k = 0; k < 6; k++)
				{
					array[j, k] = false;
				}
			}
			int num3 = Stat.Random(3, 6);
			int l = 0;
			while (l < num3)
			{
				int num4 = Stat.Random(0, 19);
				int num5 = Stat.Random(0, 5);
				if (!array[num4, num5])
				{
					array[num4, num5] = true;
					l++;
				}
			}
			for (int m = 0; m < 20; m++)
			{
				for (int n = 0; n < 6; n++)
				{
					int num6 = m * 4;
					int num7 = n * 4;
					Z.ClearBox(new Box(num6, num7, num6 + 3, num7 + 3), null);
					if (maze.Cell[m, n].AnyOpen())
					{
						if (!maze.Cell[m, n].N)
						{
							Z.GetCell(num6, num7).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 1, num7).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 2, num7).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 3, num7).AddObject(text, null, null, null, null);
						}
						if (!maze.Cell[m, n].S)
						{
							Z.GetCell(num6, num7 + 3).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 1, num7 + 3).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 2, num7 + 3).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 3, num7 + 3).AddObject(text, null, null, null, null);
						}
						if (!maze.Cell[m, n].E)
						{
							Z.GetCell(num6 + 3, num7).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 3, num7 + 1).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 3, num7 + 2).AddObject(text, null, null, null, null);
							Z.GetCell(num6 + 3, num7 + 3).AddObject(text, null, null, null, null);
						}
						if (!maze.Cell[m, n].W)
						{
							Z.GetCell(num6, num7).AddObject(text, null, null, null, null);
							Z.GetCell(num6, num7 + 1).AddObject(text, null, null, null, null);
							Z.GetCell(num6, num7 + 2).AddObject(text, null, null, null, null);
							Z.GetCell(num6, num7 + 3).AddObject(text, null, null, null, null);
						}
						if (maze.Cell[m, n].N && maze.Cell[m, n].W)
						{
							Z.GetCell(num6, num7).AddObject(text, null, null, null, null);
						}
						if (maze.Cell[m, n].N && maze.Cell[m, n].E)
						{
							Z.GetCell(num6 + 3, num7).AddObject(text, null, null, null, null);
						}
						if (maze.Cell[m, n].S && maze.Cell[m, n].W)
						{
							Z.GetCell(num6, num7 + 3).AddObject(text, null, null, null, null);
						}
						if (maze.Cell[m, n].S && maze.Cell[m, n].E)
						{
							Z.GetCell(num6 + 3, num7 + 3).AddObject(text, null, null, null, null);
						}
					}
				}
			}
			Z.ClearReachableMap();
			for (int num8 = 5; num8 < 75; num8++)
			{
				for (int num9 = 5; num9 < 15; num9++)
				{
					if (!Z.GetCell(num8, num9).IsSolid())
					{
						Z.BuildReachableMap(num8, num9);
						return true;
					}
				}
			}
			return true;
		}
	}
}
