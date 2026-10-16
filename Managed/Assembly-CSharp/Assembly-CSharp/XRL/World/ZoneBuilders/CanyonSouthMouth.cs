using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A5A RID: 2650
	public class CanyonSouthMouth : IConnectionBuilder
	{
		// Token: 0x06006E2A RID: 28202 RVA: 0x0025A1EC File Offset: 0x002583EC
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Canyon", "South", "Mouth");
		}
	}
}
