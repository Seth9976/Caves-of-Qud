using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A5C RID: 2652
	public class CanyonWestMouth : IConnectionBuilder
	{
		// Token: 0x06006E2E RID: 28206 RVA: 0x0025A227 File Offset: 0x00258427
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Canyon", "West", "Mouth");
		}
	}
}
