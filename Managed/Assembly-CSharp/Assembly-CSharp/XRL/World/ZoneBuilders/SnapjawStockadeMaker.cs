using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB5 RID: 2741
	public class SnapjawStockadeMaker
	{
		// Token: 0x06006F69 RID: 28521 RVA: 0x0026E280 File Offset: 0x0026C480
		public bool BuildZone(Zone Z)
		{
			return new StockadeMaker().BuildZone(Z, true, "BrinestalkStakes", "SnapjawFortGlobals", null, null, "30-50", "16-24", false);
		}
	}
}
