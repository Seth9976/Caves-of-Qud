using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A7B RID: 2683
	public class RoadStartMouth : IConnectionBuilder
	{
		// Token: 0x06006E77 RID: 28279 RVA: 0x0025C804 File Offset: 0x0025AA04
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionStart(Z, "Road", "Mouth");
		}
	}
}
