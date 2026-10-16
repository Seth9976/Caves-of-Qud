using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ABE RID: 2750
	public class PlaceWedgeBuilder1
	{
		// Token: 0x06006F81 RID: 28545 RVA: 0x00271928 File Offset: 0x0026FB28
		public bool BuildZone(Zone Z)
		{
			List<Cell> emptyReachableCells = Z.GetEmptyReachableCells();
			if (emptyReachableCells.Count == 0)
			{
				return false;
			}
			emptyReachableCells.GetRandomElement<Cell>(null).AddObject("WedgeChest1", null, null, null, null);
			return true;
		}
	}
}
