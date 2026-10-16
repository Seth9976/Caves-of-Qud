using System;
using XRL.World;

namespace XRL.Core
{
	// Token: 0x02000630 RID: 1584
	[Obsolete]
	[Serializable]
	public class CommandQueueEntry : IComposite
	{
		// Token: 0x0400208A RID: 8330
		public string Action;

		// Token: 0x0400208B RID: 8331
		public object Target;

		// Token: 0x0400208C RID: 8332
		public int SegmentDelay;
	}
}
