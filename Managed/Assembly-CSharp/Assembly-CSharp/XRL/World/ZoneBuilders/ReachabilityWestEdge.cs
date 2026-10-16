using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A70 RID: 2672
	public class ReachabilityWestEdge
	{
		// Token: 0x06006E5E RID: 28254 RVA: 0x0025B800 File Offset: 0x00259A00
		public bool BuildZone(Zone Z)
		{
			if (this.ClearFirst)
			{
				Z.ClearReachableMap();
			}
			for (int i = 0; i < Z.Width; i++)
			{
				Z.BuildReachableMap(0, Z.Height - 1);
			}
			return true;
		}

		// Token: 0x0400327D RID: 12925
		public bool ClearFirst = true;
	}
}
