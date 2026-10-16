using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A6E RID: 2670
	public class ReachabilitySouthEdge
	{
		// Token: 0x06006E5A RID: 28250 RVA: 0x0025B768 File Offset: 0x00259968
		public bool BuildZone(Zone Z)
		{
			if (this.ClearFirst)
			{
				Z.ClearReachableMap();
			}
			for (int i = 0; i < Z.Width; i++)
			{
				Z.BuildReachableMap(i, Z.Height - 1);
			}
			return true;
		}

		// Token: 0x0400327B RID: 12923
		public bool ClearFirst = true;
	}
}
