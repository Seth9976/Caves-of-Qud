using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A5B RID: 2651
	public class CanyonStartMouth : IConnectionBuilder
	{
		// Token: 0x06006E2C RID: 28204 RVA: 0x0025A20C File Offset: 0x0025840C
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionStart(Z, "Canyon", "Mouth");
		}
	}
}
