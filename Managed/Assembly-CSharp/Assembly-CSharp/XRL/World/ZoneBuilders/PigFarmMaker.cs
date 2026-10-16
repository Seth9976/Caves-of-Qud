using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB1 RID: 2737
	public class PigFarmMaker
	{
		// Token: 0x06006F5D RID: 28509 RVA: 0x0026D7F9 File Offset: 0x0026B9F9
		public bool BuildZone(Zone Z)
		{
			return new PigFarm().BuildZone(Z, true, null, null, null);
		}
	}
}
