using System;

namespace UnityEngine
{
	// Token: 0x02000045 RID: 69
	public struct QueryParameters
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00009A2F File Offset: 0x00007C2F
		public QueryParameters(int layerMask = -5, bool hitMultipleFaces = false, QueryTriggerInteraction hitTriggers = QueryTriggerInteraction.UseGlobal, bool hitBackfaces = false)
		{
			this.layerMask = layerMask;
			this.hitMultipleFaces = hitMultipleFaces;
			this.hitTriggers = hitTriggers;
			this.hitBackfaces = hitBackfaces;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00009A4F File Offset: 0x00007C4F
		public static QueryParameters Default
		{
			get
			{
				return new QueryParameters(-5, false, QueryTriggerInteraction.UseGlobal, false);
			}
		}

		// Token: 0x0400011D RID: 285
		public int layerMask;

		// Token: 0x0400011E RID: 286
		public bool hitMultipleFaces;

		// Token: 0x0400011F RID: 287
		public QueryTriggerInteraction hitTriggers;

		// Token: 0x04000120 RID: 288
		public bool hitBackfaces;
	}
}
