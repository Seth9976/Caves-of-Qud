using System;
using Genkit;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A43 RID: 2627
	public class BathHalls
	{
		// Token: 0x06006DD0 RID: 28112 RVA: 0x00251AE8 File Offset: 0x0024FCE8
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.Generate(11, 3, true, XRLCore.Core.Game.GetWorldSeed("BathHallsMaze" + Z.ZoneID));
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
			Z.Fill("Limestone");
			bool[,] array = new bool[10, 3];
			for (int j = 0; j < 3; j++)
			{
				for (int k = 0; k < 10; k++)
				{
					array[k, j] = false;
				}
			}
			int num3 = Stat.Random(1, 3);
			int l = 0;
			while (l < num3)
			{
				int num4 = Stat.Random(0, 9);
				int num5 = Stat.Random(0, 2);
				if (!array[num4, num5])
				{
					array[num4, num5] = true;
					l++;
				}
			}
			for (int m = 0; m < 10; m++)
			{
				for (int n = 0; n < 3; n++)
				{
					int num6 = m * 8;
					int num7 = n * 8;
					if (array[m, n])
					{
						Z.ClearBox(new Box(num6, num7, num6 + 7, num7 + 7), null);
						Z.FillBox(new Box(num6 + 1, num7 + 1, num6 + 6, num7 + 6), "ConvalessenceDeepPool", false);
						Z.GetCell(num6 + 2, num7 + 2).AddObject("Fulcrete", null, null, null, null);
						Z.GetCell(num6 + 2, num7 + 5).AddObject("Fulcrete", null, null, null, null);
						Z.GetCell(num6 + 5, num7 + 2).AddObject("Fulcrete", null, null, null, null);
						Z.GetCell(num6 + 5, num7 + 5).AddObject("Fulcrete", null, null, null, null);
					}
					else
					{
						if (maze.Cell[m, n].N)
						{
							Z.ClearBox(new Box(num6 + 1, num7, num6 + 6, num7 + 6), null);
							Z.GetCell(num6 + 2, num7 + 2).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 5, num7 + 2).AddObject("Fulcrete", null, null, null, null);
						}
						if (maze.Cell[m, n].S)
						{
							Z.ClearBox(new Box(num6 + 1, num7 + 1, num6 + 6, num7 + 7), null);
							Z.GetCell(num6 + 2, num7 + 5).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 5, num7 + 5).AddObject("Fulcrete", null, null, null, null);
						}
						if (maze.Cell[m, n].E)
						{
							Z.ClearBox(new Box(num6 + 1, num7 + 1, num6 + 7, num7 + 6), null);
							Z.GetCell(num6 + 2, num7 + 2).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 2, num7 + 5).AddObject("Fulcrete", null, null, null, null);
						}
						if (maze.Cell[m, n].W)
						{
							Z.ClearBox(new Box(num6, num7 + 1, num6 + 6, num7 + 6), null);
							Z.GetCell(num6 + 5, num7 + 2).AddObject("Fulcrete", null, null, null, null);
							Z.GetCell(num6 + 5, num7 + 5).AddObject("Fulcrete", null, null, null, null);
						}
					}
				}
			}
			for (int num8 = 0; num8 < 10; num8++)
			{
				for (int num9 = 0; num9 < 3; num9++)
				{
					int num10 = num8 * 8;
					int num11 = num9 * 8;
					if (array[num8, num9])
					{
						Z.FillBox(new Box(num10 + 1, num11 + 1, num10 + 6, num11 + 6), "ConvalessenceDeepPool", false);
					}
					else
					{
						if (Stat.Random(1, 100) <= 50 && maze.Cell[num8, num9].N)
						{
							Z.FillBox(new Box(num10 + 3, num11, num10 + 4, num11 + 6), "ConvalessenceDeepPool", false);
						}
						if (Stat.Random(1, 100) <= 50 && maze.Cell[num8, num9].S)
						{
							Z.FillBox(new Box(num10 + 3, num11 + 1, num10 + 4, num11 + 7), "ConvalessenceDeepPool", false);
						}
						if (Stat.Random(1, 100) <= 50 && maze.Cell[num8, num9].E)
						{
							Z.FillBox(new Box(num10 + 1, num11 + 3, num10 + 7, num11 + 4), "ConvalessenceDeepPool", false);
						}
						if (Stat.Random(1, 100) <= 50 && maze.Cell[num8, num9].W)
						{
							Z.FillBox(new Box(num10, num11 + 3, num10 + 6, num11 + 4), "ConvalessenceDeepPool", false);
						}
					}
				}
			}
			return true;
		}
	}
}
