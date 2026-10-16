using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A94 RID: 2708
	public class ShugBurrowAscendingMouth : IConnectionBuilder
	{
		// Token: 0x06006EEB RID: 28395 RVA: 0x0026290B File Offset: 0x00260B0B
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionStart(Z, "ShugBurrow", "AscendingMouth");
		}
	}
}
