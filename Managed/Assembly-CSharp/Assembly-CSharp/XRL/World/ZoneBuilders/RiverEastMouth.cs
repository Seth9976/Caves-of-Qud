using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A72 RID: 2674
	public class RiverEastMouth : IConnectionBuilder
	{
		// Token: 0x06006E63 RID: 28259 RVA: 0x0025C138 File Offset: 0x0025A338
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "River", "East", "Mouth");
		}
	}
}
