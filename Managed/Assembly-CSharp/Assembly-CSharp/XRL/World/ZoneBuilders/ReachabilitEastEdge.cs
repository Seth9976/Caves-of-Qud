using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A6F RID: 2671
	public class ReachabilitEastEdge
	{
		// Token: 0x06006E5C RID: 28252 RVA: 0x0025B7B4 File Offset: 0x002599B4
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

		// Token: 0x0400327C RID: 12924
		public bool ClearFirst = true;
	}
}
