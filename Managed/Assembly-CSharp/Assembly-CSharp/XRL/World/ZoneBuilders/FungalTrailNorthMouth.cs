using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A68 RID: 2664
	public class FungalTrailNorthMouth : IConnectionBuilder
	{
		// Token: 0x06006E49 RID: 28233 RVA: 0x0025B1F0 File Offset: 0x002593F0
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "FungalTrail", "North", "Mouth");
		}
	}
}
