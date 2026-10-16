using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A75 RID: 2677
	public class RiverStartMouth : IConnectionBuilder
	{
		// Token: 0x06006E69 RID: 28265 RVA: 0x0025C198 File Offset: 0x0025A398
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionStart(Z, "River", "Mouth");
		}
	}
}
