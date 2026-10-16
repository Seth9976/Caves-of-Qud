using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A5D RID: 2653
	public class CaveEastMouth : IConnectionBuilder
	{
		// Token: 0x06006E30 RID: 28208 RVA: 0x0025A247 File Offset: 0x00258447
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "Cave", "East", "Mouth");
		}
	}
}
