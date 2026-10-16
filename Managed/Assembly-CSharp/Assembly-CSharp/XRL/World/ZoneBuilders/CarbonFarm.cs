using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A46 RID: 2630
	public class CarbonFarm
	{
		// Token: 0x06006DD6 RID: 28118 RVA: 0x0025278C File Offset: 0x0025098C
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.Generate(10, 3, false, "CarbonFarm");
			for (int i = 0; i < 10; i += 2)
			{
				for (int j = 0; j < 3; j++)
				{
					maze.Cell[i, j].N = true;
					maze.Cell[i, j].S = true;
				}
			}
			for (int k = 0; k < 15; k++)
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
			for (int l = 0; l < 10; l++)
			{
				for (int m = 0; m < 3; m++)
				{
					array[l, m] = false;
				}
			}
			int num3 = Stat.Random(1, 3);
			int n = 0;
			while (n < num3)
			{
				int num4 = Stat.Random(0, 9);
				int num5 = Stat.Random(0, 2);
				if (!array[num4, num5])
				{
					array[num4, num5] = true;
					n++;
				}
			}
			for (int num6 = 0; num6 < 10; num6++)
			{
				for (int num7 = 0; num7 < 3; num7++)
				{
					int num8 = num6 * 8;
					int num9 = num7 * 8;
					if (array[num6, num7])
					{
						Z.ClearBox(new Box(num8, num9, num8 + 7, num9 + 7), null);
					}
					else
					{
						if (maze.Cell[num6, num7].N)
						{
							Z.ClearBox(new Box(num8 + 1, num9, num8 + 6, num9 + 6), null);
						}
						if (maze.Cell[num6, num7].S)
						{
							Z.ClearBox(new Box(num8 + 1, num9 + 1, num8 + 6, num9 + 7), null);
						}
						if (maze.Cell[num6, num7].E)
						{
							Z.ClearBox(new Box(num8 + 1, num9 + 1, num8 + 7, num9 + 6), null);
						}
						if (maze.Cell[num6, num7].W)
						{
							Z.ClearBox(new Box(num8, num9 + 1, num8 + 6, num9 + 6), null);
						}
					}
				}
			}
			Z.ClearReachableMap();
			for (int num10 = 5; num10 < 75; num10++)
			{
				for (int num11 = 5; num11 < 15; num11++)
				{
					if (!Z.GetCell(num10, num11).IsSolid())
					{
						Z.BuildReachableMap(num10, num11);
						return true;
					}
				}
			}
			return true;
		}
	}
}
