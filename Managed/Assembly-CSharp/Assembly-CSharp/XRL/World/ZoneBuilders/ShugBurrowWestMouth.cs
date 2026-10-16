using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A91 RID: 2705
	public class ShugBurrowWestMouth : IConnectionBuilder
	{
		// Token: 0x06006EE5 RID: 28389 RVA: 0x002628A2 File Offset: 0x00260AA2
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "ShugBurrow", "West", "Mouth");
		}
	}
}
