using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A65 RID: 2661
	public class FungalTrailEastMouth : IConnectionBuilder
	{
		// Token: 0x06006E43 RID: 28227 RVA: 0x0025B02F File Offset: 0x0025922F
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "FungalTrail", "East", "Mouth");
		}
	}
}
