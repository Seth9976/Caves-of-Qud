using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A5F RID: 2655
	public class CaveSouthMouth : IConnectionBuilder
	{
		// Token: 0x06006E34 RID: 28212 RVA: 0x0025A295 File Offset: 0x00258495
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "Cave", "South", "Mouth");
		}
	}
}
