using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A6B RID: 2667
	public class FungalTrailWestMouth : IConnectionBuilder
	{
		// Token: 0x06006E4F RID: 28239 RVA: 0x0025B24B File Offset: 0x0025944B
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "FungalTrail", "West", "Mouth");
		}
	}
}
