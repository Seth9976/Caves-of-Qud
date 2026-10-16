using System;
using Genkit;
using XRL.Core;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B14 RID: 2836
	public class WideHive
	{
		// Token: 0x060071B4 RID: 29108 RVA: 0x002A8D3C File Offset: 0x002A6F3C
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.Generate(20, 6, false, XRLCore.Core.Game.GetWorldSeed("BathHallsMaze"));
			Z.Fill("Limestone");
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 20; j++)
				{
					int num = j * 4 + 1;
					int num2 = i * 4 + 1;
					if (maze.Cell[j, i].AnyOpen())
					{
						Z.GetCell(num, num2).Clear(null, false, false, null);
						Z.GetCell(num + 1, num2).Clear(null, false, false, null);
						Z.GetCell(num, num2 + 1).Clear(null, false, false, null);
						Z.GetCell(num + 1, num2 + 1).Clear(null, false, false, null);
					}
					if (maze.Cell[j, i].S)
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
					if (maze.Cell[j, i].E)
					{
						Z.GetCell(num + 2, num2).Clear(null, false, false, null);
						Z.GetCell(num + 3, num2).Clear(null, false, false, null);
						Z.GetCell(num + 2, num2 + 1).Clear(null, false, false, null);
						Z.GetCell(num + 3, num2 + 1).Clear(null, false, false, null);
					}
				}
			}
			foreach (Box box in Tools.GenerateBoxes(BoxGenerateOverlap.NeverOverlap, new Range(8, 16), new Range(6, 12), new Range(4, 10), new Range(9, 100), null, null))
			{
				Box box2 = box.Grow(-1);
				for (int k = box2.y1; k <= box2.y2; k++)
				{
					for (int l = box2.x1; l <= box2.x2; l++)
					{
						Z.GetCell(l, k).Clear(null, false, false, null);
					}
				}
			}
			return true;
		}
	}
}
