using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ABF RID: 2751
	public class PlaceWedgeBuilder2
	{
		// Token: 0x06006F83 RID: 28547 RVA: 0x00271968 File Offset: 0x0026FB68
		public bool BuildZone(Zone Z)
		{
			List<Cell> emptyReachableCells = Z.GetEmptyReachableCells();
			if (emptyReachableCells.Count == 0)
			{
				return false;
			}
			emptyReachableCells.GetRandomElement<Cell>(null).AddObject("WedgeChest2", null, null, null, null);
			return true;
		}
	}
}
