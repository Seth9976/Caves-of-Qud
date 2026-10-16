using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A88 RID: 2696
	public class Torchposts
	{
		// Token: 0x06006E98 RID: 28312 RVA: 0x0025E7C0 File Offset: 0x0025C9C0
		public bool BuildZone(Zone Z)
		{
			int num = Stat.Random(1, 78);
			int num2 = Stat.Random(0, 1) * 23 + 1;
			for (int i = 0; i < 80; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					Cell cell = Z.GetCell(i, j);
					if (cell.HasObjectWithPart("StairsUp") || cell.HasObjectWithPart("StairsDown"))
					{
						num = i;
						num2 = j;
						goto IL_0068;
					}
				}
			}
			IL_0068:
			FindPath findPath = new FindPath(Z, num, num2, Z, Stat.Random(0, 1) * 77 + 1, Stat.Random(1, 23), false, true, null, true);
			if (!findPath.Usable)
			{
				return true;
			}
			int num3 = 0;
			foreach (Cell cell2 in findPath.Steps)
			{
				num3--;
				if (num3 <= 0)
				{
					List<Cell> localAdjacentCells = cell2.GetLocalAdjacentCells();
					if (localAdjacentCells.Count >= 7)
					{
						localAdjacentCells[2].AddObject(GameObjectFactory.Factory.CreateObject("Torchpost"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						localAdjacentCells[6].AddObject(GameObjectFactory.Factory.CreateObject("Torchpost"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					num3 += Stat.Random(3, 5);
				}
			}
			return true;
		}
	}
}
