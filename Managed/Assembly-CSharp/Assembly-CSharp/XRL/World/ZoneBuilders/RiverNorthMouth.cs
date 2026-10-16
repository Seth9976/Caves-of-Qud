using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A73 RID: 2675
	public class RiverNorthMouth : IConnectionBuilder
	{
		// Token: 0x06006E65 RID: 28261 RVA: 0x0025C158 File Offset: 0x0025A358
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "River", "North", "Mouth");
		}
	}
}
