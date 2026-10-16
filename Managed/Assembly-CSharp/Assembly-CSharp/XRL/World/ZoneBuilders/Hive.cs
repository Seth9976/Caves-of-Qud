using System;
using Genkit;
using XRL.Core;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A9F RID: 2719
	public class Hive
	{
		// Token: 0x06006F0C RID: 28428 RVA: 0x00266C28 File Offset: 0x00264E28
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.Generate(40, 13, false, XRLCore.Core.Game.GetWorldSeed("BathHallsMaze" + Z.ZoneID));
			Z.Fill("Limestone");
			for (int i = 0; i < 40; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					if (maze.Cell[i, j].AnyOpen())
					{
						Z.GetCell(i * 2, j * 2).Clear(null, false, false, null);
					}
					if (maze.Cell[i, j].S)
					{
						Z.GetCell(i * 2, j * 2 + 1).Clear(null, false, false, null);
					}
					if (maze.Cell[i, j].E)
					{
						Z.GetCell(i * 2 + 1, j * 2).Clear(null, false, false, null);
					}
				}
			}
			foreach (Box box in Tools.GenerateBoxes(BoxGenerateOverlap.NeverOverlap, new Range(8, 12), new Range(6, 16), new Range(4, 10), new Range(9, 100), null, null))
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
