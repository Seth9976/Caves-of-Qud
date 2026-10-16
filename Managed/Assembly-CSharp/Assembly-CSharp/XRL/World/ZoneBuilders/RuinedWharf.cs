using System;
using System.Collections.Generic;
using Genkit;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A4C RID: 2636
	public class RuinedWharf
	{
		// Token: 0x06006DE5 RID: 28133 RVA: 0x002549FC File Offset: 0x00252BFC
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.Generate(20, 6, false, "WharfMaze" + Z.ZoneID);
			Z.Fill("Limestone");
			for (int i = 0; i < 20; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					int num = i * 4 + 1;
					int num2 = j * 4 + 1;
					if (maze.Cell[i, j].AnyOpen())
					{
						Z.GetCell(num, num2).Clear(null, false, false, null);
						Z.GetCell(num + 1, num2).Clear(null, false, false, null);
						Z.GetCell(num, num2 + 1).Clear(null, false, false, null);
						Z.GetCell(num + 1, num2 + 1).Clear(null, false, false, null);
					}
					if (maze.Cell[i, j].S)
					{
						if (num2 + 2 < Z.Height)
						{
							Z.GetCell(num, num2 + 2).Clear(null, false, false, null);
							Z.GetCell(num + 1, num2 + 2).Clear(null, false, false, null);
						}
						if (num2 + 3 < Z.Height)
						{
							Z.GetCell(num, num2 + 3).Clear(null, false, false, null);
							Z.GetCell(num + 1, num2 + 3).Clear(null, false, false, null);
						}
					}
					if (maze.Cell[i, j].E)
					{
						Z.GetCell(num + 2, num2).Clear(null, false, false, null);
						Z.GetCell(num + 3, num2).Clear(null, false, false, null);
						Z.GetCell(num + 2, num2 + 1).Clear(null, false, false, null);
						Z.GetCell(num + 3, num2 + 1).Clear(null, false, false, null);
					}
				}
			}
			List<Box> list = Tools.GenerateBoxes(BoxGenerateOverlap.NeverOverlap, new Range(8, 16), new Range(6, 12), new Range(4, 10), new Range(9, 100), null, null);
			foreach (Box box in list)
			{
				Box box2 = box.Grow(-1);
				for (int k = box2.x1; k <= box2.x2; k++)
				{
					for (int l = box2.y1; l <= box2.y2; l++)
					{
						Z.GetCell(k, l).Clear(null, false, false, null);
					}
				}
			}
			for (int m = 0; m < 4; m++)
			{
				for (int n = 0; n < 2; n++)
				{
					for (int num3 = m * 12 + 4; num3 <= m * 8 + 12; num3++)
					{
						for (int num4 = n * 4 + 4; num4 <= n * 4 + 8; num4++)
						{
							Z.GetCell(num3, num4).Clear(null, false, false, null);
						}
					}
				}
			}
			Z.BuildReachableMap(list[0].x1 + 1, list[0].y1 + 1);
			return true;
		}
	}
}
