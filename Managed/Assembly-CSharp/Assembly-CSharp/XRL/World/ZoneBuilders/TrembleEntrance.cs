using System;
using Genkit;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE3 RID: 2787
	public class TrembleEntrance
	{
		// Token: 0x06006FFE RID: 28670 RVA: 0x0027950C File Offset: 0x0027770C
		public bool BuildZone(Zone Z)
		{
			Cell cell;
			if (Z.GetEmptyReachableCells(new Rect2D(5, 5, 75, 15)).Shuffle<Cell>(null).Count == 0)
			{
				cell = Z.GetCells().GetRandomElement<Cell>(null);
			}
			else
			{
				cell = Z.GetCell(40, 12);
			}
			int x = cell.X;
			int y = cell.Y;
			for (int i = y; i <= y - 4; i--)
			{
				for (int j = x - 2; j <= x + 2; j++)
				{
					Cell cell2 = Z.GetCell(j, i);
					if (cell2 != null)
					{
						cell2.Clear(null, false, false, null);
					}
				}
			}
			for (int k = x - 2; k <= x + 2; k++)
			{
				Z.GetCell(k, y - 4).AddObject("Fulcrete", null, null, null, null);
			}
			Z.GetCell(x - 2, y - 3).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x + 2, y - 3).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x - 2, y - 2).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x, y - 2).AddObject("TrembleEntranceWormhole", null, null, null, null);
			Z.GetCell(x + 2, y - 2).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x - 2, y - 1).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x - 1, y - 1).AddObject("DoorSwitch", null, null, null, null);
			Z.GetCell(x + 2, y - 1).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x - 2, y).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x - 1, y).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x, y).AddObject("Purple Security Door", null, null, null, null);
			Z.GetCell(x + 1, y).AddObject("Fulcrete", null, null, null, null);
			Z.GetCell(x + 2, y).AddObject("Fulcrete", null, null, null, null);
			return true;
		}
	}
}
