using System;
using XRL.World.ZoneParts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ACB RID: 2763
	public class RandomAmbientStabilization
	{
		// Token: 0x06006FAD RID: 28589 RVA: 0x00273935 File Offset: 0x00271B35
		public bool BuildZone(Zone Zone)
		{
			Zone.RequirePart<AmbientStabilization>(false).Strength = this.Strength.RollCached();
			return true;
		}

		// Token: 0x04003357 RID: 13143
		public string Strength = "40";
	}
}
