using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ABA RID: 2746
	[Serializable]
	public class OmonporchGroveBuilder
	{
		// Token: 0x06006F75 RID: 28533 RVA: 0x00270B8B File Offset: 0x0026ED8B
		public bool BuildZone(Zone Zone)
		{
			Zone.BuildReachableMap(0, 0, false);
			return true;
		}
	}
}
