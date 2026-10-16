using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A69 RID: 2665
	public class FungalTrailSouthMouth : IConnectionBuilder
	{
		// Token: 0x06006E4B RID: 28235 RVA: 0x0025B210 File Offset: 0x00259410
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "FungalTrail", "South", "Mouth");
		}
	}
}
