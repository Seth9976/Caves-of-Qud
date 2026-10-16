using System;

namespace UnityEngine
{
	// Token: 0x020001FD RID: 509
	public struct MipmapLimitDescriptor
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0002F341 File Offset: 0x0002D541
		public readonly bool useMipmapLimit { get; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0002F349 File Offset: 0x0002D549
		public readonly string groupName { get; }

		// Token: 0x06001B07 RID: 6919 RVA: 0x0002F351 File Offset: 0x0002D551
		public MipmapLimitDescriptor(bool useMipmapLimit, string groupName)
		{
			this.useMipmapLimit = useMipmapLimit;
			this.groupName = groupName;
		}
	}
}
