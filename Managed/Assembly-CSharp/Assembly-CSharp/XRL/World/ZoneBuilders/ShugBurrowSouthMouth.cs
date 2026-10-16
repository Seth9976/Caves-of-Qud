using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A90 RID: 2704
	public class ShugBurrowSouthMouth : IConnectionBuilder
	{
		// Token: 0x06006EE3 RID: 28387 RVA: 0x0026287B File Offset: 0x00260A7B
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "ShugBurrow", "South", "Mouth");
		}
	}
}
