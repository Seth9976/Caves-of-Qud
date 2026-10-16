using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC1 RID: 2753
	public class PlaceWedgeBuilder4
	{
		// Token: 0x06006F87 RID: 28551 RVA: 0x002719E8 File Offset: 0x0026FBE8
		public bool BuildZone(Zone Z)
		{
			List<Cell> emptyReachableCells = Z.GetEmptyReachableCells();
			if (emptyReachableCells.Count == 0)
			{
				return false;
			}
			emptyReachableCells.GetRandomElement<Cell>(null).AddObject("WedgeChest4", null, null, null, null);
			return true;
		}
	}
}
