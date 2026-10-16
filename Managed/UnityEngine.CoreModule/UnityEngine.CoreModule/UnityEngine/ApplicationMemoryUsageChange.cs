using System;

namespace UnityEngine
{
	// Token: 0x02000107 RID: 263
	public struct ApplicationMemoryUsageChange
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000B1DF File Offset: 0x000093DF
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000B1E7 File Offset: 0x000093E7
		public ApplicationMemoryUsage memoryUsage { readonly get; private set; }

		// Token: 0x06000620 RID: 1568 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public ApplicationMemoryUsageChange(ApplicationMemoryUsage usage)
		{
			this.memoryUsage = usage;
		}
	}
}
