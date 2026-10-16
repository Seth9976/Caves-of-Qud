using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC0 RID: 2752
	public class PlaceWedgeBuilder3
	{
		// Token: 0x06006F85 RID: 28549 RVA: 0x002719A8 File Offset: 0x0026FBA8
		public bool BuildZone(Zone Z)
		{
			List<Cell> emptyReachableCells = Z.GetEmptyReachableCells();
			if (emptyReachableCells.Count == 0)
			{
				return false;
			}
			emptyReachableCells.GetRandomElement<Cell>(null).AddObject("WedgeChest3", null, null, null, null);
			return true;
		}
	}
}
