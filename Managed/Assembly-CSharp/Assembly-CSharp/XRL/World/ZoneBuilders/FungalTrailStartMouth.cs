using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A6A RID: 2666
	public class FungalTrailStartMouth : IConnectionBuilder
	{
		// Token: 0x06006E4D RID: 28237 RVA: 0x0025B230 File Offset: 0x00259430
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionStart(Z, "FungalTrail", "Mouth");
		}
	}
}
