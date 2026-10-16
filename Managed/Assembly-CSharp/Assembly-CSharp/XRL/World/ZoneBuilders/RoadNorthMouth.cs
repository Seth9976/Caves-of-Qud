using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A79 RID: 2681
	public class RoadNorthMouth : IConnectionBuilder
	{
		// Token: 0x06006E73 RID: 28275 RVA: 0x0025C7C4 File Offset: 0x0025A9C4
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Road", "North", "Mouth");
		}
	}
}
