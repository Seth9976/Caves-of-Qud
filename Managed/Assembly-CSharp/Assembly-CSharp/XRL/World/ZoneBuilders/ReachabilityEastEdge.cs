using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A6D RID: 2669
	public class ReachabilityEastEdge
	{
		// Token: 0x06006E58 RID: 28248 RVA: 0x0025B71C File Offset: 0x0025991C
		public bool BuildZone(Zone Z)
		{
			if (this.ClearFirst)
			{
				Z.ClearReachableMap();
			}
			for (int i = 0; i < Z.Height; i++)
			{
				Z.BuildReachableMap(Z.Width - 1, i);
			}
			return true;
		}

		// Token: 0x0400327A RID: 12922
		public bool ClearFirst = true;
	}
}
