using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A92 RID: 2706
	public class ShugBurrowNorthMouth : IConnectionBuilder
	{
		// Token: 0x06006EE7 RID: 28391 RVA: 0x002628C9 File Offset: 0x00260AC9
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "ShugBurrow", "North", "Mouth");
		}
	}
}
