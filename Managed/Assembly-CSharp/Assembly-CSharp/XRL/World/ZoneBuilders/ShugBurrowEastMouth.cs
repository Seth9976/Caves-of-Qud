using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A8F RID: 2703
	public class ShugBurrowEastMouth : IConnectionBuilder
	{
		// Token: 0x06006EE1 RID: 28385 RVA: 0x00262854 File Offset: 0x00260A54
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "ShugBurrow", "East", "Mouth");
		}
	}
}
