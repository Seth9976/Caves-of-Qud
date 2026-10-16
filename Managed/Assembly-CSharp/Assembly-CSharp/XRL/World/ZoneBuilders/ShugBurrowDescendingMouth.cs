using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A93 RID: 2707
	public class ShugBurrowDescendingMouth : IConnectionBuilder
	{
		// Token: 0x06006EE9 RID: 28393 RVA: 0x002628F0 File Offset: 0x00260AF0
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionStart(Z, "ShugBurrow", "DescendingMouth");
		}
	}
}
