using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB3 RID: 2739
	public class StarappleFarmMaker
	{
		// Token: 0x06006F63 RID: 28515 RVA: 0x0026DD49 File Offset: 0x0026BF49
		public bool BuildZone(Zone Z)
		{
			return new StarappleFarm().BuildZone(Z, true, null, null, null);
		}
	}
}
