using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A5E RID: 2654
	public class CaveNorthMouth : IConnectionBuilder
	{
		// Token: 0x06006E32 RID: 28210 RVA: 0x0025A26E File Offset: 0x0025846E
		public bool BuildZone(Zone Z)
		{
			this.Range = 3;
			return base.ConnectionMouth(Z, "Cave", "North", "Mouth");
		}
	}
}
