using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A57 RID: 2647
	public class CanyonEastMouth : IConnectionBuilder
	{
		// Token: 0x06006E24 RID: 28196 RVA: 0x0025A0A4 File Offset: 0x002582A4
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Canyon", "East", "Mouth");
		}
	}
}
