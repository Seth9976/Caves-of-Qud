using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ACD RID: 2765
	public class RegionPopulator : ZoneBuilderSandbox
	{
		// Token: 0x06006FCB RID: 28619 RVA: 0x002763A8 File Offset: 0x002745A8
		public bool BuildZone(Zone Z)
		{
			ZoneBuilderSandbox.GenerateInfluenceMap(Z, null, InfluenceMapSeedStrategy.FurthestPoint, this.RegionSize, null, true, null);
			return true;
		}

		// Token: 0x0400335E RID: 13150
		public int RegionSize = 100;
	}
}
