using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A41 RID: 2625
	public class BasicLair : ZoneBuilderSandbox
	{
		// Token: 0x06006DC8 RID: 28104 RVA: 0x00250D18 File Offset: 0x0024EF18
		public bool BuildZone(Zone Z)
		{
			return new SultanDungeon().BuildRandomZoneWithArgs(Z, 0, true, this.Adjectives.Split(',', StringSplitOptions.None), this.Stairs, this.Table, "Lairs_");
		}

		// Token: 0x04003240 RID: 12864
		public string Table = "";

		// Token: 0x04003241 RID: 12865
		public string Adjectives = "";

		// Token: 0x04003242 RID: 12866
		public string Stairs = "";
	}
}
