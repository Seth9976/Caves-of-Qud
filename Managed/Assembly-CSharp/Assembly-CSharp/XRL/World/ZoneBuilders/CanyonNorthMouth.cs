using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A58 RID: 2648
	public class CanyonNorthMouth : IConnectionBuilder
	{
		// Token: 0x06006E26 RID: 28198 RVA: 0x0025A0C4 File Offset: 0x002582C4
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Canyon", "North", "Mouth");
		}
	}
}
