using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A76 RID: 2678
	public class RiverWestMouth : IConnectionBuilder
	{
		// Token: 0x06006E6B RID: 28267 RVA: 0x0025C1B3 File Offset: 0x0025A3B3
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "River", "West", "Mouth");
		}
	}
}
