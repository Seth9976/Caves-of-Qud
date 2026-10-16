using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC2 RID: 2754
	public class PlaceWedgeBuilder6
	{
		// Token: 0x06006F89 RID: 28553 RVA: 0x00271A28 File Offset: 0x0026FC28
		public bool BuildZone(Zone Z)
		{
			List<Cell> emptyReachableCells = Z.GetEmptyReachableCells();
			if (emptyReachableCells.Count == 0)
			{
				return false;
			}
			emptyReachableCells.GetRandomElement<Cell>(null).AddObject("WedgeChest6", null, null, null, null);
			return true;
		}
	}
}
